using Cell.Interface;
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
using Tissue.UI;

namespace Org.IMotionDaq
{
    public partial class FormDios : windowBase
    {
        public FormDios()
        {
            InitializeComponent();
        }

        private void FormDios_Load(object sender, EventArgs e)
        {
            UpdateModleStatus();
        }
       
        List<IPlatModule_DIO> _lstModules = new List<IPlatModule_DIO>();
     

        public void ClearModules()
        {
            _lstModules.Clear();
            tabCtrl.TabPages.Clear();
        }
        public void AddModule(IPlatModule_DIO module, string moduleName)
        {
            if (null == module)
                return;
            if (_lstModules.Contains(module))
                return;

            if (null == moduleName)
                moduleName = "DIO";
            TabPage tp = new TabPage();
            tabCtrl.TabPages.Add(tp);
            UcDIO uc = new UcDIO();
            uc.Dock = DockStyle.Fill;
            uc.Parent = tp;
            uc.Visible = true;
            uc.SetDioModule(module, null, null);
            tp.Text = moduleName;
            tp.Name = moduleName;
            tp.Controls.Add(uc);
            _lstModules.Add(module);

        }

        public void UpdateModleStatus()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateModleStatus));
                return;
            }
            if (_lstModules.Count == 0)
                return;
            if (tabCtrl.SelectedIndex < 0)
                return;
            UcDIO uc = tabCtrl.TabPages[tabCtrl.SelectedIndex].Controls[0] as UcDIO;
            uc.UpdateSrc2UI();
        }
    }
}
