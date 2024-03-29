using Cell.DataModel;
using Cell.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.Interface
{
    public interface IPlatMainStation
    {
        string AppName { get; }
        /// <summary>
        /// 人机交互面板，用于在主窗口中显示
        /// </summary>
        /// <returns></returns>
        Control UIPanel { get; }


        /// <summary>
        /// 设备简介面板
        /// </summary>
        Control BriefPanel { get; }


        /// <summary>
        /// 向用户提供一个可以设置参数的界面 ， 
        /// 如果不需实现，可以返回null
        /// </summary>
        Control ConfigPanel { get; }



        /// <summary>
        /// 向用户提供一个调试界面
        /// 如果不需实现，返回null
        /// </summary>
        Control TestPanel { get; }


        /// <summary>
        /// 获取当前工作状态
        /// </summary>
        IWorkStatus WorkStatus { get; }

        bool IsAlarming { get; }

        /// <summary>
        /// 重置（消除）报警信号
        /// </summary>
        bool ClearAlarming(out string errorInfo);

        /// <summary>
        /// 获取报警信息
        /// </summary>
        /// <returns></returns>
        string GetAlarmInfo();
        bool Start(out string errorInfo);//开始运行
        /// <summary>停止运行</summary>
        bool Stop(out string errorInfo);
        /// <summary>暂停</summary>
        bool Pause(out string errorInfo);
        /// <summary>从暂停中恢复运行</summary>
        bool Resume(out string errorInfo);
        /// <summary>
        /// 所有工站复位
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        bool Reset(out string errorInfo);

        /// <summary>
        /// 处理工站状态改变
        /// </summary>
        /// <param name="station"></param>
        /// <param name="currWorkStatus"></param>
        void OnStationWorkStatusChanged(IPlatStation station, IWorkStatus currWorkStatus);

        /// <summary>
        ///  处理工站的业务状态发生改变
        /// </summary>
        /// <param name="station"></param>
        /// <param name="currCustomStatus"></param>
        void OnStationCustomStatusChanged(IPlatStation station, int currCustomStatus);

        /// <summary>
        /// 产品加工完成消息
        /// </summary>
        /// <param name="station">消息发送者</param>
        /// <param name="PassCount">本次生产完成的成品数量</param>
        /// <param name="NGCount">本次生产的次品数量</param>
        /// <param name="NGInfo">次品信息</param>
        void OnStationProductFinished(IPlatStation station, int passCount, string[] passIDs, int ngCount, string[] ngIDs, string[] ngInfo);

        /// <summary>
        /// 处理工站发来的其他定制化的消息
        /// </summary>
        /// <param name="station"></param>
        /// <param name="msg"></param>
        void OnStationCustomizeMsg(IPlatStation station, string msgCategory, object[] msgParam);

        /// <summary>
        /// 处理工站文本消息
        /// </summary>
        /// <param name="station"></param>
        /// <param name="msgInfo"></param>
        void OnStationTxtMsg(IPlatStation station, string msgInfo);

    }
}
