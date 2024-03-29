using Cell.DataModel;
using Cell.Tools;
using Cell.UI;
using Sys.IStations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tissue.UI;

namespace Body.IMainStation
{
    /// <summary>
    /// 主界面设置参数的配套界面
    /// </summary>
    public partial class UcMainStationBaseCfg : UserControl
    {
        public UcMainStationBaseCfg()
        {
            InitializeComponent();
            //AppHubCenter.Instance.UserManager.EventLogin += OnUserLogin;
            //AppHubCenter.Instance.UserManager.EventLogout += OnUserLogout;
        }


        private void UcMainStationBaseCfg_Load(object sender, EventArgs e)
        {
            UpdateViewByStation();
            UpdateUIByCfg();
        }

        /// <summary>
        /// 用户登录回调
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="level"></param>
        /// <param name="levelName"></param>
        void OnUserLogin(string userName, int level, string levelName)
        {
            if (!IsHandleCreated)
                return;
            Invoke(new Action(() =>
            {
                if (!IsHandleCreated)
                    return;
                Enabled = true;
                tabControlCF1.TabPages.Clear();
                if (level > 2) //操作员
                    return;

                if (level == 2) //客户管理员，只能操作CustomSetting
                {
                    if (_isUseCumstomUI)
                        tabControlCF1.TabPages.Add(tpCustom);
                    else
                    {
                        tabControlCF1.TabPages.Add(tpSysCfg);
                        tabControlCF1.TabPages.Add(tpMSCfg);
                    }

                    return;
                }

                if (_isUseCumstomUI)
                    tabControlCF1.TabPages.Add(tpCustom);
                tabControlCF1.TabPages.Add(tpSysCfg);
                tabControlCF1.TabPages.Add(tpMSCfg);
            }));


        }

        void OnUserLogout(string userName, int level, string levelName)
        {
            if (!IsHandleCreated)
                return;
            try
            {
                Invoke(new Action(() =>
                {
                    if (!IsHandleCreated)
                        return;
                    Enabled = false;
                }));
            }
            catch
            {

            }

        }

        IMainStationBase _mainStation = null;
        bool _isUseCumstomUI = true; //是否使用用户自定义设置界面
        List<UcMsDioSetting> _lstFixedDI = new List<UcMsDioSetting>(); //固定DI 
        List<UcMsDioSetting> _lstFixedDO = new List<UcMsDioSetting>();
        List<UcMsDioSetting> _lstDeclearedDI = new List<UcMsDioSetting>();//声明的DI
        List<UcMsDioSetting> _lstDeclearedDO = new List<UcMsDioSetting>();
        List<UcParamEdit> _lstMsCfg = new List<UcParamEdit>(); //主公站配置
        List<UcParamEdit> _lstSysCfg = new List<UcParamEdit>(); //系统配置

        bool _isParamEditting = false; //工站参数是否在编辑中
        bool _isSysCfgEditting = false;//系统参数是否在编辑中


        void UpdateViewByStation()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateViewByStation));
                return;
            }
            if (_mainStation == null)
                return;

            _lstFixedDI.Clear(); //固定DI 
            _lstFixedDO.Clear();
            _lstDeclearedDI.Clear();//声明的DI
            _lstDeclearedDO.Clear();
            _lstMsCfg.Clear(); //主公站配置
            _lstSysCfg.Clear();

            //添加FixedDIO 设置控件
            Array arFixedDI = Enum.GetValues(typeof(FixedDI));
            foreach (int i in arFixedDI)
            {
                UcMsDioSetting ucDI = new UcMsDioSetting();
                ucDI.Enabled = false;
                ucDI.Tag = i;
                ucDI.DioName = ((FixedDI)i).ToString();
                ucDI.DioCombo.Tag = i;
                ucDI.DioCombo.SelectedIndexChanged += FixDiChnSelectIndexChg;
                ucDI.IsDout = false;
                fpFixedDIs.Controls.Add(ucDI);
                _lstFixedDI.Add(ucDI);
            }
            Array arFixedDO = Enum.GetValues(typeof(FixedDO));
            foreach (int i in arFixedDO)
            {
                UcMsDioSetting ucDO = new UcMsDioSetting();
                ucDO.Enabled = false;
                ucDO.Tag = i;
                ucDO.DioName = ((FixedDO)i).ToString();
                ucDO.DioCombo.Tag = i;
                ucDO.DioCombo.SelectedIndexChanged += FixDoChnSelectIndexChg;
                ucDO.IsDout = true;
                fpFixedDos.Controls.Add(ucDO);
                ucDO.DioLamp.Tag = i;
                ucDO.DioLamp.Click += OnFixedDoClick;
                _lstFixedDO.Add(ucDO);
            }

            //添加DeclearedIO 设置控件
            string[] diNames = _mainStation.DeclearedDiNames;
            if (null != diNames)
                foreach (string diName in diNames)
                {
                    UcMsDioSetting ucDi = new UcMsDioSetting();
                    ucDi.Enabled = false;
                    ucDi.Tag = diName;
                    ucDi.DioName = diName;
                    ucDi.DioCombo.Tag = diName;
                    ucDi.DioCombo.SelectedIndexChanged += DeclearedDiChnSelectIndexChg;
                    ucDi.IsDout = false;
                    fpDis.Controls.Add(ucDi);
                    _lstDeclearedDI.Add(ucDi);
                }
            string[] doNames = _mainStation.DeclearedDoNames;
            if (null != doNames)
                foreach (string doName in doNames)
                {
                    UcMsDioSetting ucDo = new UcMsDioSetting();
                    ucDo.Enabled = false;
                    ucDo.Tag = doName;
                    ucDo.DioName = doName;
                    ucDo.DioCombo.Tag = doName;
                    ucDo.DioCombo.SelectedIndexChanged += DeclearedDoChnSelectIndexChg;
                    ucDo.IsDout = true;
                    ucDo.DioLamp.Tag = doName;
                    ucDo.DioLamp.Click += OnDeclearDoClick;
                    fpDos.Controls.Add(ucDo);
                    _lstDeclearedDO.Add(ucDo);
                }

            int cnt = 0;
            int gbHeight = 150;
            //添加MainStation 参数控件
            string[] paramCategotys = _mainStation.AllParamCategotys;
            if (null != paramCategotys)
                foreach (string categoty in paramCategotys)
                {
                    CosParams[] pds = _mainStation.GetParamDescribs(categoty); //所有参数描述信息
                    if (null == pds || pds.Length == 0)
                        continue;
                    GroupBox gb = new GroupBox();
                    gb.Text = categoty;
                    gb.Height = gbHeight;
                    gb.Location = new Point(0, cnt * (gbHeight + 2));
                    gb.Width = pnCfg.Width;
                    gb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    FlowLayoutPanel fp = new FlowLayoutPanel();
                    fp.Dock = DockStyle.Fill;
                    fp.AutoScroll = true;
                    fp.FlowDirection = FlowDirection.TopDown;
                    fp.BackColor = SystemColors.ControlLight;
                    gb.Controls.Add(fp);
                    foreach (CosParams pd in pds)
                    {
                        UcParamEdit uc = new UcParamEdit();
                        uc.SetParamDesribe(pd);
                        uc.IsValueReadOnly = true;
                        uc.Height = 20;
                        fp.Controls.Add(uc);
                        _lstMsCfg.Add(uc);
                    }
                    pnCfg.Controls.Add(gb);
                    cnt++;
                }



            cnt = 0;
            //添加系统配置参数控件
            string[] sysCategotys = _mainStation.AllSysCfgCategotys;
            if (null != sysCategotys)
                foreach (string categoty in sysCategotys)
                {
                    CosParams[] pds = _mainStation.GetSysCfgDescribs(categoty); //所有参数描述信息
                    if (null == pds || pds.Length == 0)
                        continue;
                    GroupBox gb = new GroupBox();
                    gb.Text = categoty;
                    gb.Height = gbHeight;
                    gb.Location = new Point(0, cnt * (gbHeight + 2));
                    gb.Width = pnSysCfg.Width;
                    gb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    FlowLayoutPanel fp = new FlowLayoutPanel();
                    fp.Dock = DockStyle.Fill;
                    fp.AutoScroll = true;
                    fp.FlowDirection = FlowDirection.TopDown;
                    fp.BackColor = SystemColors.ControlLight;
                    gb.Controls.Add(fp);
                    foreach (CosParams pd in pds)
                    {
                        UcParamEdit uc = new UcParamEdit();
                        uc.SetParamDesribe(pd);
                        uc.IsValueReadOnly = true;
                        uc.Height = 20;
                        fp.Controls.Add(uc);
                        _lstSysCfg.Add(uc);
                    }
                    pnSysCfg.Controls.Add(gb);
                    cnt++;
                }
        }


        /// <summary>
        /// 固定DI 通道选择改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FixDiChnSelectIndexChg(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string ioName = ((FixedDI)cb.Tag).ToString();
            string chnName = cb.SelectedIndex < 0 ? "" : cb.Text;
            DioInfo ioInfo = (DioInfo)_mainStation._cfg.GetItemValue(ioName);//.GlobalChnName = chnName;
            ioInfo.GlobalChnName = chnName;
            _mainStation._cfg.SetItemValue(ioName, ioInfo); //结构体需要重新设入
        }


        void FixDoChnSelectIndexChg(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string ioName = ((FixedDO)cb.Tag).ToString();
            string chnName = cb.SelectedIndex < 0 ? "" : cb.Text;
            DioInfo ioInfo = (DioInfo)_mainStation._cfg.GetItemValue(ioName);//.GlobalChnName = chnName;
            ioInfo.GlobalChnName = chnName;
            _mainStation._cfg.SetItemValue(ioName, ioInfo);
        }

        void DeclearedDiChnSelectIndexChg(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string ioName = cb.Tag as string;
            string chnName = cb.SelectedIndex < 0 ? "" : cb.Text;
            DictionaryEx<string, DioInfo> declearedDIInfos = _mainStation._cfg.GetItemValue("DeclearedDI") as DictionaryEx<string, DioInfo>;
            DioInfo ioInfo = declearedDIInfos[ioName];
            ioInfo.GlobalChnName = chnName;
            declearedDIInfos[ioName] = ioInfo;
            //hehe
        }


        void DeclearedDoChnSelectIndexChg(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string ioName = cb.Tag as string;
            string chnName = cb.SelectedIndex < 0 ? "" : cb.Text;
            DictionaryEx<string, DioInfo> declearedDOInfos = _mainStation._cfg.GetItemValue("DeclearedDO") as DictionaryEx<string, DioInfo>;
            DioInfo ioInfo = declearedDOInfos[ioName];
            ioInfo.GlobalChnName = chnName;
            declearedDOInfos[ioName] = ioInfo;
        }


        public void SetMainStation(IMainStationBase mainStation)
        {
            _mainStation = mainStation;
            if (null == _mainStation)
            {
                lbMsTips.Text = "主工站未设置";
                lbSysTips.Text = "主工站未设置";
                return;
            }



            if (Created)
            {
                UpdateViewByStation();
                UpdateUIByCfg();
            }
        }


        /// <summary>
        /// 固定DO的按钮被点击（用于测试DO是否被正确设置）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnFixedDoClick(object sender, EventArgs e)
        {
            LampButton uc = sender as LampButton;
            FixedDO doType = (FixedDO)Enum.Parse(typeof(FixedDO), uc.Text);

            bool isCurrSigON = false;
            int ret = _mainStation.GetFixDoSig(doType, out isCurrSigON);
            if (ret != 0)
            {
                string info = "设置DO:" + doType.ToString() + (isCurrSigON ? "=关闭" : "=使能") + " 失败!未能获取DO当前状态,ErrorInfo:" + _mainStation.GetErrorInfo(ret);
                lbMsTips.Text = info;
                MessageBox.Show(info);
                return;
            }

            ret = _mainStation.SetFixDoSig(doType, !isCurrSigON);
            if (ret != 0)
            {
                string info = "设置DO:" + doType.ToString() + (isCurrSigON ? "=关闭" : "=使能") + " 失败!ErrorInfo:" + _mainStation.GetErrorInfo(ret);
                MessageBox.Show(info);
                lbMsTips.Text = info;
                return;
            }
            else
                lbMsTips.Text = "设置DO:" + doType.ToString() + " 信号 = " + (isCurrSigON ? "关闭" : "使能") + " 成功";


        }


        void OnDeclearDoClick(object sender, EventArgs e)
        {
            LampButton uc = sender as LampButton;
            string doName = uc.Text;           
            bool isCurrSigON = false;
            int ret = _mainStation.GetDoSig(doName, out isCurrSigON);
            if (ret != 0)
            {
                string info = "设置DO:" + doName + (isCurrSigON ? "=关闭" : "=使能") + " 失败!未能获取DO当前状态,ErrorInfo:" + _mainStation.GetErrorInfo(ret);
                lbMsTips.Text = info;
                MessageBox.Show(info);
                return;
            }

            ret = _mainStation.SetDoSig(doName, !isCurrSigON);
            if (ret != 0)
            {
                string info = "设置DO:" + doName + (isCurrSigON ? "=关闭" : "=使能") + " 失败!ErrorInfo:" + _mainStation.GetErrorInfo(ret);
                lbMsTips.Text = info;
                MessageBox.Show(info);
                return;
            }
            else
                lbMsTips.Text = "设置DO:" + doName + " 信号 = " + (isCurrSigON ? "关闭" : "使能") + " 成功";


        }



        /// <summary>
        /// 添加一个用户自定义的配置界面
        /// </summary>
        /// <param name="cfgUI"></param>
        public void AppendCfgUI(Control cfgUI)
        {
            if (null == cfgUI)
            {
                if (!_isUseCumstomUI)
                    return;
                tabControlCF1.TabPages.Remove(tpCustom);
                _isUseCumstomUI = false;
                return;
            }
            else
            {
                if (!tabControlCF1.TabPages.Contains(tpCustom))
                    tabControlCF1.TabPages.Insert(0, tpCustom);
                if (_isUseCumstomUI)
                    tpCustom.Controls.Clear();


                cfgUI.Dock = DockStyle.Fill;
                if (cfgUI is Form)
                {
                    (cfgUI as Form).FormBorderStyle = FormBorderStyle.None;
                    (cfgUI as Form).TopLevel = false;
                }
                cfgUI.Dock = DockStyle.Fill;
                tpCustom.Controls.Add(cfgUI);
                _isUseCumstomUI = true;
            }
        }

        public void UpdateUIByCfg()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateUIByCfg));
                return;
            }
            //添加代码
            UpdateMSCfg();
            UpdateSysCfg();
        }


        void UpdateMSCfg()
        {
            _mainStation._cfg.Load(); //重新载入
            string[] allDiChNames = AppHubCenter.Instance.MDCellNameMgr.AllDiNames();
            string[] allDoChNames = AppHubCenter.Instance.MDCellNameMgr.AllDoNames();



            int cnt = 0;
            //更新IO设置
            //FixedDI
            foreach (UcMsDioSetting uc in _lstFixedDI)
            {
                DioInfo diInfo = (DioInfo)_mainStation._cfg.GetItemValue(uc.DioName);
                uc.DioCheckBox.Checked = diInfo.Enabled;
                uc.DioCombo.Items.Clear();
                if (allDiChNames != null && allDiChNames.Length > 0)
                {
                    uc.DioCombo.BackColor = Color.White;
                    uc.DioCombo.Items.AddRange(allDiChNames);

                    if (string.IsNullOrEmpty(diInfo.GlobalChnName))
                    {
                        uc.DioCombo.SelectedIndex = -1;
                    }
                    else
                    {
                        if (allDiChNames.Contains(diInfo.GlobalChnName))
                        {
                            uc.DioCombo.SelectedItem = diInfo.GlobalChnName;
                            uc.DioCombo.ForeColor = Color.Black;
                        }
                        else
                        {
                            uc.DioCombo.Text = diInfo.GlobalChnName;
                            uc.DioCombo.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    uc.DioCombo.BackColor = Color.Black; //没有已被命名的全局通道
                }

                cnt++;
            }

            cnt = 0;
            //FixedDO
            foreach (UcMsDioSetting uc in _lstFixedDO)
            {
                DioInfo diInfo = (DioInfo)_mainStation._cfg.GetItemValue(uc.DioName);
                uc.DioCheckBox.Checked = diInfo.Enabled;
                uc.DioCombo.Items.Clear();
                if (allDoChNames != null && allDoChNames.Length > 0)
                {
                    uc.DioCombo.BackColor = Color.White;
                    uc.DioCombo.Items.AddRange(allDoChNames);

                    if (string.IsNullOrEmpty(diInfo.GlobalChnName))
                    {
                        uc.DioCombo.SelectedIndex = -1;
                    }
                    else
                    {
                        if (allDoChNames.Contains(diInfo.GlobalChnName))
                        {
                            uc.DioCombo.SelectedItem = diInfo.GlobalChnName;
                            uc.DioCombo.ForeColor = Color.Black;
                        }
                        else
                        {
                            uc.DioCombo.Text = diInfo.GlobalChnName;
                            uc.DioCombo.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    uc.DioCombo.BackColor = Color.Black; //没有已被命名的全局通道
                }

                cnt++;
            }



            cnt = 0;
            //更新IO设置
            //DeclearedDI
            DictionaryEx<string, DioInfo> declearedDIInfos = _mainStation._cfg.GetItemValue("DeclearedDI") as DictionaryEx<string, DioInfo>;
            foreach (UcMsDioSetting uc in _lstDeclearedDI)
            {
                DioInfo diInfo = declearedDIInfos[uc.DioName];
                uc.DioCheckBox.Checked = diInfo.Enabled;
                uc.DioCombo.Items.Clear();
                if (allDiChNames != null && allDiChNames.Length > 0)
                {
                    uc.DioCombo.BackColor = Color.White;
                    uc.DioCombo.Items.AddRange(allDiChNames);

                    if (string.IsNullOrEmpty(diInfo.GlobalChnName))
                    {
                        uc.DioCombo.SelectedIndex = -1;
                    }
                    else
                    {
                        if (allDiChNames.Contains(diInfo.GlobalChnName))
                        {
                            uc.DioCombo.SelectedItem = diInfo.GlobalChnName;
                            uc.DioCombo.ForeColor = Color.Black;
                        }
                        else
                        {
                            uc.DioCombo.Text = diInfo.GlobalChnName;
                            uc.DioCombo.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    uc.DioCombo.BackColor = Color.Black; //没有已被命名的全局通道
                }

                cnt++;
            }


            cnt = 0;
            //更新IO设置
            //DeclearedDO
            DictionaryEx<string, DioInfo> declearedDOInfos = _mainStation._cfg.GetItemValue("DeclearedDO") as DictionaryEx<string, DioInfo>;
            foreach (UcMsDioSetting uc in _lstDeclearedDO)
            {
                DioInfo diInfo = declearedDOInfos[uc.DioName];
                uc.DioCheckBox.Checked = diInfo.Enabled;
                uc.DioCombo.Items.Clear();
                if (allDoChNames != null && allDoChNames.Length > 0)
                {
                    uc.DioCombo.BackColor = Color.White;
                    uc.DioCombo.Items.AddRange(allDoChNames);

                    if (string.IsNullOrEmpty(diInfo.GlobalChnName))
                    {
                        uc.DioCombo.SelectedIndex = -1;
                    }
                    else
                    {
                        if (allDoChNames.Contains(diInfo.GlobalChnName))
                        {
                            uc.DioCombo.SelectedItem = diInfo.GlobalChnName;
                            uc.DioCombo.ForeColor = Color.Black;
                        }
                        else
                        {
                            uc.DioCombo.Text = diInfo.GlobalChnName;
                            uc.DioCombo.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    uc.DioCombo.BackColor = Color.Black; //没有已被命名的全局通道
                }

                cnt++;
            }


            //更新参数设置
            cnt = 0;
            string[] categotys = _mainStation.AllParamCategotys;

            if (null != categotys)
                foreach (string categoty in categotys)
                {
                    CosParams[] descs = _mainStation.GetParamDescribs(categoty);
                    if (null != descs)
                        foreach (CosParams desc in descs)
                        {
                            object val = _mainStation._cfg.GetItemValue(desc.pName);
                            _lstMsCfg[cnt].SetParamValue(val);
                            cnt++;
                        }

                }

        }


        /// <summary>
        /// 将当前系统配置参数值更新到界面上
        /// </summary>
        void UpdateSysCfg()
        {
            if (_mainStation == null)
                return;
            ///重新加载系统配置
            AppHubCenter.Instance.SystemCfg.Load();
            string[] categotys = _mainStation.AllSysCfgCategotys;
            int cnt = 0;
            if (null != categotys)
                foreach (string categoty in categotys)
                {
                    CosParams[] descs = _mainStation.GetSysCfgDescribs(categoty);
                    if (null == descs || descs.Length == 0)
                        continue;
                    foreach (CosParams desc in descs)
                    {
                        object val = AppHubCenter.Instance.SystemCfg.GetItemValue(desc.pName);
                        _lstSysCfg[cnt].SetParamValue(val);
                        cnt++;
                    }
                }
        }


        private void tabControlCF1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlCF1.SelectedTab == tpMSCfg)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;
        }

        private void tabControlCF1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (IsSysCfgEditting || IsParamEditting)
            {
                MessageBox.Show("当前页面正处于编辑状态，不能切换！");
                e.Cancel = true;
            }
        }



        bool IsParamEditting
        {
            get { return _isParamEditting; }
            set
            {
                _isParamEditting = value;
                btMSEditSave.Text = _isParamEditting ? "保存" : "编辑";
                btMSEditCancel.Enabled = _isParamEditting;
                foreach (UcMsDioSetting uc in _lstFixedDI)
                    uc.Enabled = _isParamEditting;
                foreach (UcMsDioSetting uc in _lstFixedDO)
                    uc.Enabled = _isParamEditting;
                foreach (UcMsDioSetting uc in _lstDeclearedDI)
                    uc.Enabled = _isParamEditting;
                foreach (UcMsDioSetting uc in _lstDeclearedDO)
                    uc.Enabled = _isParamEditting;

                foreach (UcParamEdit uc in _lstMsCfg)
                    uc.IsValueReadOnly = !_isParamEditting;


            }
        }


        bool IsSysCfgEditting
        {
            get { return _isSysCfgEditting; }
            set
            {
                _isSysCfgEditting = value;
                btSysEditSave.Text = _isSysCfgEditting ? "保存" : "编辑";
                btSysEditCancel.Enabled = _isSysCfgEditting;
                foreach (UcParamEdit uc in _lstSysCfg)
                    uc.IsValueReadOnly = !_isSysCfgEditting;

            }
        }


        /// <summary>
        /// 编辑/保存 主工站配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btMSEditSave_Click(object sender, EventArgs e)
        {
            if (!IsParamEditting)
                IsParamEditting = true;
            else //保存主工站参数
            {
                //保存配置项
                foreach (UcParamEdit uc in _lstMsCfg)
                {
                    object val = null;
                    if (!uc.GetParamValue(out val))
                    {
                        string info = "保存失败！请检查参数:\"" + uc.GetParamDesribe().pName + "\"的合法性";
                        lbMsTips.Text = info;
                        MessageBox.Show(info);
                        uc.Focus();
                        return;
                    }
                    _mainStation._cfg.SetItemValue(uc.GetParamDesribe().pName, val);
                }




                //保存固定DI配置
                foreach (UcMsDioSetting uc in _lstFixedDI)
                {
                    DioInfo info = new DioInfo();
                    info.GlobalChnName = uc.DioCombo.Text;
                    info.Enabled = uc.DioCheckBox.Checked;
                    _mainStation._cfg.SetItemValue(uc.DioName, info);
                }

                //保存固定DO
                foreach (UcMsDioSetting uc in _lstFixedDO)
                {
                    DioInfo info = new DioInfo();
                    info.GlobalChnName = uc.DioCombo.Text;
                    info.Enabled = uc.DioCheckBox.Checked;
                    _mainStation._cfg.SetItemValue(uc.DioName, info);
                }

                //保存声明的DI
                DictionaryEx<string, DioInfo> declearedDIInfos = _mainStation._cfg.GetItemValue("DeclearedDI") as DictionaryEx<string, DioInfo>;
                foreach (UcMsDioSetting uc in _lstDeclearedDI)
                {
                    DioInfo info = new DioInfo();
                    info.GlobalChnName = uc.DioCombo.Text;
                    info.Enabled = uc.DioCheckBox.Checked;
                    declearedDIInfos[uc.DioName] = info;
                }

                DictionaryEx<string, DioInfo> declearedDOInfos = _mainStation._cfg.GetItemValue("DeclearedDO") as DictionaryEx<string, DioInfo>;
                foreach (UcMsDioSetting uc in _lstDeclearedDO)
                {
                    DioInfo info = new DioInfo();
                    info.GlobalChnName = uc.DioCombo.Text;
                    info.Enabled = uc.DioCheckBox.Checked;
                    declearedDOInfos[uc.DioName] = info;
                }



                lbMsTips.Text = "参数保存完毕！";
                MessageBox.Show("参数保存完毕！");
                _mainStation._cfg.Save();
                _mainStation.UpdateWatchDioList();
                IsParamEditting = false;
            }
        }

        /// <summary>
        /// 取消编辑主工站配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btMSEditCancel_Click(object sender, EventArgs e)
        {
            UpdateMSCfg();
            lbMsTips.Text = "已取消参数编辑";
            IsParamEditting = false;
        }

        /// <summary>
        /// 编辑/保存 系统配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSysEditSave_Click(object sender, EventArgs e)
        {
            if (!IsSysCfgEditting)
                IsSysCfgEditting = true;
            else //保存系统配置参数
            {
                foreach (UcParamEdit uc in _lstSysCfg)
                {
                    object val = null;
                    if (!uc.GetParamValue(out val))
                    {
                        string info = "保存系统参数失败！请检查参数：\"" + uc.GetParamDesribe().pName + "\"合法性";
                        lbSysTips.Text = info;
                        MessageBox.Show(info);
                        uc.Focus();
                        return;
                    }
                    AppHubCenter.Instance.SystemCfg.SetItemValue(uc.GetParamDesribe().pName, val);
                }
                AppHubCenter.Instance.SystemCfg.Save();
                lbSysTips.Text = "系统参数保存完毕！";
                MessageBox.Show("系统参数保存完毕！");
                IsSysCfgEditting = false;

            }
        }
        /// <summary>
        /// 取消系统配置编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSysEditCancel_Click(object sender, EventArgs e)
        {
            UpdateSysCfg();
            lbSysTips.Text = "已取消系统参数编辑";
            IsSysCfgEditting = false;
        }


        /// <summary>
        /// 刷新IO状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (null == _mainStation || !Visible || tabControlCF1.SelectedTab != tpMSCfg)
            {
                timer1.Enabled = false;
                return;
            }
            int cnt = 0;
            Array arFixedDI = Enum.GetValues(typeof(FixedDI));
            foreach (int i in arFixedDI)
            {
                bool isSigON = false;
                int nRet = _mainStation.GetFixDiSig((FixedDI)i, out isSigON);
                if (nRet != 0)
                {
                    _lstFixedDI[cnt].DioLamp.BackColor = Color.Orange;
                    _lstFixedDI[cnt].DioLamp.LampColor = LampButton.LColor.Gray;
                }
                else
                {
                    _lstFixedDI[cnt].DioLamp.BackColor = SystemColors.Control;
                    _lstFixedDI[cnt].DioLamp.LampColor = isSigON ? LampButton.LColor.Green : LampButton.LColor.Gray;
                }
                cnt++;
            }


            cnt = 0;
            Array arFixedDO = Enum.GetValues(typeof(FixedDO));
            foreach (int i in arFixedDO)
            {
                bool isSigON = false;
                int nRet = _mainStation.GetFixDoSig((FixedDO)i, out isSigON);
                if (nRet != 0)
                {
                    _lstFixedDO[cnt].DioLamp.BackColor = Color.Orange;
                    _lstFixedDO[cnt].DioLamp.LampColor = LampButton.LColor.Gray;
                }
                else
                {
                    _lstFixedDO[cnt].DioLamp.BackColor = SystemColors.Control;
                    _lstFixedDO[cnt].DioLamp.LampColor = isSigON ? LampButton.LColor.Green : LampButton.LColor.Gray;
                }
                cnt++;
            }

            cnt = 0;
            foreach (UcMsDioSetting uc in _lstDeclearedDI)
            {
                string ioName = uc.DioName;
                bool isSigON = false;
                int nRet = _mainStation.GetDiSig(ioName, out isSigON);
                if (nRet != 0)
                {
                    uc.DioLamp.BackColor = Color.Orange;
                    uc.DioLamp.LampColor = LampButton.LColor.Gray;
                }
                else
                {
                    uc.DioLamp.BackColor = SystemColors.Control;
                    uc.DioLamp.LampColor = isSigON ? LampButton.LColor.Green : LampButton.LColor.Gray;
                }
                cnt++;
            }



            cnt = 0;
            foreach (UcMsDioSetting uc in _lstDeclearedDO)
            {
                string ioName = uc.DioName;
                bool isSigON = false;
                int nRet = _mainStation.GetDoSig(ioName, out isSigON);
                if (nRet != 0)
                {
                    uc.DioLamp.BackColor = Color.Orange;
                    uc.DioLamp.LampColor = LampButton.LColor.Gray;
                }
                else
                {
                    uc.DioLamp.BackColor = SystemColors.Control;
                    uc.DioLamp.LampColor = isSigON ? LampButton.LColor.Green : LampButton.LColor.Gray;
                }
                cnt++;
            }


        }

        private void tabControlCF1_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                timer1.Enabled = false;
            }
            else
            {
                if (tabControlCF1.SelectedTab == tpMSCfg)
                    timer1.Enabled = true;
            }
        }
    }
}
