using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using Sys.IStations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys.IStations
{
    public class AppHubCenter : IDisposable
    {
        private static object locker = new object();

        string folderPath = AppDomain.CurrentDomain.BaseDirectory + "AppConfig";
        string SystemCfgFilePath = AppDomain.CurrentDomain.BaseDirectory + "AppConfig\\AppConfig.cfg";
        string MCardChnNamesPath = AppDomain.CurrentDomain.BaseDirectory + "AppConfig\\MCardChnNames.cfg";
        string StationMgrPath = AppDomain.CurrentDomain.BaseDirectory + "AppConfig\\AppStation.cfg";

        public AppCfgFromXml SystemCfg { get; private set; }

        public static string CK_InitDevParams = "子设备初始化参数";//用于保存系统初始化时创建设备(运动控制器/相机/机械手)对象的参数，Key = string, ValueType = SortedDictionary ,SortedDictionary[Key = DeviceID,value = List<object>]
        static string CT_DEV = "设备管理";

        private static readonly Lazy<AppHubCenter> lazy = new Lazy<AppHubCenter>(() => new AppHubCenter());
        public static AppHubCenter Instance { get { return lazy.Value; } }

        /// <summary>
        /// </summary>
        AppHubCenter()
        {
            Initialize();
        }

        void Initialize()
        {
            string chkError = "";
            //判断文件夹是否存在
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (!_CheckSysCfg(SystemCfgFilePath, true, out chkError))
            {
                if (DialogResult.Cancel == MessageBox.Show("错误信息:" + chkError + "\n点击 \"确定\" 重新选择配置\n点击 \"取消\" 退出应用程序", "配置文件格式错误!", MessageBoxButtons.OKCancel))
                    System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id).Kill();

            }

            // 加载参数
            SystemCfg = new AppCfgFromXml();
            SystemCfg.Load(SystemCfgFilePath, true);

            // 线程中 IPlatInitializable 实体类
            InitorHelp = new AppIplatinitHelper();

            // 数据池
            dataPool = new AppDataPool();
            // 运动控制卡 单元管理
            _mdCellNameMgr = new AppDevCellNameManeger(MCardChnNamesPath);//运动控制卡单元名称管理


            // 添加 外部dll
            // InitorHelp.AppendDll(System.AppDomain.CurrentDomain.BaseDirectory + "Org.IBarcode.dll");
            // InitorHelp.AppendDll(System.AppDomain.CurrentDomain.BaseDirectory + "Org.ICamera.dll");
        }

        /// <summary>
        ///  内外部所有继承 IPlatInitializable 实体类
        /// </summary>
        public AppIplatinitHelper InitorHelp { get; private set; }


        AppInitorManager _initMgr = null;
        object initMgrLock = new object();
        public AppInitorManager InitorManager
        {
            get
            {
                if (_initMgr != null)
                    return _initMgr;
                else
                {
                    if (_initMgr != null)
                        return _initMgr;

                    _initMgr = new AppInitorManager();
                    _initMgr.Init();
                }
                return _initMgr;
            }

        }

        AppDevCellNameManeger _mdCellNameMgr = null; //控制卡名称管理
        public AppDevCellNameManeger MDCellNameMgr { get { return _mdCellNameMgr; } }

        AppDataPool dataPool = null;
        public IDataPool DataPool { get { return dataPool; } }


        AppStationManager _stationMgr = null;
        public AppStationManager StationMgr
        {
            get
            {
                if (_stationMgr == null)
                {
                    lock (this)
                    {
                        if (null == _stationMgr)
                        {
                            _stationMgr = new AppStationManager(StationMgrPath);//工站管理器
                        }
                    }
                }
                return _stationMgr;
            }
        }


        /// <summary>
        /// 检查配置文件是否合规（如果缺少必要的数据项，则返回false）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="isOpenOrCreate">如果文件不存在，是否创建</param>
        bool _CheckSysCfg(string filePath, bool isOpenOrCreate, out string errorInfo)
        {
            errorInfo = "";
            if (string.IsNullOrEmpty(filePath))
            {
                errorInfo = "文件名为空值（或空格）";
                return false;
            }

            if (!File.Exists(filePath)) //文件不存在
            {
                if (!isOpenOrCreate)
                {
                    errorInfo = "文件不存在";
                    return false;
                }
                try
                {
                    AppCfgFromXml cfg = new AppCfgFromXml();
                    cfg.Load(filePath, true);
                    cfg.AddItem(CK_InitDevParams, new DictionaryEx<string, List<object>>(), CT_DEV);

                    cfg.Save();
                    return true;
                }
                catch (Exception ex)
                {
                    errorInfo = ex.Message;
                    return false;
                }
            }
            else //文件已存在，检查格式（只检查必须项是否存在）
            {
                try
                {
                    bool isCheckOK = true;
                    StringBuilder sbError = new StringBuilder();
                    AppCfgFromXml cfg = new AppCfgFromXml();
                    cfg.Load(filePath, false);

                    if (!cfg.ContainsItem(CK_InitDevParams))
                    {
                        sbError.Append("文件中不存在配置项:" + CK_InitDevParams + "\n");
                        isCheckOK = false;
                    }
                    if (!isCheckOK)
                        errorInfo = sbError.ToString();
                    return isCheckOK;
                }
                catch (Exception ex)
                {
                    errorInfo = ex.Message;
                    return false;
                }

            }

        }


        ~AppHubCenter()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            ////////////释放非托管资源
            if (disposing)//////////////释放其他托管资源
            {

            }

        }
    }
}
