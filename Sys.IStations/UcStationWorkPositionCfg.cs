﻿using Cell.DataModel;
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

namespace Sys.IStations
{
    /// <summary>
    /// 用于配置工站点位的界面类
    /// </summary>
    public partial class UcStationWorkPositionCfg : UserControl
    {
        public UcStationWorkPositionCfg()
        {
            InitializeComponent();
        }

        bool _isFormLoaded = false;
        bool _isEditting = false; //是否处于点位编辑状态
      
        IStationBase _station = null;
        private void UcStationWorkPositionCfg_Load(object sender, EventArgs e)
        {
            _isFormLoaded = true;
            if (Parent != null)
                Parent.VisibleChanged += new System.EventHandler(this.UcStationWorkPositionCfg_VisibleChanged);//将当前的VisibalChange绑定到父控件上

            UpdateStation2UI();
            IsEditting = false;
        }
        bool IsEditting
        {
            get { return _isEditting; }
            set
            {
                _isEditting = value;
                if (_station == null)
                    return;
                string[] posNames = _station.WorkPositionNames;
                string[] axisNames = _station.AxisNames;
                dgvPos.Enabled = _isEditting;
                btAdd.Enabled = _isEditting;
                btDel.Enabled = _isEditting ? (dgvPos.Rows.Count > 0 ? true : false) : false;
                btCancel.Enabled = _isEditting;
                btEdit.Text = _isEditting ? "保存" : "编辑";
                if (_isEditting)
                {
                    for (int i = 0; i < posNames.Length; i++)
                        for (int j = 0; j < axisNames.Length; j++)
                        {
                            if (_station.GetWorkPosition(posNames[i]).ContainAxis(axisNames[j]))
                            {
                                dgvPos.Rows[i].Cells[j * 2 + 2].ReadOnly = false;//开放设置
                                dgvPos.Rows[i].Cells[j * 2 + 2].Style.BackColor = Color.White;
                                double tmp = 0;
                                if (!double.TryParse(dgvPos.Rows[i].Cells[j * 2 + 2].Value as string, out tmp))
                                    dgvPos.Rows[i].Cells[j * 2 + 2].Value = "";
                            }
                            else
                            {
                                dgvPos.Rows[i].Cells[j * 2 + 2].Value = "未指定";
                                dgvPos.Rows[i].Cells[j * 2 + 2].ReadOnly = true;//
                                dgvPos.Rows[i].Cells[j * 2 + 2].Style.BackColor = SystemColors.ControlDark;
                            }
                        }
                }
                else
                {

                }
            }
        }

        public void SetStation(IStationBase station)
        {
            _station = station;
            if (_isFormLoaded)
            {
                UpdateStation2UI();
                IsEditting = false;
            }
        }

        /// <summary>
        /// 将编辑后得内容保存到工站配置中
        /// </summary>
        /// <returns></returns>
        public bool SaveEditChange()
        {
            string[] axisNames = _station.AxisNames;
            for (int i = 0; i < dgvPos.Rows.Count; i++)
            {
                DataGridViewRow row = dgvPos.Rows[i];
                string posName = row.Cells[0].Value as string;
                IMultiAxisProPos ap = _station.GetWorkPosition(posName);
                for (int j = 0; j < axisNames.Length; j++)
                {
                    DataGridViewCheckBoxCell enableCell = row.Cells[j * 2 + 1] as DataGridViewCheckBoxCell; //是否使能轴位置
                    DataGridViewTextBoxCell posCell = row.Cells[j * 2 + 2] as DataGridViewTextBoxCell;
                    if ((bool)enableCell.Value)
                    {
                        double pos = 0;
                        if (!double.TryParse(posCell.Value as string, out pos))
                        {
                            string axisName =  _station.AxisNames[j];
                            MessageBox.Show(string.Format("保存失败！请检查位置参数格式，必须为数字\n点位名称：{0}  轴名称:{1}", posName, axisName));
                            posCell.Style.BackColor = Color.Red;
                            return false;
                        }
                        ap.SetAxisPos(axisNames[j], pos);
                    }
                    else
                        ap.RemoveAxis(axisNames[j]);

                }

            }
            _station.SaveCfg();
            return true;
        }



        /// <summary>
        /// 根据工站点位更新界面布局
        /// </summary>
        void UpdateStation2UI()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateStation2UI));
                return;
            }
            dgvPos.Columns.Clear();
            if (null == _station)
            {
                lbTips.Text = "工站未设置";
                btDel.Enabled = false;
                btAdd.Enabled = false;
                btEdit.Enabled = false;
                btCancel.Enabled = false;
                dgvPos.Enabled = false;
                return;
            }


            string[] axisNames = _station.AxisNames;
            if (null == axisNames || 0 == axisNames.Length)
            {
                lbTips.Text = "工站:" + _station.Name + "  无可用轴！";
                btDel.Enabled = false;
                btAdd.Enabled = false;
                btEdit.Enabled = false;
                btCancel.Enabled = false;
                dgvPos.Enabled = false;
                return;
            }
            btEdit.Enabled = true;
            lbTips.Text = "工站:" + _station.Name + " 点位设置";


            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = "工作点位名称";
            col.Width = 150;
            dgvPos.Columns.Add(col);
            for (int i = 0; i < axisNames.Length; i++)
            {
                col = new DataGridViewCheckBoxColumn(); //是否使能轴位置//new DataGridViewColumn();
                col.HeaderText = "使能";
                col.Width = 40;
                dgvPos.Columns.Add(col);
                col = new DataGridViewTextBoxColumn();
                col.HeaderText = "位置";
                col.Width = 120;
                dgvPos.Columns.Add(col);
            }
            col = new DataGridViewButtonColumn();
            col.HeaderText = "使用当前位置";
            col.Width = 100;
            dgvPos.Columns.Add(col);

            string[] posNames = _station.WorkPositionNames;
            if (null == posNames || 0 == posNames.Length)
            {
                lbTips.Text += "无点位";
                return;
            }
            foreach (string posName in posNames)
            {
                IMultiAxisProPos pos = _station.GetWorkPosition(posName);
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell cellName = new DataGridViewTextBoxCell();
                cellName.Value = posName;
                row.Cells.Add(cellName);
                for (int i = 0; i < axisNames.Length; i++)
                {
                    DataGridViewCheckBoxCell cellEnable = new DataGridViewCheckBoxCell(); //本轴是否使能
                    row.Cells.Add(cellEnable);


                    DataGridViewTextBoxCell cellAP = new DataGridViewTextBoxCell();
                    if (!pos.ContainAxis(axisNames[i]))
                    {
                        cellEnable.Value = false;
                        cellAP.Style.BackColor = SystemColors.ControlDark;
                        cellAP.Value = "未指定";
                    }
                    else
                    {
                        cellEnable.Value = true;
                        cellAP.Style.BackColor = Color.White;
                        cellAP.Value = pos.GetAxisPos(axisNames[i]).ToString();
                    }

                    row.Cells.Add(cellAP);


                }


                DataGridViewButtonCell btCell = new DataGridViewButtonCell();
                btCell.Value = "更新";
                row.Cells.Add(btCell);
                dgvPos.Rows.Add(row);
            }
         
            for (int i = 0; i < axisNames.Length; i++)
            {
                string axisName = axisNames[i];
                dgvPos.ColumnHeadersHeight = 40;
                dgvPos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                dgvPos.AddSpanHeader(i * 2 + 1, 2, _station.GetDecChnAliasName(NamedChnType.Axis, axisName));//dgvPos.AddSpanHeader(i*2+1, 2, axisName);
            }


        }

        private void UcStationWorkPositionCfg_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                UpdateStation2UI();
                IsEditting = false;
            }
            else
            {
                if (_station == null)
                    return;
                if (_isEditting) //当前正处于编辑模式
                {
                    if (DialogResult.Yes == MessageBox.Show("离开点位配置界面，是否保存已变更的数据？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        SaveEditChange();

                }
            }
        }

        /// <summary>
        /// 添加新点位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, EventArgs e)
        {
            BenameDialog nameDialog = new BenameDialog();
            nameDialog.Text = "添加新点位";
            if (nameDialog.ShowDialog() == DialogResult.OK)
            {
                string posName = nameDialog.GetName();
                if (_station.ContianPositionName(posName))
                {
                    MessageBox.Show("不能添加已存在的点位名称:" + posName);
                    return;
                }
                IMultiAxisProPos newPos = new IMultiAxisProPos();
                newPos.Name = posName;
                _station.AddWorkPosition(newPos);
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell cellName = new DataGridViewTextBoxCell();
                cellName.Value = posName;
                row.Cells.Add(cellName);
                foreach (string axisName in _station.AxisNames)
                {
                    DataGridViewCheckBoxCell chkEnable = new DataGridViewCheckBoxCell();
                    chkEnable.Value = false;
                    row.Cells.Add(chkEnable);
                    DataGridViewTextBoxCell cellPos = new DataGridViewTextBoxCell();
                    cellPos.Style.BackColor = SystemColors.ControlDark;
                    row.Cells.Add(cellPos);
                }
                DataGridViewButtonCell btCell = new DataGridViewButtonCell();
                btCell.Value = "更新";
                row.Cells.Add(btCell);
                dgvPos.Rows.Add(row);
                btDel.Enabled = true;
            }

        }

        /// <summary>
        /// 删除所选点位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDel_Click(object sender, EventArgs e)
        {
            if (null == _station)
                return;
            if (dgvPos.SelectedRows == null || dgvPos.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要删除的点位！");
                return;
            }
            List<string> delNames = new List<string>();
            foreach (DataGridViewRow row in dgvPos.SelectedRows)
            {
                string wpName = row.Cells[0].Value as string;
                if (_station.IsWorkPosDecleared(wpName))
                {
                    MessageBox.Show("工作点位:\"" + wpName + "\"为固有属性，不可删除！");
                    return;
                }
                delNames.Add(wpName);
            }
            if (DialogResult.OK == MessageBox.Show("确定删除以下点位?\n" + string.Join("\n", delNames)))
            {
                foreach (string delName in delNames)
                    _station.RemoveWorkPosition(delName);
            }
            foreach (DataGridViewRow row in dgvPos.SelectedRows)
                dgvPos.Rows.Remove(row);
            if (dgvPos.Rows.Count == 0)
                btDel.Enabled = false;
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (!IsEditting) //开始编辑
                IsEditting = true;
            else //保存编辑后的内容
            {
                if (DialogResult.OK == MessageBox.Show("确定要保存编辑的内容？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                {
                    if (SaveEditChange())
                        IsEditting = false;
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确定要取消当前变更?\n\"重新载入配置\""))
            {
                _station.LoadCfg();
                UpdateStation2UI();
                IsEditting = false;
            }
        }

        /// <summary>
        /// 点击单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPos.ColumnCount - 1) //用工站各轴的当前位置更新点位
            {
                bool isOK = true;
                StringBuilder sbErrorInfo = new StringBuilder();
                string posName = dgvPos.Rows[e.RowIndex].Cells[0].Value as string;
                for (int i = 0; i < _station.AxisNames.Length; i++)
                {
                    double pos = 0;
                    string errorInfo;
                    if (!_station.GetAxisPosition(_station.AxisNames[i], out pos, out errorInfo))
                    {
                        string axisShowName =  _station.AxisNames[i];                       
                        sbErrorInfo.Append(string.Format("未能获取轴\"{0}\" 当前位置！:{1}\n", axisShowName, errorInfo));
                        dgvPos.Rows[e.RowIndex].Cells[i * 2 + 2].Value = "未能获取";
                        isOK = false;
                    }
                    else
                    {
                        dgvPos.Rows[e.RowIndex].Cells[i * 2 + 2].Value = pos.ToString();
                        _station.GetWorkPosition(posName).SetAxisPos(_station.AxisNames[i], pos);
                    }

                }
                if (!isOK)
                    MessageBox.Show("更新工作点位失败,错误信息:\n" + sbErrorInfo.ToString());
            }
            else
            {
                if (e.ColumnIndex % 2 != 0) //轴使能按钮被点击
                {
                    DataGridViewCheckBoxCell cellEnable = dgvPos.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                    DataGridViewTextBoxCell cellPos = dgvPos.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] as DataGridViewTextBoxCell;
                    cellEnable.Value = Convert.ToBoolean(cellEnable.EditingCellFormattedValue);
                    if (Convert.ToBoolean(cellEnable.EditingCellFormattedValue))//if ((bool)cellEnable.Value)
                    {
                        cellPos.ReadOnly = false;
                        cellPos.Style.BackColor = Color.White;
                        double tmp;
                        if (!double.TryParse(cellPos.Value as string, out tmp))
                            cellPos.Value = "";
                    }
                    else
                    {
                        cellPos.ReadOnly = true;
                        cellPos.Style.BackColor = SystemColors.ControlDark;
                    }
                    return;
                }
            }
        }

        private void dgvPos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().Equals(typeof(DataGridViewTextBoxEditingControl)))
            {
                e.CellStyle.BackColor = Color.FromName("window");
                DataGridViewTextBoxEditingControl textControl = e.Control as DataGridViewTextBoxEditingControl;
                textControl.TextChanged += new EventHandler(PosCell_TextChanged);
            }
        }

        private void PosCell_TextChanged(object sender, EventArgs e)
        {
            string posText = ((TextBox)sender).Text;
            DataGridViewTextBoxCell currCell = dgvPos.CurrentCell as DataGridViewTextBoxCell;
            string posName = dgvPos.Rows[currCell.RowIndex].Cells[0].Value as string;
            string axisName = _station.AxisNames[currCell.ColumnIndex / 2 - 1];
            double tmp = 0;
            if (!double.TryParse(posText, out tmp))
            {
                currCell.Style.BackColor = Color.Red;
                ((TextBox)sender).BackColor = currCell.Style.BackColor;
            }
            else
            {
                if (_station.GetWorkPosition(posName).ContainAxis(axisName) && tmp == _station.GetWorkPosition(posName).GetAxisPos(axisName))
                {
                    currCell.Style.BackColor = Color.White;
                    ((TextBox)sender).BackColor = currCell.Style.BackColor;
                }
                else
                {
                    currCell.Style.BackColor = Color.Orange;
                    ((TextBox)sender).BackColor = currCell.Style.BackColor;
                }
            }

        }
    }

}
