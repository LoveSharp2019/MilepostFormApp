using Cell.DataModel;
using Cell.Interface;
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
    public partial class UcStationRealTimeUIDebug : UcRealTimeUI
    {
        FormStationBaseAxisPanel _formAxis = new FormStationBaseAxisPanel();
        FormStationBaseDioPanel _formDio = new FormStationBaseDioPanel();
        UcStationWorkPositionCfg ucWorkPosition = new UcStationWorkPositionCfg();

        // 提供消息托管的方法
        AppStationManager stationMgr;

        public UcStationRealTimeUIDebug()
        {
            InitializeComponent();
        }

        IPlatStation sta = null;
        public void SetStation(IPlatStation _sta)
        {
            sta = _sta;
            ucStationRealtimeUI1.SetStation(sta);
            stationMgr = AppHubCenter.Instance.StationMgr;
            string[] allEnabledStationName = stationMgr.AllEnabledStationNames();

            // 由站点发出 UI 界面回调
            stationMgr.RemoveStationMsgReciever(ucStationRealtimeUI1);
            stationMgr.AppendStationMsgReceiver(_sta, ucStationRealtimeUI1);

            _formAxis.SetStation((IStationBase)sta);
            _formDio.SetStation((IStationBase)sta);
            ucWorkPosition.SetStation((IStationBase)sta);
        }

        /// <summary>
        /// UserControl 释放资源
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            stationMgr?.RemoveStationMsgReciever(ucStationRealtimeUI1);
            base.OnHandleDestroyed(e);
        }

        public void StationMsg(object ob, string info)
        {
            ucStationRealtimeUI1.OnTxtMsg(info);
        }

        private void UcStationRealTimeUIDebug_Load(object sender, EventArgs e)
        {
            (sta as IStationBase).WorkMsg2Outter += StationMsg;

            _formAxis.FormBorderStyle = FormBorderStyle.None;
            _formAxis.TopLevel = false;
            tabPageAxis.Controls.Add(_formAxis);
            _formAxis.Dock = DockStyle.Fill;
            _formAxis.Show();

            _formDio.FormBorderStyle = FormBorderStyle.None;
            _formDio.TopLevel = false;
            tabPageIO.Controls.Add(_formDio);
            _formDio.Dock = DockStyle.Fill;
            _formDio.Show();

            tabPagePos.Controls.Add(ucWorkPosition);
            ucWorkPosition.Dock = DockStyle.Fill;
            ucWorkPosition.Show();
        }
    }
}
