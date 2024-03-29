using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public delegate void WorkMsgInfo(object sender, string msg);

    /// <summary>
    /// 线程退出代码
    /// </summary>
    public enum WorkExitCode
    {
        Normal, //线程正常完成后退出
        Command,    //收到退出指令
        Error,      //发生错误退出
        Exception,  //发生(程序)异常退出
    }

    /// <summary>
    /// 站点运行模式
    /// </summary>
    public enum IStationRunMode
    {
        Auto,   //自动(连续)运行
        Manual, //手动(单站)运行
    }

    public enum IWorkStatus
    {
        UnStart = 0,    //线程未开始运行
        Running,        //线程正在运行，未退出
        Pausing,        //线程暂停中
        Interactiving,  //人机交互 ， 等待人工干预指令
        NormalEnd,     //线程正常完成后退出
        CommandExit,    //收到退出指令
        ErrorExit,      //发生错误退出，（重启或人工消除错误后可恢复）
        ExceptionExit,  //发生异常退出 ,  (不可恢复的错误)
        AbortExit,      //由调用者强制退出
    }

    /// <summary>向线程发送用户指令的执行结果</summary>
    public enum ICmdResult
    {
        UnknownError = -1, //发生未定义的错误
        Success = 0, //指令执行成功
        IllegalCmd,//不支持的非法指令
        StatusError, //工作状态（包括用户自定义状态）不支持当前指令 ，（向未运行的线程发送Resume指令）
        ActionError, //指令执行失败
        Timeout,//线程超时未响应
    }
}
