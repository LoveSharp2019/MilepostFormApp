﻿using Cell.DataModel;
using Cell.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys.IStations
{
    /// <summary>
    /// 用于编辑工站声明的设备通道和全局通道绑定关系的界面
    /// </summary>
    public partial class UcStationDevChnNameMapping : UserControl
    {
        public UcStationDevChnNameMapping()
        {
            InitializeComponent();

        }

        IStationBase _station = null;
        bool _isFormLoaded = false;

        private void UcLocDevChnNameMapping_Load(object sender, EventArgs e)
        {
            _isFormLoaded = true;
            if (Parent != null)
                Parent.VisibleChanged += new System.EventHandler(this.UcStationDevChnNameMapping_VisibleChanged);//将当前的VisibalChange绑定到父控件上

            AdjustStationView();

        }


        public void SetStation(IStationBase station)
        {
            _station = station;
            if (_isFormLoaded)
                AdjustStationView();
        }

        /// <summary>
        /// 根据Station中声明的设备通道变量布局界面
        /// </summary>
        void AdjustStationView()
        {
            tabControlCF1.TabPages.Clear();
            _isEditting = false;
            btEditCancel.Enabled = false;
            btEditSave.Text = "编辑";
            if (null == _station)
            {
                lbStationName.Text = "未设置";
                btEditSave.Enabled = false;
                return;
            }
            lbStationName.Text = _station.Name;
            btEditSave.Enabled = true;
            DictionaryEx<NamedChnType, List<List<string>>> devChns = _station.DeclearedDevChnMapping;
            ///工站声明的轴
            if (devChns.ContainsKey(NamedChnType.Axis))
            {
                List<List<string>> chnMapping = devChns[NamedChnType.Axis];
                if (null != chnMapping && chnMapping.Count > 0)
                {
                    TabPage tp = new TabPage("Axis");
                    tp.Tag = NamedChnType.Axis;
                    tabControlCF1.TabPages.Add(tp);
                    tp.AutoScroll = true;

                    DataGridView dgv = new DataGridView();

                    dgv.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
                    dgv.Columns.Add(new DataGridViewTextBoxColumn());
                    dgv.Columns.Add(new DataGridViewComboBoxColumn());
                    dgv.Columns[0].HeaderText = "轴名/站内";
                    dgv.Columns[0].Width = 200;
                    dgv.Columns[1].HeaderText = "全局标识名";
                    dgv.Columns[1].Width = 300;
                    dgv.Dock = DockStyle.Fill;
                    dgv.ReadOnly = true;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.RowHeadersVisible = false;
                    tp.Controls.Add(dgv);
                    foreach (List<string> locAndGlobName in chnMapping)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewTextBoxCell cellLocName = new DataGridViewTextBoxCell();
                        cellLocName.Value = locAndGlobName[0];
                        DataGridViewComboBoxCell cellGlobName = new DataGridViewComboBoxCell();
                        if (locAndGlobName.Count > 1)
                            cellGlobName.Value = locAndGlobName[1];                        
                        row.Cells.Add(cellLocName);
                        row.Cells.Add(cellGlobName);
                        dgv.Rows.Add(row);
                    }
                }

            }

            ///工站声明的DI
            if (devChns.ContainsKey(NamedChnType.Di))
            {
                List<List<string>> chnMapping = devChns[NamedChnType.Di];
                if (null != chnMapping && chnMapping.Count > 0)
                {
                    TabPage tp = new TabPage("DI");
                    tp.Tag = NamedChnType.Di;
                    tabControlCF1.TabPages.Add(tp);
                    tp.AutoScroll = true;

                    DataGridView dgv = new DataGridView();
                    dgv.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
                    dgv.Columns.Add(new DataGridViewTextBoxColumn());
                    dgv.Columns.Add(new DataGridViewComboBoxColumn());
                    dgv.Columns[0].HeaderText = "DI名/站内";
                    dgv.Columns[0].Width = 200;
                    dgv.Columns[0].ReadOnly = true;
                    dgv.Columns[1].HeaderText = "全局标识名";
                    dgv.Columns[1].Width = 300;
                    dgv.Columns[1].ReadOnly = true;
                    dgv.Dock = DockStyle.Fill;
                    dgv.ReadOnly = true;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.RowHeadersVisible = false;
                    tp.Controls.Add(dgv);
                    foreach (List<string> locAndGlobName in chnMapping)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewTextBoxCell cellLocName = new DataGridViewTextBoxCell();
                        cellLocName.Value = locAndGlobName[0];
                        DataGridViewComboBoxCell cellGlobName = new DataGridViewComboBoxCell();
                        if (locAndGlobName.Count > 1)
                            cellGlobName.Value = locAndGlobName[1];
                        //cellGlobName.Items.Add()
                        row.Cells.Add(cellLocName);
                        row.Cells.Add(cellGlobName);
                        dgv.Rows.Add(row);
                    }
                }
            }

            ///工站声明的DO
            if (devChns.ContainsKey(NamedChnType.Do))
            {
                List<List<string>> chnMapping = devChns[NamedChnType.Do];
                if (null != chnMapping && chnMapping.Count > 0)
                {
                    TabPage tp = new TabPage("DO");
                    tp.Tag = NamedChnType.Do;
                    tabControlCF1.TabPages.Add(tp);
                    tp.AutoScroll = true;

                    DataGridView dgv = new DataGridView();
                    dgv.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
                    dgv.Columns.Add(new DataGridViewTextBoxColumn());
                    dgv.Columns.Add(new DataGridViewComboBoxColumn());
                    dgv.Columns[0].HeaderText = "DO名/站内";
                    dgv.Columns[0].Width = 200;
                    dgv.Columns[0].ReadOnly = true;
                    dgv.Columns[1].HeaderText = "全局标识名";
                    dgv.Columns[1].Width = 300;
                    dgv.Columns[1].ReadOnly = true;
                    dgv.Dock = DockStyle.Fill;
                    dgv.ReadOnly = true;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.RowHeadersVisible = false;
                    tp.Controls.Add(dgv);
                    foreach (List<string> locAndGlobName in chnMapping)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewTextBoxCell cellLocName = new DataGridViewTextBoxCell();
                        cellLocName.Value = locAndGlobName[0];
                        DataGridViewComboBoxCell cellGlobName = new DataGridViewComboBoxCell();
                        if (locAndGlobName.Count > 1)
                            cellGlobName.Value = locAndGlobName[1];                      
                        row.Cells.Add(cellLocName);
                        row.Cells.Add(cellGlobName);
                        dgv.Rows.Add(row);
                    }
                }
            }

            ///工站声明的Cmr
            if (devChns.ContainsKey(NamedChnType.Camera))
            {
                List<List<string>> cmrMapping = devChns[NamedChnType.Camera];
                if (null != cmrMapping && cmrMapping.Count > 0)
                {
                    TabPage tp = new TabPage("相机");
                    tp.Tag = NamedChnType.Camera;
                    tabControlCF1.TabPages.Add(tp);
                    tp.AutoScroll = true;

                    DataGridView dgv = new DataGridView();
                    dgv.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
                    dgv.Columns.Add(new DataGridViewTextBoxColumn());
                    dgv.Columns.Add(new DataGridViewComboBoxColumn());
                    dgv.Columns[0].HeaderText = "相机名/站内";
                    dgv.Columns[0].Width = 200;
                    dgv.Columns[0].ReadOnly = true;
                    dgv.Columns[1].HeaderText = "全局标识名";
                    dgv.Columns[1].Width = 300;
                    dgv.Columns[1].ReadOnly = true;
                    dgv.Dock = DockStyle.Fill;
                    dgv.ReadOnly = true;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.RowHeadersVisible = false;
                    tp.Controls.Add(dgv);
                    foreach (List<string> cmrLocGlobName in cmrMapping)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewTextBoxCell cellLocName = new DataGridViewTextBoxCell();
                        cellLocName.Value = cmrLocGlobName[0];
                        DataGridViewComboBoxCell cellGlobName = new DataGridViewComboBoxCell();
                        if (cmrLocGlobName.Count > 1)
                            cellGlobName.Value = cmrLocGlobName[1];                      
                        row.Cells.Add(cellLocName);
                        row.Cells.Add(cellGlobName);
                        dgv.Rows.Add(row);
                    }
                }

            }
            ///工站声明的Cmr
            if (devChns.ContainsKey(NamedChnType.LineScan))
            {
                List<List<string>> cmrMapping = devChns[NamedChnType.LineScan];
                if (null != cmrMapping && cmrMapping.Count > 0)
                {
                    TabPage tp = new TabPage("线扫激光");
                    tp.Tag = NamedChnType.LineScan;
                    tabControlCF1.TabPages.Add(tp);
                    tp.AutoScroll = true;

                    DataGridView dgv = new DataGridView();
                    dgv.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
                    dgv.Columns.Add(new DataGridViewTextBoxColumn());
                    dgv.Columns.Add(new DataGridViewComboBoxColumn());
                    dgv.Columns[0].HeaderText = "激光名/站内";
                    dgv.Columns[0].Width = 200;
                    dgv.Columns[0].ReadOnly = true;
                    dgv.Columns[1].HeaderText = "全局标识名";
                    dgv.Columns[1].Width = 300;
                    dgv.Columns[1].ReadOnly = true;
                    dgv.Dock = DockStyle.Fill;
                    dgv.ReadOnly = true;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.RowHeadersVisible = false;
                    tp.Controls.Add(dgv);
                    foreach (List<string> cmrLocGlobName in cmrMapping)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewTextBoxCell cellLocName = new DataGridViewTextBoxCell();
                        cellLocName.Value = cmrLocGlobName[0];
                        DataGridViewComboBoxCell cellGlobName = new DataGridViewComboBoxCell();
                        if (cmrLocGlobName.Count > 1)
                            cellGlobName.Value = cmrLocGlobName[1];
                        row.Cells.Add(cellLocName);
                        row.Cells.Add(cellGlobName);
                        dgv.Rows.Add(row);
                    }
                }

            }
            ///工站声明的AI
            if (devChns.ContainsKey(NamedChnType.Ai))
            {
                List<List<string>> chnMapping = devChns[NamedChnType.Ai];
                if (null != chnMapping && chnMapping.Count > 0)
                {
                    TabPage tp = new TabPage("Ai");
                    tp.Tag = NamedChnType.Ai;
                    tabControlCF1.TabPages.Add(tp);
                    tp.AutoScroll = true;

                    DataGridView dgv = new DataGridView();
                    dgv.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
                    dgv.Columns.Add(new DataGridViewTextBoxColumn());
                    dgv.Columns.Add(new DataGridViewComboBoxColumn());
                    dgv.Columns[0].HeaderText = "Ai名/站内";
                    dgv.Columns[0].Width = 200;
                    dgv.Columns[0].ReadOnly = true;
                    dgv.Columns[1].HeaderText = "全局标识名";
                    dgv.Columns[1].Width = 300;
                    dgv.Columns[1].ReadOnly = true;
                    dgv.Dock = DockStyle.Fill;
                    dgv.ReadOnly = true;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.RowHeadersVisible = false;
                    tp.Controls.Add(dgv);
                    foreach (List<string> locAndGlobName in chnMapping)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewTextBoxCell cellLocName = new DataGridViewTextBoxCell();
                        cellLocName.Value = locAndGlobName[0];
                        DataGridViewComboBoxCell cellGlobName = new DataGridViewComboBoxCell();
                        if (chnMapping.Count > 1)
                            cellGlobName.Value = locAndGlobName[1];
                        //cellGlobName.Items.Add()
                        row.Cells.Add(cellLocName);
                        row.Cells.Add(cellGlobName);
                        dgv.Rows.Add(row);
                    }
                }
            }
            ///工站声明的AO
            if (devChns.ContainsKey(NamedChnType.Ao))
            {
                List<List<string>> chnMapping = devChns[NamedChnType.Ao];
                if (null != chnMapping && chnMapping.Count > 0)
                {
                    TabPage tp = new TabPage("Ao");
                    tp.Tag = NamedChnType.Ao;
                    tabControlCF1.TabPages.Add(tp);
                    tp.AutoScroll = true;

                    DataGridView dgv = new DataGridView();
                    dgv.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
                    dgv.Columns.Add(new DataGridViewTextBoxColumn());
                    dgv.Columns.Add(new DataGridViewComboBoxColumn());
                    dgv.Columns[0].HeaderText = "Ao名/站内";
                    dgv.Columns[0].Width = 200;
                    dgv.Columns[0].ReadOnly = true;
                    dgv.Columns[1].HeaderText = "全局标识名";
                    dgv.Columns[1].Width = 300;
                    dgv.Columns[1].ReadOnly = true;
                    dgv.Dock = DockStyle.Fill;
                    dgv.ReadOnly = true;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.RowHeadersVisible = false;
                    tp.Controls.Add(dgv);
                    foreach (List<string> locAndGlobName in chnMapping)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewTextBoxCell cellLocName = new DataGridViewTextBoxCell();
                        cellLocName.Value = chnMapping[0];
                        DataGridViewComboBoxCell cellGlobName = new DataGridViewComboBoxCell();
                        if (locAndGlobName.Count > 1)
                            cellGlobName.Value = locAndGlobName[1];
                        //cellGlobName.Items.Add()
                        row.Cells.Add(cellLocName);
                        row.Cells.Add(cellGlobName);
                        dgv.Rows.Add(row);
                    }
                }
            }

            LoadStation();
        }


        /// <summary>
        /// 将Station的名称映射表加载到界面上
        /// </summary>
        void LoadStation()
        {
            try
            {
                if (_station == null)
                    return;
                foreach (TabPage tp in tabControlCF1.TabPages)
                {
                    DataGridView dgv = tp.Controls[0] as DataGridView;
                    NamedChnType chnType = (NamedChnType)tp.Tag;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        List<List<string>> locAndGlobMaps = null;
                        string[] allGlobChnNamesInStation = null; //所有可供绑定的全局通道名称
                        switch (chnType)
                        {
                            case NamedChnType.Di://数字输入
                                locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Di];
                                allGlobChnNamesInStation = _station.DINames;
                                break;
                            case NamedChnType.Do://
                                locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Do];
                                allGlobChnNamesInStation = _station.DONames;
                                break;
                            case NamedChnType.Axis://
                                locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Axis];
                                allGlobChnNamesInStation = _station.AxisNames;
                                break;                           
                            case NamedChnType.Ai://
                                locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Ai];
                                allGlobChnNamesInStation = _station.AINames;
                                break;
                            case NamedChnType.Ao://
                                locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Ao];
                                allGlobChnNamesInStation = _station.AONames;
                                break;
                            case NamedChnType.Camera:
                                locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Camera];
                                allGlobChnNamesInStation = _station.CameraNames;
                                break;
                            case NamedChnType.LineScan:
                                locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.LineScan];
                                allGlobChnNamesInStation = _station.LineScanNames;
                                break;                      
                            default:
                                continue;
                        }
                        string chnLocName = row.Cells[0].Value as string;//站内通道名称
                        string currGlobName = null;//当前绑定的全局通道名
                        foreach (List<string> kv in locAndGlobMaps)
                            if (kv[0] == chnLocName)
                                currGlobName = kv[1];
                        DataGridViewComboBoxCell cellGlobNames = row.Cells[1] as DataGridViewComboBoxCell;
                        cellGlobNames.Items.Clear();
                        if (allGlobChnNamesInStation != null)
                            foreach (string s in allGlobChnNamesInStation)
                                cellGlobNames.Items.Add(s);
                        if (!string.IsNullOrEmpty(currGlobName))
                        {
                            cellGlobNames.Value = currGlobName;
                            if (allGlobChnNamesInStation == null || !allGlobChnNamesInStation.Contains(currGlobName))
                                cellGlobNames.Style.ForeColor = Color.Red;
                            else
                                cellGlobNames.Style.ForeColor = Color.Black;
                        }
                        else
                            cellGlobNames.Value = "";


                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// 将界面上的内容保存到Station配置中
        /// </summary>
        /// <returns></returns>
        bool Save2Station(out string errorInfo)
        {
            errorInfo = "Success";
            if (_station == null)
                return true;
            bool isCheckOK = true;
            StringBuilder sbErrorInfo = new StringBuilder();
            //先检查所有参数合法性
            foreach (TabPage tp in tabControlCF1.TabPages)
            {
                DataGridView dgv = tp.Controls[0] as DataGridView;
                NamedChnType chnType = (NamedChnType)tp.Tag;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    List<List<string>> locAndGlobMaps = null;
                    string[] allGlobChnNamesInStation = null; //所有可供绑定的全局通道名称
                    switch (chnType)
                    {
                        case NamedChnType.Di://数字输入
                            locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Di];
                            allGlobChnNamesInStation = _station.DINames;
                            break;
                        case NamedChnType.Do://
                            locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Do];
                            allGlobChnNamesInStation = _station.DONames;
                            break;
                        case NamedChnType.Axis://
                            locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Axis];
                            allGlobChnNamesInStation = _station.AxisNames;
                            break;
                      
                        case NamedChnType.Ai://
                            locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Ai];
                            allGlobChnNamesInStation = _station.AINames;
                            break;
                        case NamedChnType.Ao://
                            locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Ao];
                            allGlobChnNamesInStation = _station.AONames;
                            break;
                        case NamedChnType.Camera:
                            locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.Camera];
                            allGlobChnNamesInStation = _station.CameraNames;
                            break;
                        case NamedChnType.LineScan://
                            locAndGlobMaps = _station.DeclearedDevChnMapping[NamedChnType.LineScan];
                            allGlobChnNamesInStation = _station.LineScanNames;
                            break;
                      
                        default:
                            continue;
                    }
                    string chnLocName = row.Cells[0].Value as string;//站内通道名称
                  
                    DataGridViewComboBoxCell cellGlobNames = row.Cells[1] as DataGridViewComboBoxCell;
                    string currGlobNameSel = cellGlobNames.Value as string; //当前所选的全局通道名称
                    if (string.IsNullOrEmpty(currGlobNameSel))
                    {
                        isCheckOK = false;
                        sbErrorInfo.AppendLine("站内" + chnType.ToString() + "通道:\"" + chnLocName + "\"未绑定全局ID");
                    }
                    else
                    {
                        if (null == allGlobChnNamesInStation || !allGlobChnNamesInStation.Contains(currGlobNameSel))
                        {
                            isCheckOK = false;
                            sbErrorInfo.AppendLine("站内" + chnType.ToString() + "通道:\"" + chnLocName + "\n绑定的全局ID = \"" + currGlobNameSel + "\"在工站设备表中不存在");
                        }

                    }

                }
            }
            if (!isCheckOK)
            {
                errorInfo = sbErrorInfo.ToString();
                return false;
            }
            //保存所有参数

            foreach (TabPage tp in tabControlCF1.TabPages)
            {
                DataGridView dgv = tp.Controls[0] as DataGridView;
                NamedChnType chnType = (NamedChnType)tp.Tag;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    string locName = row.Cells[0].Value as string;
                    string globName = row.Cells[1].Value as string;
                    List<List<string>> locGlobMappings = _station.DeclearedDevChnMapping[chnType];
                    foreach (List<string> lg in locGlobMappings)
                        if (lg[0] == locName)
                        {
                            lg[1] = globName;
                            break;
                        }
                }
            }
            _station.SaveCfg();
            return true;
        }


        bool _isEditting = false;

        /// <summary>
        /// 编辑/保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEditSave_Click(object sender, EventArgs e)
        {
            if (_station == null)
                return;
            if (!_isEditting)
            {
                _isEditting = true;
                btEditSave.Text = "保存";
                btEditCancel.Enabled = true;
                foreach (TabPage tp in tabControlCF1.TabPages)
                {
                    DataGridView dgv = tp.Controls[0] as DataGridView;
                    dgv.ReadOnly = false;
                    dgv.Columns[0].ReadOnly = true;
                    dgv.Columns[1].ReadOnly = false;
                }
            }
            else
            {
                string errInfo = null;
                if (!Save2Station(out errInfo))
                {
                    MessageBox.Show("保存操作失败:\n" + errInfo);
                    return;
                }
                MessageBox.Show("变更已保存");
                _isEditting = false;
                btEditSave.Text = "编辑";
                btEditCancel.Enabled = false;
                foreach (TabPage tp in tabControlCF1.TabPages)
                {
                    DataGridView dgv = tp.Controls[0] as DataGridView;
                    dgv.ReadOnly = true;
                }

            }
        }

        /// <summary>
        /// 取消变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEditCancel_Click(object sender, EventArgs e)
        {
            if (_station == null)
                return;
            LoadStation();
            _isEditting = false;
            btEditSave.Text = "编辑";
            btEditCancel.Enabled = false;
            foreach (TabPage tp in tabControlCF1.TabPages)
            {
                DataGridView dgv = tp.Controls[0] as DataGridView;
                dgv.ReadOnly = true;
            }

        }

        public void UcStationDevChnNameMapping_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                LoadStation();

            }
            else
            {
                if (_station == null)
                    return;
                if (_isEditting)
                {
                    if (DialogResult.OK == MessageBox.Show("即将离开正在编辑的DevChn-Mapping界面\n是否保存当前变更？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    {
                        string errInfo = null;
                        if (!Save2Station(out errInfo))
                        {
                            MessageBox.Show("DevChn-Mapping参数保存失败，错误信息:\n" + errInfo);
                            LoadStation();
                        }
                    }
                    _isEditting = false;
                    btEditSave.Text = "编辑";
                    btEditCancel.Enabled = false;
                    foreach (TabPage tp in tabControlCF1.TabPages)
                    {
                        DataGridView dgv = tp.Controls[0] as DataGridView;
                        dgv.Columns[1].ReadOnly = true;
                    }

                }
            }
        }
    }
}
