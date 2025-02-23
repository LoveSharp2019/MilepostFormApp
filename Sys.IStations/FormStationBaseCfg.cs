﻿using Cell.DataModel;
using Cell.UI;
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
    public partial class FormStationBaseCfg : windowBase
    {
        public FormStationBaseCfg()
        {
            InitializeComponent();
        }
        bool _isFormLoaded = false;
        private void FormStationBaseCfg_Load(object sender, EventArgs e)
        {
            _isFormLoaded = true;
            AdjustStationView();
        }

        void AdjustStationView()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(AdjustStationView));
                return;
            }

            tabControl1.TabPages.Clear();
            if (_station == null)
                return;
            //tabControl1.TabPages.Add()
            //TabPage tabPageDI = new TabPage();
            //tabPageDI.Text = "DI";
            //tabPageDI.Name = "DI";
            //tabControl1.TabPages.Add(tabPageDI);
            ////添加DI界面
            //UcChnNamesInStation ucDiEdit = new UcChnNamesInStation();
            //ucDiEdit.SetStationChnType(_station, NamedChnType.Di);
            //ucDiEdit.Dock = DockStyle.Fill;
            //ucDiEdit.Parent = tabPageDI;
            //tabPageDI.Controls.Add(ucDiEdit);


            //TabPage tabPageDO = new TabPage();
            //tabPageDO.Text = "DO";
            //tabPageDO.Name = "DO";
            //tabControl1.TabPages.Add(tabPageDO);
            ////添加DO界面
            //UcChnNamesInStation ucDoEdit = new UcChnNamesInStation();
            //ucDoEdit.SetStationChnType(_station, NamedChnType.Do);
            //ucDoEdit.Dock = DockStyle.Fill;
            //ucDoEdit.Parent = tabPageDO;
            //tabPageDO.Controls.Add(ucDoEdit);


            //TabPage tabPageAxis = new TabPage();
            //tabPageAxis.Text = "Axis";
            //tabPageAxis.Name = "Axis";
            //tabControl1.TabPages.Add(tabPageAxis);
            //UcChnNamesInStation ucAxisEdit = new UcChnNamesInStation();
            //ucAxisEdit.SetStationChnType(_station, NamedChnType.Axis);
            //ucAxisEdit.Dock = DockStyle.Fill;
            //ucAxisEdit.Parent = tabPageAxis;
            //tabPageAxis.Controls.Add(ucAxisEdit);


            //TabPage tabPageCmpTrig = new TabPage();
            //tabPageCmpTrig.Text = "CmpTrig";
            //tabPageCmpTrig.Name = "CmpTrig";
            //tabControl1.TabPages.Add(tabPageCmpTrig);
            //UcChnNamesInStation ucCmpTrigEdit = new UcChnNamesInStation();
            //ucCmpTrigEdit.SetStationChnType(_station, NamedChnType.CmpTrig);
            //ucCmpTrigEdit.Dock = DockStyle.Fill;
            //ucCmpTrigEdit.Parent = tabPageCmpTrig;
            //tabPageCmpTrig.Controls.Add(ucCmpTrigEdit);

            //TabPage tabPageAI = new TabPage();
            //tabPageAI.Text = "AI";
            //tabPageAI.Name = "AI";
            //tabControl1.TabPages.Add(tabPageAI);
            //UcChnNamesInStation ucAiEdit = new UcChnNamesInStation();
            //ucAiEdit.SetStationChnType(_station, NamedChnType.Ai);
            //ucAiEdit.Dock = DockStyle.Fill;
            //ucAiEdit.Parent = tabPageAI;
            //tabPageAI.Controls.Add(ucAiEdit);


            //TabPage tabPageAO = new TabPage();
            //tabPageAO.Text = "AO";
            //tabPageAO.Name = "AO";
            //tabControl1.TabPages.Add(tabPageAO);
            //UcChnNamesInStation ucAoEdit = new UcChnNamesInStation();
            //ucAoEdit.SetStationChnType(_station, NamedChnType.Ao);
            //ucAoEdit.Dock = DockStyle.Fill;
            //ucAoEdit.Parent = tabPageAI;
            //tabPageAO.Controls.Add(ucAoEdit);


            TabPage tabPageCmr = new TabPage();
            tabPageCmr.Text = "相机";
            tabPageCmr.Name = "相机";
            tabControl1.TabPages.Add(tabPageCmr);
            UcChnNamesInStation ucCmrEdit = new UcChnNamesInStation();
            ucCmrEdit.SetStationChnType(_station, NamedChnType.Camera);
            ucCmrEdit.Dock = DockStyle.Fill;
            ucCmrEdit.Parent = tabPageCmr;
            tabPageCmr.Controls.Add(ucCmrEdit);

            TabPage tabPageLine = new TabPage();
            tabPageLine.Text = "线扫激光";
            tabPageLine.Name = "线扫激光";
            tabControl1.TabPages.Add(tabPageLine);
            UcChnNamesInStation ucLineEdit = new UcChnNamesInStation();
            ucLineEdit.SetStationChnType(_station, NamedChnType.LineScan);
            ucLineEdit.Dock = DockStyle.Fill;
            ucLineEdit.Parent = tabPageLine;
            tabPageLine.Controls.Add(ucLineEdit);



            //TabPage tabPageLight = new TabPage();
            //tabPageLight.Text = "Light";
            //tabPageLight.Name = "Light";
            //tabControl1.TabPages.Add(tabPageLight);
            //UcChnNamesInStation ucLightEdit = new UcChnNamesInStation();
            //ucLightEdit.SetStationChnType(_station, NamedChnType.Light);
            //ucLightEdit.Dock = DockStyle.Fill;
            //ucLightEdit.Parent = tabPageLight;
            //tabPageLight.Controls.Add(ucLightEdit);


            //TabPage tabPageTrig = new TabPage();
            //tabPageTrig.Text = "Trig";
            //tabPageTrig.Name = "Trig";
            //tabControl1.TabPages.Add(tabPageTrig);
            //UcChnNamesInStation ucTrigEdit = new UcChnNamesInStation();
            //ucTrigEdit.SetStationChnType(_station, NamedChnType.Trig);
            //ucTrigEdit.Dock = DockStyle.Fill;
            //ucTrigEdit.Parent = tabPageTrig;
            //tabPageTrig.Controls.Add(ucTrigEdit);



            //TabPage tabPageWorkFlow = new TabPage();
            //tabPageWorkFlow.Text = "WorkFlow";
            //tabPageWorkFlow.Name = "WorkFlow";
            //tabControl1.TabPages.Add(tabPageWorkFlow);
            //UcStationWorkFlowCfg ucWorkFlow = new UcStationWorkFlowCfg();
            //ucWorkFlow.SetStation(_station);
            //ucWorkFlow.Dock = DockStyle.Fill;
            //ucWorkFlow.Parent = tabPageCmr;
            //tabPageWorkFlow.Controls.Add(ucWorkFlow);


            //TabPage tabPagePosition = new TabPage();
            //tabPagePosition.Text = "Position";
            //tabPagePosition.Name = "Position";
            //tabControl1.TabPages.Add(tabPagePosition);
            //UcStationWorkPositionCfg ucWorkPosition = new UcStationWorkPositionCfg();
            //ucWorkPosition.SetStation(_station);
            //ucWorkPosition.Dock = DockStyle.Fill;
            //ucWorkPosition.Parent = tabPagePosition;
            //tabPagePosition.Controls.Add(ucWorkPosition);


            TabPage tpDevChnMapping = new TabPage();
            tpDevChnMapping.Text = "DevChn映射表";
            tpDevChnMapping.Name = "DevChn映射表";
            tabControl1.TabPages.Add(tpDevChnMapping);
            UcStationDevChnNameMapping ucDevChnMapping = new UcStationDevChnNameMapping();
            ucDevChnMapping.SetStation(_station);
            ucDevChnMapping.Dock = DockStyle.Fill;
            ucDevChnMapping.Parent = tpDevChnMapping;
            tpDevChnMapping.Controls.Add(ucDevChnMapping);

            //TabPage tpSysPoolMapping = new TabPage();
            //tpSysPoolMapping.Text = "系统数据项映射表";
            //tpSysPoolMapping.Name = "系统数据项映射表";
            ////tpDevChnMapping.Tag = ""
            //tabControl1.TabPages.Add(tpSysPoolMapping);
            //UcStationBaseSPAliasEdit ucSPMapping = new UcStationBaseSPAliasEdit();
            //ucSPMapping.SetStation(_station);
            //ucSPMapping.Dock = DockStyle.Fill;
            //ucSPMapping.Parent = tpSysPoolMapping;
            //tpSysPoolMapping.Controls.Add(ucSPMapping);


            //TabPage tpCfg = new TabPage();
            //tpCfg.Text = "配置项";
            //tpCfg.Name = "配置项";
            //tabControl1.TabPages.Add(tpCfg);

            //FormStationBaseXCfgEdit fmCfg = new FormStationBaseXCfgEdit();
            //fmCfg.SetStation(_station);

            //fmCfg.FormBorderStyle = FormBorderStyle.None;
            //fmCfg.TopLevel = false;
            //fmCfg.Dock = DockStyle.Fill;
            //fmCfg.Parent = tpCfg;
            UpdateCfg2View();

        }

        void OnBtShowStationCfgDialog(object sender, EventArgs e) //显示工站自定义配置界面
        {
            _station.ShowCfgDialog();
        }

        public void SetStation(IStationBase station)
        {
            _station = station;
            if (_isFormLoaded)
                AdjustStationView();
        }

        IStationBase _station = null;
        void UpdateCfg2View()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateCfg2View));
                return;
            }


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage tp = tabControl1.SelectedTab;
            if (null == tp)
                return;
            if (tabControl1.SelectedTab.HasChildren)
                foreach (Control item in tabControl1.SelectedTab.Controls)
                    if (item is Form)
                        item.Visible = true;
        }
    }
}
