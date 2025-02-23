﻿using Cell.Interface;
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

namespace Org.IMotionDaq
{
    /// <summary>
    /// 单个DIO 的控制元件
    /// </summary>
    public partial class UcDioChn : UserControl
    {

        public UcDioChn()
        {
            InitializeComponent();
            tbEdit.Visible = false;
            OnColor = LampButton.LColor.Green;
            OffColor = LampButton.LColor.Gray;
            Lamp.Click += new EventHandler(this.OnLampButtonClick);
        }

        private void UcDIO_Load(object sender, EventArgs e)
        {

        }

        public LampButton LampBT { get { return Lamp; } }


        [Category("属性"), Description("正在编辑名称"), Browsable(true)]
        public bool IsEditting
        {
            get { return tbEdit.Visible; }
            set
            {
                tbEdit.Visible = value;
                if (tbEdit.Visible)
                {
                    tbEdit.Text = IOName;
                }

            }
        }

        [Category("属性"), Description("DIO名称"), Browsable(true)]
        public string IOName
        {
            get { return Lamp.Text; }
            set { Lamp.Text = value; }
        }

        [Category("属性"), Description("修改的DIO名称"), Browsable(true)]
        public string IONameEditting
        {
            get { return tbEdit.Text; }
            set { tbEdit.Text = value; }
        }

        [Category("属性"), Description("DIO有信号颜色"), Browsable(true)]
        public LampButton.LColor OnColor { get; set; }

        [Category("属性"), Description("DIO无信号颜色"), Browsable(true)]
        public LampButton.LColor OffColor { get; set; }


        [Category("属性"), Description("DIO名称文字颜色"), Browsable(true)]
        public Color IONameTextColor
        {
            get { return Lamp.ForeColor; }
            set { Lamp.ForeColor = value; }
        }


        bool isTurnOn = false;

        public bool IsTurnOn
        {
            get { return isTurnOn; }
            set
            {
                isTurnOn = value;
                Lamp.LampColor = isTurnOn ? OnColor : OffColor;
            }
        }

        public LampButton LButton { get { return Lamp; } }


        private void Lamp_SizeChanged(object sender, EventArgs e)
        {
            Rectangle tbEditRect = new Rectangle(ClientRectangle.Height * 5 / 6, (ClientRectangle.Height - tbEdit.Height) / 2,
                   ClientRectangle.Width - ClientRectangle.Height * 5 / 6, tbEdit.Height);
            tbEdit.Location = tbEditRect.Location;
            tbEdit.Size = tbEditRect.Size;
        }

        private void tbEdit_TextChanged(object sender, EventArgs e)
        {
            if (IOName != IONameEditting)
                tbEdit.ForeColor = Color.Red;
            else
                tbEdit.ForeColor = Color.Black;
        }

        //点击事件
        private void OnLampButtonClick(object sender, EventArgs e)
        {
            if (null != _dio)
            {
                if (!_isDo)
                    return;
                if (_ioIndex < 0 || _ioIndex >= _dio.DOCount)
                    return;
                bool isSigOn = false;
                if (0 != _dio.GetDO(_ioIndex, out isSigOn))
                    return;
                if (0 != _dio.SetDO(_ioIndex, !isSigOn))
                    return;
                if (0 != _dio.GetDO(_ioIndex, out isSigOn))
                    return;
                IsTurnOn = isSigOn;
            }

            else //IO信息未设置，调用外部的Click函数代理
            {
                Delegate dl = GetEventsDelegate("EventClick");
                if (null != dl)
                    dl.DynamicInvoke(new object[] { sender, e });
            }
        }

        /// <summary>
        /// 通过反射获取私有事件的代理
        /// </summary>
        /// <param name="p_EventName"></param>
        /// <returns></returns>
        private Delegate GetEventsDelegate(string p_EventName)
        {
            PropertyInfo _PropertyInfo = this.GetType().GetProperty("Events", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            if (_PropertyInfo != null)
            {
                object _EventList = _PropertyInfo.GetValue(this);
                if (_EventList != null && _EventList is EventHandlerList)
                {
                    EventHandlerList _List = (EventHandlerList)_EventList;
                    FieldInfo _FieldInfo = (typeof(Control)).GetField(p_EventName, BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
                    if (_FieldInfo == null) return null;
                    Delegate _ObjectDelegate = _List[_FieldInfo.GetValue(this)];
                    return _ObjectDelegate;
                }
            }
            return null;
        }

        /// <summary>
        /// 在编辑框中拦截回车键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        IPlatModule_DIO _dio = null;
        int _ioIndex = 0;
        bool _isDo = false;
        delegate void DgtSetDioInfo(IPlatModule_DIO dio, int index, bool isDo, string ioName);
        //设置DIO信息，方便控件单独使用,
        public void SetDioInfo(IPlatModule_DIO dio, int index, bool isDo, string ioName)
        {
            if (InvokeRequired)
            {
                DgtSetDioInfo dlgt = new DgtSetDioInfo(SetDioInfo);
                BeginInvoke(dlgt, new object[] { dio, index, isDo, ioName });
                return;
            }
            _dio = dio;
            _ioIndex = index;
            _isDo = isDo;
            IOName = ioName;
        }

        public void UpdateIO()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateIO));
                return;
            }
            if (null == _dio)
                return;

            if (_ioIndex < 0 || (_isDo ? _ioIndex >= _dio.DOCount : _ioIndex >= _dio.DICount))
                return;
            bool isSigOn = false;
            if (0 != (_isDo ? _dio.GetDO(_ioIndex, out isSigOn) : _dio.GetDI(_ioIndex, out isSigOn)))
                return;
            IsTurnOn = isSigOn;

        }

    }
}
