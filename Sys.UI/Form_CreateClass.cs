using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using Cell.UI;
using Org.IBarcode;
using Org.ICamera;
using Org.ILineScan;
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

namespace Sys.UI
{
    public partial class Form_CreateClass : windowBase
    {
        string _initorCaption = "设备";
        public Form_CreateClass()
        {
            InitializeComponent();
        }

        Type _initorType = typeof(IPlatDevice);
        public Type InitorType
        {
            get { return _initorType; }
            set
            {
                _initorType = value;
            }
        }

        public string InitorCaption
        {
            get { return _initorCaption; }
            set
            {
                _initorCaption = value;
                Text = _initorCaption + "模块管理";
                labelID.Text = _initorCaption + "ID";
                dgv_IPlatDev.Columns[0].HeaderText = _initorCaption + "ID";
                dgv_IPlatDev.Columns[1].HeaderText = _initorCaption + "类型";
                btAdd.Text = "添加新" + _initorCaption;
                btRemove.Text = "移除所选" + _initorCaption;
                btDebug.Text = "调试所选" + _initorCaption;
                btCfg.Text = "编辑" + _initorCaption + "配置";
            }
        }

        private void Form_CreateDevice_Load(object sender, EventArgs e)
        {
            dgv_IPlatDev.Rows.Clear();
            string[] devIDs = AppHubCenter.Instance.InitorManager.GetIDs(InitorType);
            foreach (string devID in devIDs)
            {
                IPlatInitializable dev = AppHubCenter.Instance.InitorManager[devID];
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell cellID = new DataGridViewTextBoxCell();
                cellID.Value = devID;
                row.Cells.Add(cellID);

                DataGridViewTextBoxCell cellModel = new DataGridViewTextBoxCell();
                cellModel.Value = AppIplatinitHelper.DispalyTypeName(dev.GetType());
                row.Cells.Add(cellModel);
                DataGridViewTextBoxCell cellType = new DataGridViewTextBoxCell();
                cellType.Value = dev.GetType().Name;
                row.Cells.Add(cellType);
                dgv_IPlatDev.Rows.Add(row);
                if (!dev.IsInitOK)
                    row.DefaultCellStyle.ForeColor = Color.Red;
            }

            dgv_IPlatDev.ClearSelection();
        }

        /// <summary>
        /// 添加新设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, EventArgs e)
        {
            Form_CreateInitor fm = new Form_CreateInitor();
            fm.Text = "创建" + InitorCaption + "对象";
            fm.MatchType = InitorType;
            if (DialogResult.OK == fm.ShowDialog())
            {
                IPlatInitializable newDevice = fm.Initor;
                string devID = fm.ID;
                AppHubCenter.Instance.InitorManager.Add(devID, newDevice);

                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell cellID = new DataGridViewTextBoxCell();
                cellID.Value = devID;
                row.Cells.Add(cellID);

                DataGridViewTextBoxCell cellModel = new DataGridViewTextBoxCell();
                cellModel.Value = AppIplatinitHelper.DispalyTypeName(newDevice.GetType());
                row.Cells.Add(cellModel);
                DataGridViewTextBoxCell cellType = new DataGridViewTextBoxCell();
                cellType.Value = newDevice.GetType().Name;
                row.Cells.Add(cellType);
                dgv_IPlatDev.Rows.Add(row);
                newDevice.Initialize();
                if (!newDevice.IsInitOK)
                {
                    btInit.Enabled = true;
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                    btInit.Enabled = false;



                /// 更新参数到界面
                dgv_IPlatDev.Rows[dgv_IPlatDev.Rows.Count - 1].Selected = true;
                RemoveAllPEs();
                string[] iniParamNames = newDevice.InitParamNames;
                if (null == iniParamNames)
                {
                    btEditSave.Enabled = false;
                    return;
                }
                btEditSave.Enabled = true;
                int locY = btInit.Location.Y + btInit.Size.Height + 5;
                foreach (string ipName in iniParamNames)
                {
                    UcParamEdit pe = new UcParamEdit();
                    pe.EventValueChanged += OnParamEditValueChanged;
                    pe.Width = gbParams.Width - 1;
                    pe.Location = new Point(4, locY);
                    locY += pe.Height;
                    pe.IsValueReadOnly = true;
                    gbParams.Controls.Add(pe);
                    pe.SetParamDesribe(newDevice.GetInitParamDescribe(ipName));
                    pe.SetParamValue(newDevice.GetInitParamValue(ipName));
                }

                Type devType = newDevice.GetType();
                if (typeof(IPlatDevice_Camera).IsAssignableFrom(devType) ||
                    typeof(IPlatRealtimeUIProvider).IsAssignableFrom(devType)) //提供调试界面
                {
                    btDebug.Enabled = true;
                    if (typeof(IPlatRealtimeUIProvider).IsAssignableFrom(devType))
                        chkSelfUI.Enabled = true;
                    else
                    {
                        chkSelfUI.Checked = false;
                        chkSelfUI.Enabled = false;
                    }
                }
                else
                    btDebug.Enabled = false;

                if (typeof(IPlatCfgUIProvider).IsAssignableFrom(devType))
                    btCfg.Enabled = true;
                else
                    btCfg.Enabled = false;
            }
        }

        /// <summary>
        /// 移除其他活动控件
        /// </summary>
        void RemoveAllPEs()
        {
            while (gbParams.Controls.Count > 5)
                gbParams.Controls.RemoveAt(5);
        }

        /// <summary>
        /// 当其中的一个初始化参数发生变化时，可能需要修改参数列表
        /// </summary>
        /// <param name="describe"></param>
        /// <param name="newValue"></param>
        void OnParamEditValueChanged(UcParamEdit sender, CosParams describe, object newValue)
        {
            DataGridViewRow row = dgv_IPlatDev.SelectedRows[0];
            IPlatInitializable dev = AppHubCenter.Instance.InitorManager[row.Cells[0].Value.ToString()];

            object value = Convert.ChangeType(newValue, Type.GetType(describe.ptype));
            if (!dev.SetInitParamValue(describe.pName, value))
            {
                MessageBox.Show("设置参数项：\"" + describe.pName + "\" Value = \"" + value.ToString() + "\"失败！");
                return;
            }
            tbDevID.Text = row.Cells[0].Value.ToString();
            btInit.Enabled = !dev.IsInitOK;

            //**  需要回调的都是 通过回调， 如果不需要回调的 则需要主动修改自动参数
            //  退出
            if (!describe.pboolCallBack)
            {
                sender.SetParamValue(value);
                return;
            }

            RemoveAllPEs();

            string[] iniParamNames = dev.InitParamNames;
            if (null == iniParamNames)
            {
                btEditSave.Enabled = false;
                return;
            }
            btEditSave.Enabled = true;
            int locY = btInit.Location.Y + btInit.Size.Height + 5;
            foreach (string ipName in iniParamNames)
            {
                UcParamEdit pe = new UcParamEdit();
                pe.EventValueChanged += OnParamEditValueChanged;
                pe.Width = gbParams.Width - 1;
                pe.Location = new Point(4, locY);
                locY += pe.Height;
                pe.IsValueReadOnly = false;
                gbParams.Controls.Add(pe);

                pe.SetParamDesribe(dev.GetInitParamDescribe(ipName));

                pe.SetParamValue(dev.GetInitParamValue(ipName));
            }
        }

        private void dgv_IPlatDev_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv_IPlatDev.CurrentCell == null || e.RowIndex < 0)
                return;

            btRemove.Enabled = true;
            RemoveAllPEs();

            DataGridViewRow row = dgv_IPlatDev.SelectedRows[0];
            IPlatInitializable dev = AppHubCenter.Instance.InitorManager[row.Cells[0].Value.ToString()];
            Type devType = dev.GetType();
            if (typeof(IPlatDevice_Camera).IsAssignableFrom(devType)
                || typeof(IPlatDevice_Barcode).IsAssignableFrom(devType)
                || typeof(IPlatDevice_LineScan).IsAssignableFrom(devType)
                 || typeof(IPlatStation).IsAssignableFrom(devType)) //提供调试界面
            {
                btDebug.Enabled = true;
                if (typeof(IPlatRealtimeUIProvider).IsAssignableFrom(devType))
                    chkSelfUI.Enabled = true;
                else
                {
                    chkSelfUI.Checked = false;
                    chkSelfUI.Enabled = false;
                }
            }
            else
                btDebug.Enabled = false;

            if (typeof(IPlatCfgUIProvider).IsAssignableFrom(devType))
                btCfg.Enabled = true;
            else
                btCfg.Enabled = false;


            tbDevID.Text = row.Cells[0].Value.ToString();
            btInit.Enabled = !dev.IsInitOK;
            string[] iniParamNames = dev.InitParamNames;
            if (null == iniParamNames)
            {
                btEditSave.Enabled = false;
                return;
            }
            btEditSave.Enabled = true;
            int locY = btInit.Location.Y + btInit.Size.Height + 5;
            foreach (string ipName in iniParamNames)
            {
                UcParamEdit pe = new UcParamEdit();
                pe.EventValueChanged += OnParamEditValueChanged;
                pe.Width = gbParams.Width - 1;
                pe.Location = new Point(4, locY);
                locY += pe.Height + 5;
                pe.IsValueReadOnly = true;
                gbParams.Controls.Add(pe);

                //** 因为 UcParamEdit 创建不成功则不会显示数值，所以顺序很重要
                pe.SetParamDesribe(dev.GetInitParamDescribe(ipName));
                pe.SetParamValue(dev.GetInitParamValue(ipName));
            }
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            if (dgv_IPlatDev.SelectedRows == null || 0 == dgv_IPlatDev.SelectedRows.Count)
            {
                MessageBox.Show("请先选择需要移除的" + _initorCaption);
                return;
            }

            DataGridViewRow row = dgv_IPlatDev.SelectedRows[0];

            if (DialogResult.Cancel == MessageBox.Show("确定要移除:" + row.Cells[0].Value.ToString() + "?", "移除设备警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                return;
            AppHubCenter.Instance.InitorManager.Remove(row.Cells[0].Value.ToString()); //从设备管理器中删除
            DictionaryEx<string, List<object>> devCfg = AppHubCenter.Instance.SystemCfg.GetItemValue(AppHubCenter.CK_InitDevParams) as DictionaryEx<string, List<object>>;
            devCfg.Remove(row.Cells[0].Value.ToString());//从设备配置文件中删除
            AppHubCenter.Instance.SystemCfg.NotifyItemChanged(AppHubCenter.CK_InitDevParams);
            AppHubCenter.Instance.SystemCfg.Save();
            MessageBox.Show(_initorCaption + row.Cells[0].Value.ToString() + "已从系统中移除！");
            dgv_IPlatDev.Rows.Remove(row);
            RemoveAllPEs();
            dgv_IPlatDev.ClearSelection();
        }

        private void btDebug_Click(object sender, EventArgs e)
        {
            if (null == dgv_IPlatDev.SelectedRows || 0 == dgv_IPlatDev.SelectedRows.Count)
            {
                MessageBox.Show("请先选择需要调试的" + InitorCaption + "对象");
                return;
            }
            IPlatInitializable initor = AppHubCenter.Instance.InitorManager[dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString()];
            Type devType = initor.GetType();
            if (chkSelfUI.Checked)
            {
                if (!typeof(IPlatRealtimeUIProvider).IsAssignableFrom(devType))
                {
                    MessageBox.Show(InitorCaption + "对象未提供自带调试界面，请取消\"自带界面\"后重试！");
                    return;
                }

            }
            else
            {
                if (typeof(IPlatDevice_Camera).IsAssignableFrom(devType)
                   || typeof(IPlatDevice_Barcode).IsAssignableFrom(devType)
                   || typeof(IPlatDevice_LineScan).IsAssignableFrom(devType)
                    || typeof(IPlatStation).IsAssignableFrom(devType))
                {
                    Form_ShowRealTimeUI fm = new Form_ShowRealTimeUI();
                    fm.Text = dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString() + " Debug" + "待实现！！";
                    if (typeof(IPlatDevice_Camera).IsAssignableFrom(devType))
                    {
                        fm.Text = dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString() + "调试窗口";
                        IPlatRealtimeUIProvider platDevice_LineScan = (initor as IPlatRealtimeUIProvider);
                        fm.SetRTUI(platDevice_LineScan.GetRealtimeUI());
                    }
                    else if (typeof(IPlatDevice_Barcode).IsAssignableFrom(devType))
                    {
                        fm.Text = dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString() + "调试窗口";
                        UcBarcodeScan uc = new UcBarcodeScan();
                        uc.SetDevice(initor as IPlatDevice_Barcode);
                        fm.SetRTUI(uc);
                    }
                    else if (typeof(IPlatDevice_LineScan).IsAssignableFrom(devType))
                    {
                        fm.Text = dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString() + "调试窗口";
                        IPlatRealtimeUIProvider platDevice_LineScan = (initor as IPlatRealtimeUIProvider);
                        fm.SetRTUI(platDevice_LineScan.GetRealtimeUI());
                    }
                    else if (typeof(IPlatStation).IsAssignableFrom(devType))
                    {
                        fm.Text = dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString() + "调试窗口";

                        UcStationRealTimeUIDebug ucStation = new UcStationRealTimeUIDebug();
                        ucStation.SetStation(initor as IPlatStation);
                        fm.SetRTUI(ucStation);
                    }
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("无法为" + InitorCaption + "类型的对象提供调试界面");
                    return;
                }

            }
        }

        private void btCfg_Click(object sender, EventArgs e)
        {
            if (dgv_IPlatDev.SelectedRows == null || dgv_IPlatDev.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要设置的" + InitorCaption + "对象！");
                return;
            }

            IPlatInitializable initor = AppHubCenter.Instance.InitorManager[dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString()];
            Type devType = initor.GetType();
            if (!typeof(IPlatCfgUIProvider).IsAssignableFrom(devType))
            {
                MessageBox.Show(InitorCaption + "对象未提供参数配置界面！");
                return;
            }

                (initor as IPlatCfgUIProvider).ShowCfgDialog();
        }

        private void btInit_Click(object sender, EventArgs e)
        {
            IPlatInitializable initor = AppHubCenter.Instance.InitorManager[dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString()];
            if (!initor.Initialize())
                MessageBox.Show("初始化失败:" + initor.GetInitErrorInfo());
        }


        bool isEditting; ///是否处于编辑初始化参数状态
        private void btEditSave_Click(object sender, EventArgs e)
        {
            if (!isEditting)//参数未编辑状态
            {
                if (dgv_IPlatDev.SelectedRows == null || 0 == dgv_IPlatDev.SelectedRows.Count)
                {
                    MessageBox.Show("请选择需要编辑参数的对象");
                    return;
                }

                if (gbParams.Controls.Count <= 5) //没有初始化参数
                {
                    return;
                }

                for (int i = 5; i < gbParams.Controls.Count; i++)
                {
                    UcParamEdit pe = gbParams.Controls[i] as UcParamEdit;
                    pe.IsValueReadOnly = false;
                }

                dgv_IPlatDev.Enabled = false;
                btEditSave.Text = "保存";
                btCancel.Enabled = true;
                btInit.Enabled = false;
                btAdd.Enabled = false;
                btRemove.Enabled = false;
                btDebug.Enabled = false;
                isEditting = true;
            }
            else //编辑完成，需要存储编辑后的参数
            {
                IPlatInitializable dev = AppHubCenter.Instance.InitorManager[dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString()];
                List<object> initParams = new List<object>();
                initParams.Add(dev.GetType().AssemblyQualifiedName);//DevType
                for (int i = 5; i < gbParams.Controls.Count; i++)
                {
                    object paramVal = null;
                    UcParamEdit pe = gbParams.Controls[i] as UcParamEdit;
                    pe.GetParamValue(out paramVal);
                    //  paramVal = dev.GetInitParamValue(dev.InitParamNames[i - 5]);
                    CosParams prDes = dev.GetInitParamDescribe(dev.InitParamNames[i - 5]);
                    object value = Convert.ChangeType(paramVal, Type.GetType(prDes.ptype));

                    if (!dev.SetInitParamValue(dev.InitParamNames[i - 5], value))
                    {
                        pe.Focus();
                        MessageBox.Show("设置参数\"" + dev.InitParamNames[i - 5] + "\"失败：" + dev.GetInitErrorInfo());
                    }
                    initParams.Add(paramVal);
                }
                for (int i = 5; i < gbParams.Controls.Count; i++)
                {
                    UcParamEdit pe = gbParams.Controls[i] as UcParamEdit;
                    pe.IsValueReadOnly = true;
                }
                if (!dev.Initialize())
                {
                    MessageBox.Show("用当前参数初始化设备失败:" + dev.GetInitErrorInfo());
                }
                DictionaryEx<string, List<object>> devCfg = AppHubCenter.Instance.SystemCfg.GetItemValue(AppHubCenter.CK_InitDevParams) as DictionaryEx<string, List<object>>;
                devCfg[dgv_IPlatDev.SelectedRows[0].Cells[0].Value.ToString()] = initParams;
                AppHubCenter.Instance.SystemCfg.NotifyItemChanged(AppHubCenter.CK_InitDevParams);
                AppHubCenter.Instance.SystemCfg.Save();

                btInit.Enabled = true;
                btAdd.Enabled = true;
                btRemove.Enabled = true;

                isEditting = false;
                btEditSave.Text = "编辑参数";
                btCancel.Enabled = false;
                dgv_IPlatDev.Enabled = true;

                Type devType = dev.GetType();
                if (typeof(IPlatDevice_Camera).IsAssignableFrom(devType) ||
                    typeof(IPlatDevice_Barcode).IsAssignableFrom(devType))
                {
                    btDebug.Enabled = true;
                    if (typeof(IPlatRealtimeUIProvider).IsAssignableFrom(devType))
                        chkSelfUI.Enabled = true;
                    else
                    {
                        chkSelfUI.Checked = false;
                        chkSelfUI.Enabled = false;
                    }
                }
                else
                    btDebug.Enabled = false;

                if (typeof(IPlatRealtimeUIProvider).IsAssignableFrom(devType))
                    btCfg.Enabled = true;
                else
                    btCfg.Enabled = false;
            }
        }
    }
}
