using Cell.DataModel;
using Cell.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sys.IStations
{
    public abstract class ICmdWorkBase : IPlatCmdWork
    {
        public ICmdWorkBase()
        {
            cmdEvent = new ManualResetEvent(false);
            rspEvent = new AutoResetEvent(false);
            command = CmdUnknown;
            cmdResult = ICmdResult.UnknownError;
            thread = new Thread(ThreadFunc);
            CurrWorkStatus = IWorkStatus.UnStart;
            accessLocker = new object();
            CycleMilliseconds = 3;
        }

        public event CustomStatusChange CustomStatusChanged;

        /// <summary>线程内部轮询周期</summary>
        public int CycleMilliseconds { get; set; }

        public IWorkStatus CurrWorkStatus { get; protected set; }


        /// <summary> 用于保存调用者向线程发送的指令</summary>
        protected long command = 0;
        /// <summary>线程访问同步锁</summary>
        protected object accessLocker;
        /// <summary>用于保存线程返回的执行指令（结果）</summary>
        protected ICmdResult cmdResult = ICmdResult.UnknownError;
        /// <summary>调用者向线程发送指令的触发对象</summary>
        protected EventWaitHandle cmdEvent = null;
        /// <summary>线程返回执行结果的触发对象</summary>
        protected EventWaitHandle rspEvent = null;
        /// <summary>工作线程对象</summary>
        Thread thread = null;

        #region  IPlatOrder'API

        public virtual string Name { get; set; }

        public bool IsWorking()
        {
            return IsWorkingStatus(CurrWorkStatus);
        }

        public static bool IsWorkingStatus(IWorkStatus ws)
        {
            return ws == IWorkStatus.Running || ws == IWorkStatus.Pausing || ws == IWorkStatus.Interactiving;

        }

        /// <summary>
        /// 用于判断当前函数是否在主线程中运行
        /// </summary>
        /// <returns></returns>
        protected bool IsInWorkThread()
        {
            if (null == thread)
                return false;
            if (IsWorking() && Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId)
                return true;
            return false;
        }

        /// <summary>
        /// 发送开始 指令
        /// </summary>
        /// <returns></returns>
        public virtual ICmdResult Start()
        {
            lock (accessLocker)
            {
                lock (WorkStatusLocker)
                {
                    if (IsWorking())
                        return ICmdResult.Success;
                    thread = new Thread(ThreadFunc);
                    cmdEvent.Reset();
                    rspEvent.Reset();
                    thread.Start();
                }

                return _SendCmd(CmdStart);


            }
        }
        public virtual ICmdResult Stop(int timeoutMilliseconds = -1)
        {
            lock (accessLocker)
            {
                ICmdResult ret = ICmdResult.UnknownError;
                Monitor.Enter(WorkStatusLocker);
                {
                    if (!IsWorking())
                    {
                        Monitor.Exit(WorkStatusLocker);
                        return ICmdResult.Success;
                    }
                    ret = _SendCmd(CmdStop, timeoutMilliseconds);

                }
                Monitor.Exit(WorkStatusLocker);
                return ret;
            }
        }
        public virtual ICmdResult Pause(int timeoutMilliseconds = -1)
        {
            lock (accessLocker)
            {
                lock (WorkStatusLocker)
                {
                    if (CurrWorkStatus == IWorkStatus.Pausing)
                        return ICmdResult.Success;
                    if (CurrWorkStatus != IWorkStatus.Running)
                        return ICmdResult.StatusError;
                }
                return _SendCmd(CmdPause, timeoutMilliseconds);
            }
        }
        public virtual ICmdResult Resume(int timeoutMilliseconds = -1)
        {
            lock (accessLocker)
            {
                lock (WorkStatusLocker)
                {
                    if (CurrWorkStatus == IWorkStatus.Running)
                        return ICmdResult.Success;
                    if (CurrWorkStatus != IWorkStatus.Pausing)
                        return ICmdResult.StatusError;
                }
                return _SendCmd(CmdResume, timeoutMilliseconds);

            }
        }

        public void Abort()
        {
            lock (accessLocker)
            {
                if (!thread.IsAlive)
                    return;
                thread.Abort();
                thread = null;
                ChangeWorkStatus(IWorkStatus.AbortExit);
            }
        }

        public event WorkStatusChange WorkStatusChanged;
        public event WorkMsgInfo WorkMsg2Outter;
        /// <summary>
        /// 线程函数内部调用
        /// 工作状态变化时，需要调用此函数
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="info"></param>
        protected void ChangeWorkStatus(IWorkStatus ws)
        {
            lock (WorkStatusLocker)
            {
                if (CurrWorkStatus == ws)
                    return;
                CurrWorkStatus = ws;
            }
            WorkStatusChanged?.Invoke(this, CurrWorkStatus);
        }

        protected virtual void ThreadFunc()
        {
            //  重写了
        }
        #endregion


        #region  附加方法  中的 方法


        /// <summary>
        /// 通过注册的消息回调函数向外界发送一条文本消息
        /// </summary>
        /// <param name="info"></param>
        protected void SendMsg2Outter(string info)
        {

            if (string.IsNullOrEmpty(info))
                return;
            WorkMsg2Outter?.Invoke(this, info);
        }

        /// <summary>
        /// 工作线程开始时的准备工作，在线程开始时只执行一次
        /// 线程函数内部在调用PrepareWhenWorkStart后，进入While循环调用RunLoopInWork
        /// </summary>
        protected abstract void PrepareWhenWorkStart();

        /// <summary>
        /// 一个工作循环步骤，在线程的While循环中被重复调用，直到
        /// </summary>
        protected abstract void RunLoopInWork();

        /// <summary>
        /// 线程结束前的清理步骤
        /// </summary>
        protected abstract void CleanupWhenWorkExit();

        #endregion

        #region 工作线程函数内使用
        /// <summary>
        /// 基类内部调用，向线程发送包括开始/停止/暂停/恢复指令和用户指令在内的所有指令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <returns></returns>
        protected ICmdResult _SendCmd(long cmd, int timeoutMilliseconds = -1)
        {
            lock (accessLocker)
            {
                rspEvent.Reset();
                command = cmd;
                cmdEvent.Set();
                if (!rspEvent.WaitOne(timeoutMilliseconds))
                {
                    cmdEvent.Reset();
                    return ICmdResult.Timeout;
                }
                cmdEvent.Reset();
                return cmdResult;
            }
        }



        /// <summary>
        /// 继承类线程函数内部使用
        /// ☆此方法禁止在非runloop线程中使用 会误发指令☆
        /// </summary>
        protected CCRet CheckCmd(int timeoutMilliseconds)
        {
            while (true)
            {
                if (CurrWorkStatus == IWorkStatus.Pausing) //当前处于暂停状态
                {
                    if (!cmdEvent.WaitOne(timeoutMilliseconds))
                        continue;
                    if (command == CmdResume) //收到恢复运行指令
                    {
                        RespCmd(ICmdResult.Success);
                        OnResume();
                        ChangeWorkStatus(IWorkStatus.Running);
                        return CCRet.Resume;
                    }
                    else if (command == CmdStop)
                    {

                        RespCmd(ICmdResult.Success);
                        OnStop();
                        ExitWork(WorkExitCode.Command, "");
                        return CCRet.Normal;
                    }
                    else //不接受其他指令
                    {
                        RespCmd(ICmdResult.StatusError);
                        continue;
                    }
                }

                if (CurrWorkStatus != IWorkStatus.Running)
                {
                    Thread.Sleep(timeoutMilliseconds);
                    RespCmd(ICmdResult.StatusError);
                    return CCRet.Error;
                }

                //线程当前处于运行状态
                if (!cmdEvent.WaitOne(timeoutMilliseconds))
                    return CCRet.Normal;
                if (command == CmdStop)
                {
                    RespCmd(ICmdResult.Success);
                    OnStop();
                    ExitWork(WorkExitCode.Command, "收到退出指令" + DateTime.Now.ToString("yy.MM.dd.hh:mm:ss"));
                    return CCRet.Normal;
                }
                else if (command == CmdPause)
                {

                    RespCmd(ICmdResult.Success);
                    OnPause();
                    ChangeWorkStatus(IWorkStatus.Pausing);
                    continue;
                }
                else if (command == CmdResume)
                {
                    RespCmd(ICmdResult.StatusError);
                    return CCRet.Resume;
                }
                else if (command >= int.MinValue && command <= int.MaxValue) //收到用户指令
                {
                    return CCRet.Normal;
                }
                else //不支持的指令
                {
                    RespCmd(ICmdResult.IllegalCmd);
                    return CCRet.Error;
                }

            }


        }

        protected void RespCmd(ICmdResult cmdRst)
        {
            cmdEvent.Reset();
            cmdResult = cmdRst;
            rspEvent.Set();

        }

        #endregion

        /// <summary>
        /// 业务状态发生改变时，调用此函数
        /// </summary>
        /// <param name="customStatus"></param>
        /// <param name="info"></param>
        /// <param name="param"></param>
        public void ChangeCustomStatus(int customStatus)
        {
            if (AllCustomStatus == null || AllCustomStatus.Length == 0)
                throw new ArgumentException("ChangeCustomStatus(int customStatus) failed by: " + customStatus + " is not defined!");
            CurrCustomStatus = customStatus;
            CustomStatusChanged?.Invoke(this, customStatus);
        }

        protected object WorkStatusLocker = new object();



        /// <summary>
        /// 工作线程内部收到暂停指令时，会先调用OnPause函数，继承类可重写此函数
        /// </summary>
        protected abstract void OnPause();

        /// <summary>
        /// 工作线程内部处于暂停状态时，收到恢复运行指令时，会调用OnResume函数，继承类可重写此函数
        /// </summary>
        protected abstract void OnResume();

        /// <summary>
        /// 工作线程内部收到推出指令时，会先调用OnStop函数，然后进入退出流程
        /// </summary>
        protected abstract void OnStop();


        /// <summary>
        /// 线程内部调用，退出线程
        /// </summary>
        /// <param name="wec"></param>
        /// <param name="info"></param>
        /// <param name="param"></param>
        protected void ExitWork(WorkExitCode wec, string info, object param = null)
        {
            switch (wec)
            {
                case WorkExitCode.Command:
                    break;
                case WorkExitCode.Error:
                    break;
                case WorkExitCode.Exception:
                    break;
                case WorkExitCode.Normal:
                    break;
            }
            throw new IWorkExitException(wec, info, param);
        }


        #region 用户指令

        List<int> _lstCommands = new List<int>();
        Dictionary<int, string> _dctCommandNames = new Dictionary<int, string>();
        public virtual int[] AllCmds { get { return _lstCommands.ToArray(); } }

        public virtual string GetCmdName(int cmd)
        {
            if (!_lstCommands.Contains(cmd))
                throw new ArgumentOutOfRangeException("cmd = " + cmd + "is not contained in command list");
            if (_dctCommandNames.ContainsKey(cmd))
                return _dctCommandNames[cmd];
            return cmd.ToString();
        }


        /// <summary>
        /// 声明一个工作命令 , 在集成类的构造函数中调用
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="name"></param>
        protected void DelearCommand(int cmd, string name)
        {
            if (null == name)
                throw new ArgumentNullException();

            if (_lstCommands.Contains(cmd))
                throw new ArgumentException("cmd = " + cmd + " is already decleared!");

            _lstCommands.Add(cmd);


            if (_dctCommandNames.ContainsKey(cmd))
                _dctCommandNames[cmd] = name;
            else
                _dctCommandNames.Add(cmd, name);


        }

        /// <summary>
        /// 用枚举类型申明所有的工作命令
        /// </summary>
        /// <param name="cmdEnum"></param>
        protected void DeclearAllCommands(Type cmdEnum)
        {
            _lstCommands.Clear();
            _dctCommandNames.Clear();
            Array cmds = Enum.GetValues(cmdEnum);
            string[] cmdNames = Enum.GetNames(cmdEnum);
            for (int i = 0; i < cmds.Length; i++)
                DelearCommand((int)cmds.GetValue(i), cmdNames[i]);
        }



        public ICmdResult SendCmd(int cmd, int timeoutMilliseconds = -1)
        {
            return _SendCmd((long)cmd, timeoutMilliseconds);
        }
        #endregion

        #region 用户自定义工作状态
        public int CurrCustomStatus { get; private set; }

        List<int> _lstCustomStatus = new List<int>();
        Dictionary<int, string> _dctCSNames = new Dictionary<int, string>();
        /// <summary>
        /// 在继承类中枚举所有的自定义工作状态（与业务逻辑相关的）
        /// 建议0为默认状态
        /// </summary>
        public virtual int[] AllCustomStatus { get { return _lstCustomStatus.ToArray(); } }


        public virtual string GetCustomStatusName(int status)
        {
            if (!_lstCustomStatus.Contains(status))
                throw new ArgumentOutOfRangeException();
            if (!_dctCSNames.ContainsKey(status))
                return status.ToString();
            return _dctCSNames[status];

        }

        /// <summary>
        /// 声明一个自定义状态 ,在继承类的构造函数中使用
        /// </summary>
        /// <param name="status"></param>
        /// <param name="name"></param>
        protected void DeclearCustomStatus(int status, string name)
        {
            if (null == name)
                throw new ArgumentNullException();
            if (_lstCustomStatus.Contains(status))
                throw new ArgumentException("status = " + status + " is already decleared!");
            _lstCustomStatus.Add(status);
            _dctCSNames.Add(status, name);
        }

        /// <summary>
        /// 用结构体类型定义所有的自定义状态,在继承类的构造函数中使用
        /// </summary>
        /// <param name="csEnum"></param>
        protected void DeclearAllCustomStatus(Type csEnum)
        {
            _lstCustomStatus.Clear();
            _dctCSNames.Clear();
            Array arCS = Enum.GetValues(csEnum);
            string[] csNames = Enum.GetNames(csEnum);
            for (int i = 0; i < arCS.Length; i++)
            {
                DeclearCustomStatus((int)arCS.GetValue(i), csNames[i]);
            }
        }
        #endregion


        protected static long CmdUnknown = long.MinValue;
        protected static long CmdStart = long.MinValue + 1;
        protected static long CmdStop = long.MinValue + 2;
        protected static long CmdPause = long.MinValue + 3;
        protected static long CmdResume = long.MinValue + 4;
    }
}
