using Cell.DataModel;
using Cell.Interface;
using Sys.IStations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Body.IMainStation.ProjectHipMainUC
{
    public class UcHipMainStationVM : IMainStationBase
    {
       
        // 客制化主界面
        UcHipMainStation _uiPanel = new UcHipMainStation();
        public override string AppName { get { return "Hip 尺寸机器"; } }

        public UcHipMainStationVM()
        {
            _uiPanel.SetMainStation(this);
            AppendUIPanel(_uiPanel);
            (UIPanel as UcMainStationBasePanel).ShowPart = MSShowPart.None;

            WorkStatus = IWorkStatus.UnStart;
            IsAlarming = false;

        }


        #region UI 面板

        /// <summary>
        /// 设备简介面板,用于显示设备简介，包含图片和文字信息等
        /// </summary>
        public override Control BriefPanel { get; }

        /// <summary>
        /// 将一个自定义的界面控件粘到MainStationBase 提供的Panel中
        /// </summary>
        /// <param name="ctrl"></param>
        protected void AppendUIPanel(Control ctrl)
        {
            (UIPanel as UcMainStationBasePanel).AppendCustomUIPanel(ctrl);
        }


        #endregion

        #region CallBack

        public override void OnStationWorkStatusChanged(IPlatStation station, IWorkStatus currWorkStatus)
        {
            base.OnStationWorkStatusChanged(station, currWorkStatus);

            string[] allEnableStationNames = AppHubCenter.Instance.StationMgr.AllEnabledStationNames();
            if (currWorkStatus == IWorkStatus.AbortExit || currWorkStatus == IWorkStatus.ErrorExit || currWorkStatus == IWorkStatus.ExceptionExit)
            {
                foreach (string stName in allEnableStationNames)
                {
                    IPlatStation st = AppHubCenter.Instance.StationMgr.GetStation(stName);
                    if (st != station && IStationBase.IsWorkingStatus(st.CurrWorkStatus))
                        st.Stop(500);
                }
                IsAlarming = true;
                WorkStatus = IWorkStatus.ErrorExit;
            }
        }

        /// <summary>
        /// 处理工站发来的其他定制化的消息
        /// </summary>
        /// <param name="station"></param>
        /// <param name="msg"></param>
        public override void OnStationCustomizeMsg(IPlatStation station, string msgCategory, object[] msgParams)
        {
            _uiPanel.OnCustomizeMsg(msgCategory, msgParams);
        }

        public override void OnStationTxtMsg(IPlatStation station, string msgInfo)
        {
            //   _uiPanel.OnTxtMsg(msgInfo);
        }

        /// <summary>
        ///  绑定产品结束信号
        /// </summary>
        /// <param name="station"></param>
        /// <param name="passCount"></param>
        /// <param name="passIDs"></param>
        /// <param name="ngCount"></param>
        /// <param name="ngIDs"></param>
        /// <param name="ngInfo"></param>
        public override void OnStationProductFinished(IPlatStation station, int passCount, string[] passIDs, int ngCount, string[] ngIDs, string[] ngInfo)
        {
            //if (UIPanel is UcMainStationBasePanel)
            //{
            //    (UIPanel as UcMainStationBasePanel).ProductDone(passCount, ngCount);
            //}

            //_uiPanel.OnProductFinished(passCount, passIDs, ngCount, ngIDs, ngInfo);
        }

        // 展示工站信息
        public void ShowStationMsg(object ob, string msg)
        {
            // _uiPanel.OnTxtMsg(msg);
        }

        #endregion

        /// <summary>
        /// 手动测试界面
        /// </summary>
        public override Control TestPanel
        {
            get { return null; }
        }
    }
}
