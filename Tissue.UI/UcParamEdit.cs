using Cell.DataModel;
using Cell.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tissue.UI;

namespace Tissue.UI
{
    /// <summary>
    /// 控件中的值发生改变
    /// </summary>
    /// <param name="paramDescribe"></param>
    /// <param name="newValue"></param>
    public delegate void dgParamEditValueChanged(UcParamEdit sender, CosParams paramDescribe, object newValue);

    public partial class UcParamEdit : UserControl
    {
        // 参数数值变更事件
        public event dgParamEditValueChanged EventValueChanged;

        //  当前参数的变量
        private ucProperty proty = new ucProperty();
        private Type paramType;
        private CosParams paramDescribe;

        public UcParamEdit()
        {
            InitializeComponent();
        }

        private void UcInitorParam_Load(object sender, EventArgs e)
        {

        }


        [Category("uc属性"), Description("ValueReadOnly"), Browsable(true)]
        public bool IsValueReadOnly
        {
            get { return !this.Enabled; }
            set { this.Enabled = !value; }
        }


        public Control EditCotl { get; set; }
        /// <summary>
        /// 设置参数描述信息
        /// </summary>
        /// <param name="pd"></param>
        public void SetParamDesribe(CosParams pd)
        {
            paramDescribe = pd;
            proty.Name = paramDescribe.pName;
            proty.ParamType =
            paramType = Type.GetType(paramDescribe.ptype);

            if (paramDescribe.pvLimit == cValueLimit.File)// 文件
            {
                UcFileEdit ucFileEdit = new UcFileEdit();
                ucFileEdit.UcFileEdit_ValueChange += FileFolderChange;
                ucFileEdit.Dock = DockStyle.Fill;
                pnl_control.Controls.Add(ucFileEdit);
                EditCotl = ucFileEdit;
            }
            else if (paramDescribe.pvLimit == cValueLimit.Folder)// 文件夹
            {
                UcFolderEdit ucFolderEdit = new UcFolderEdit();
                ucFolderEdit.UcFolderEdit_ValueChange += FileFolderChange;
                ucFolderEdit.Dock = DockStyle.Fill;
                pnl_control.Controls.Add(ucFolderEdit);
                EditCotl = ucFolderEdit;
            }
            else if (paramDescribe.pvLimit == cValueLimit.Range) // 下拉框
            {
                ComboBox comboBox = new ComboBox();
                comboBox.SelectedIndexChanged += new System.EventHandler(comboBox_SelectedIndexChanged);
                comboBox.Dock = DockStyle.Fill;
                pnl_control.Controls.Add(comboBox);
                EditCotl = comboBox;
            }
            else
            {
                UcTextBoxPop ucTextBoxPop = new UcTextBoxPop();
                ucTextBoxPop.LostFocus += UcTextBoxPop_LostFocus;
                // ucTextBoxPop.MouseLeave += UcTextBoxPop_MouseLeave;
                // ucTextBoxPop.TextChanged += UcTextBoxPop_TextChanged;
                ucTextBoxPop.Dock = DockStyle.Fill;
                pnl_control.Controls.Add(ucTextBoxPop);
                EditCotl = ucTextBoxPop;
            }

            proty.Value = GetDefaultValue(Type.GetType(paramDescribe.ptype));

            if (proty.Value is List<string>)
            {
                ComboBox comboBox = new ComboBox();
                comboBox.SelectedIndexChanged += new System.EventHandler(comboBox_SelectedIndexChanged);
                comboBox.Dock = DockStyle.Fill;
                pnl_control.Controls.Add(comboBox);
                EditCotl = comboBox;
            }
            else if (proty.Value is string[])
            {
                ComboBox comboBox = new ComboBox();
                comboBox.SelectedIndexChanged += new System.EventHandler(comboBox_SelectedIndexChanged);
                comboBox.Dock = DockStyle.Fill;
                pnl_control.Controls.Add(comboBox);
                EditCotl = comboBox;
            }


            StringBuilder sb = new StringBuilder();
            sb.Append("描述:" + paramDescribe.pDescription + " || ");
            int ct = 0;
            //有最小值限制
            if ((paramDescribe.pvLimit & cValueLimit.Min) > 0 && paramDescribe.pvalue != null && paramDescribe.pvalue.Length > 0)
            {
                sb.Append("Min:" + paramDescribe.pvalue[0].ToString());
                ct++;
            }
            //有最大值限制
            if ((paramDescribe.pvLimit & cValueLimit.Max) > 0 && paramDescribe != null && paramDescribe.pvalue.Length > ct)
            {
                if (ct > 0)
                    sb.Append("|");
                sb.Append("Max:" + paramDescribe.pvalue[ct].ToString());
                ct++;
            }

            proty.Description = sb.ToString();

            _isSetGridVal = true;

            if (EditCotl is ComboBox)
                ((ComboBox)EditCotl).DataSource = paramDescribe.pvalue;

            //  显示描述和名称
            if (this.IsHandleCreated)
            {
                this.lbl_name.BeginInvoke(new Action(() => this.lbl_name.Text = proty.Name));
                this.lblDes.BeginInvoke(new Action(() => this.lblDes.Text = proty.Description));
            }

            _isSetGridVal = false;
        }






        #region  数值变更事件
        object lastValue = null;
        bool _isSetGridVal = false; //由软件主动设置

        private void UcTextBoxPop_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UcTextBoxPop_MouseLeave(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void UcTextBoxPop_LostFocus(object sender, EventArgs e)
        {
            UcTextBoxPop ucTextBoxPop = (UcTextBoxPop)sender;
            // if (!ucTextBoxPop.Focused) return;
            //有最小值限制
            if (paramType.IsValueType && (paramDescribe.pvLimit & cValueLimit.Min) > 0 && paramDescribe.pvalue != null && paramDescribe.pvalue.Length > 0)
            {
                double dlValue = Convert.ToDouble(ucTextBoxPop.Text);
                if (dlValue < Convert.ToDouble(paramDescribe.pvalue[0]))
                {
                    ucTextBoxPop.Text = lastValue.ToString();
                    MessageBox.Show(string.Format("取值超出范围",
                        paramDescribe.pvalue[0].ToString(), paramDescribe.pvalue[0].ToString()),
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //有最大值限制
            if (paramType.IsValueType && (paramDescribe.pvLimit & cValueLimit.Max) > 0 && paramDescribe.pvalue != null && paramDescribe.pvalue.Length > 1)
            {
                double dlValue = Convert.ToDouble(ucTextBoxPop.Text);
                if (dlValue > Convert.ToDouble(paramDescribe.pvalue[1]))
                {
                    ucTextBoxPop.Text = lastValue.ToString();
                    MessageBox.Show(string.Format("取值超出范围",
                        paramDescribe.pvalue[0].ToString(), paramDescribe.pvalue[1].ToString()),
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (_isSetGridVal)
                return;

            lastValue = ucTextBoxPop.Text.Trim();
            EventValueChanged.Invoke(this,paramDescribe, ucTextBoxPop.Text);
        }


        private void FileFolderChange(string txt)
        {
            if (_isSetGridVal)
                return;

            lastValue = txt;
            EventValueChanged.Invoke(this,paramDescribe, txt);
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (_isSetGridVal)
                return;

            lastValue = comboBox.SelectedItem.ToString();
            if (_isSetGridVal)
                return;
            EventValueChanged.Invoke(this,paramDescribe, comboBox.SelectedItem.ToString());
        }
        #endregion


        /// <summary>
        /// 设置 当前配置的值
        /// </summary>
        /// <param name="pv"></param>
        public void SetParamValue(object pv)
        {
            _isSetGridVal = true;
            if (pv != null)
            {
                if (EditCotl is ComboBox)
                    ((ComboBox)EditCotl).SelectedItem = pv;
                if (EditCotl is UcTextBoxPop)
                    ((UcTextBoxPop)EditCotl).Text = pv.ToString();
                if (EditCotl is UcFolderEdit)
                    ((UcFolderEdit)EditCotl).FolderPath = pv.ToString();
                if (EditCotl is UcFileEdit)
                    ((UcFileEdit)EditCotl).FilePath = pv.ToString();
            }
            proty.Value = pv;
            _isSetGridVal = false;
        }

        string _paramErrorInfo = "";


        public bool GetParamValue(out object val)
        {
            val = null;
            _paramErrorInfo = "";

            try
            {
                if (proty.Value == null)
                {
                    _paramErrorInfo = "输入参数为空";
                    return false;
                }
                val = proty.Value;

                return true;
            }
            catch (Exception ex)
            {
                _paramErrorInfo = ex.Message;
                val = null;
                return false;
            }
        }

        public CosParams GetParamDesribe()
        {
            return paramDescribe;
        }

        /// <summary>
        ///  获取类型默认值
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private object GetDefaultValue(Type t)
        {
            if (t == typeof(string))
                return "";
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            ConstructorInfo[] ctors = t.GetConstructors(System.Reflection.BindingFlags.Instance
                                                          | System.Reflection.BindingFlags.NonPublic
                                                          | System.Reflection.BindingFlags.Public);
            if (null == ctors)
                throw new Exception("CreateInstance(Type t) failed By: Not found t-Instance's Constructor");
            foreach (ConstructorInfo ctor in ctors)
            {
                ParameterInfo[] ps = ctor.GetParameters();
                if (ps == null || ps.Length == 0)
                    return ctor.Invoke(null);
                else if (ps.Length == 1 && ps[0].ParameterType == typeof(int)) //用于初始化数组类/
                    return ctor.Invoke(new object[] { 0 });

            }

            return null;
        }

    }
}
