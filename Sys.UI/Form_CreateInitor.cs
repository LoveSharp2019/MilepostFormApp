using Cell.DataModel;
using Cell.Interface;
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

namespace Sys.UI
{
    public partial class Form_CreateInitor : windowBase
    {
        public Type MatchType { private get; set; }
        //系统中已经存在的名称
        public string[] ExistIDs { private get; set; }
        public string ID { get { return tbID.Text; } }
        bool _isFixedID = false;
        string _fixID = null;

        // 私有
        IPlatInitializable initor;
        //公有的
        public IPlatInitializable Initor { get { return initor; } }

        public Form_CreateInitor()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            initor = null;
            ExistIDs = AppHubCenter.Instance.InitorManager.InitorIDs;
        }

        private void Form_CreateInitor_Load(object sender, EventArgs e)
        {
            dgvTypes.Rows.Clear();
            dgvTypes.MultiSelect = false;
            dgvTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Type[] initorTypes = AppHubCenter.Instance.InitorHelp.InstantiatedClasses(MatchType);
            foreach (Type it in initorTypes)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell cellType = new DataGridViewTextBoxCell();
                cellType.Value = it.AssemblyQualifiedName;
                row.Cells.Add(cellType);
                MyDisplayNameAttribute[] vn = it.GetCustomAttributes(typeof(MyDisplayNameAttribute), false) as MyDisplayNameAttribute[];
                if (null != vn && vn.Length > 0)
                {
                    DataGridViewTextBoxCell cellName = new DataGridViewTextBoxCell();
                    cellName.Value = vn[0].Name;
                    row.Cells.Add(cellName);
                }
                dgvTypes.Rows.Add(row);
            }
            dgvTypes.ClearSelection();

            if (_isFixedID)
            {
                tbID.Text = _fixID;
                tbID.ReadOnly = true;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {

            if (dgvTypes.SelectedRows == null || dgvTypes.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要创建的设备类型");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbID.Text))
            {
                MessageBox.Show("设备ID不能为空,请重新输入");
                tbID.Focus();
                return;
            }

            if (!_isFixedID && AppHubCenter.Instance.InitorManager.ContainID(tbID.Text))
            {
                IPlatInitializable dev = AppHubCenter.Instance.InitorManager.GetInitor(tbID.Text);
                string disTypeName = AppIplatinitHelper.DispalyTypeName(dev.GetType());
                MessageBox.Show(string.Format("Initor列表中已存在ID = \"{0}\" Type = \"{1}\"的设备\n请重新输入ID!", tbID.Text, disTypeName));
                tbID.Focus();
                return;
            }

            for (int i = 0; i < panelParams.Controls.Count; i++)
            {
                UcParamEdit pe = panelParams.Controls[i] as UcParamEdit;
                object paramVal = null;
                pe.GetParamValue(out paramVal);

                if (!initor.SetInitParamValue(initor.InitParamNames[i], paramVal))
                {
                    MessageBox.Show(string.Format("设置初始化参数失败:Name = {0},Value = {1}", initor.InitParamNames[i], paramVal.ToString()));

                    pe.Focus();
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (null != initor)
            {
                initor.Dispose();
                initor = null;
            }
            DialogResult = DialogResult.Cancel;
        }

        int RowIndexSelected = -1;
        private void dgvTypes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (RowIndexSelected == e.RowIndex)
                return;
            RowIndexSelected = e.RowIndex;
            if (null != initor)
            {
                initor.Dispose();
                initor = null;
            }

            panelParams.RowStyles.Clear();
            panelParams.Controls.Clear();
            panelParams.AutoScroll = true;
            initor = AppHubCenter.Instance.InitorHelp.CreateInstance(dgvTypes.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (null == initor)
            {
                string err = string.Format("Invoke Initor's Ctor Failed！InitorHelp.CreateInstance(Type = {0}) return null", dgvTypes.Rows[e.RowIndex].Cells[0].Value.ToString());
                lbTips.Text = err;
                //MessageBox.Show(err);
                return;
            }

            string[] paramNames = initor.InitParamNames;
            if (null == paramNames || 0 == paramNames.Length)
                return;

            panelParams.RowCount = paramNames.Length + 1;
            for (int i = 0; i < paramNames.Length; i++)
            {
                string pn = paramNames[i];
                CosParams pd = initor.GetInitParamDescribe(pn);
                UcParamEdit uc = new UcParamEdit();
                uc.EventValueChanged += OnParamValueChanged;
                uc.Parent = panelParams;
                panelParams.SetRow(uc, i);
                uc.Width = panelParams.Width - 200;
                uc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                uc.SetParamDesribe(pd);
                uc.SetParamValue(initor.GetInitParamValue(paramNames[i]));
                uc.IsValueReadOnly = false;
                // 一个UcParamEdit 标准高度
                panelParams.RowStyles.Add(new RowStyle(SizeType.Absolute, 51));
            }
            this.Focus();
        }

        void OnParamValueChanged(UcParamEdit sender, CosParams describe, object newValue)
        {
            object value = Convert.ChangeType(newValue, Type.GetType(describe.ptype));
            // 赋值的时候进行转型
            if (!initor.SetInitParamValue(describe.pName, value))
            {
                MessageBox.Show("设置参数项：\"" + describe.pName + "\" Value = \"" + newValue.ToString() + "\"失败！");
                return;
            }

            //**  需要回调的都是 通过回调， 如果不需要回调的 则需要主动修改自动参数
            //  退出
            if (!describe.pboolCallBack)
            {
                sender.SetParamValue(value);
                return;
            }


            panelParams.RowStyles.Clear();
            panelParams.Controls.Clear();
            panelParams.AutoScroll = true;

            if (null == initor)
            {
                string err = string.Format("Invoke Initor's Ctor Failed！InitorHelp.CreateInstance(Type = {0}) return null", dgvTypes.SelectedRows[0].Cells[0].Value.ToString());
                lbTips.Text = err;
                return;
            }

            string[] paramNames = initor.InitParamNames;
            if (null == paramNames || 0 == paramNames.Length)
                return;

            panelParams.RowCount = paramNames.Length + 1;
            for (int i = 0; i < paramNames.Length; i++)
            {
                string pn = paramNames[i];
                CosParams pd = initor.GetInitParamDescribe(pn);

                UcParamEdit uc = new UcParamEdit();
                uc.EventValueChanged += OnParamValueChanged;
                uc.Parent = panelParams;
                panelParams.SetRow(uc, i);
                uc.Width = panelParams.Width - 200;
                uc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                uc.SetParamDesribe(pd);
                uc.SetParamValue(initor.GetInitParamValue(paramNames[i]));
                uc.IsValueReadOnly = false;

                // 一个UcParamEdit 标准高度
                panelParams.RowStyles.Add(new RowStyle(SizeType.Absolute, 51));
            }
            this.Focus();

        }
    }
}
