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

namespace Org.IMotionDaq
{
    public partial class UcDIO : UcRealTimeUI
    {
        public UcDIO()
        {
            InitializeComponent();
        }

        private void FormModuleTest_DIO_Load(object sender, EventArgs e)
        {
            ucDiPanel.Location = new Point(0, 0);
            ucDiPanel.Size = new Size(ClientRectangle.Width / 2, ClientRectangle.Height - tbInfo.Height - 2);
            ucDoPanel.Location = new Point(ClientRectangle.Width / 2, 0);
            ucDoPanel.Size = new Size(ClientRectangle.Width / 2, ClientRectangle.Height - tbInfo.Height - 2);
        }

        [Category("DIO"), Description("编辑DIO名称"), Browsable(true)]
        public bool IsDioNamesEditting
        {
            get { return _isDioNameEdittting; }
            set
            {
                if (_isDioNameEdittting == value)
                    return;
                _isDioNameEdittting = value;
                ucDiPanel.DIOEditting = _isDioNameEdittting;
                ucDoPanel.DIOEditting = _isDioNameEdittting;
            }
        }


        delegate void dgSetDioModule(IPlatModule_DIO module, string[] diNames, string[] doNames);
        public void SetDioModule(IPlatModule_DIO module, string[] diNames, string[] doNames)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new dgSetDioModule(SetDioModule), new object[] { module, diNames, doNames });
                return;
            }
            ucDiPanel.RemoveAllDIO();
            ucDoPanel.RemoveAllDIO();
            if (null == module)
                return;
            for (int i = 0; i < module.DICount; i++)
                ucDiPanel.AddIO(module, i, diNames == null ? null : (diNames.Length > i ? diNames[i] : null));
            for (int i = 0; i < module.DOCount; i++)
                ucDoPanel.AddIO(module, i, doNames == null ? null : (doNames.Length > i ? doNames[i] : null));
        }




        private void FormModuleTest_DIO_SizeChanged(object sender, EventArgs e)
        {
            ucDiPanel.Location = new Point(0, 0);
            ucDiPanel.Size = new Size(ClientRectangle.Width / 2, ClientRectangle.Height - tbInfo.Height - 2);
            ucDoPanel.Location = new Point(ClientRectangle.Width / 2, 0);
            ucDoPanel.Size = new Size(ClientRectangle.Width / 2, ClientRectangle.Height - tbInfo.Height - 2);
        }

        private void UpdateIOStatus()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateIOStatus));
                return;
            }
            ucDiPanel.UpdateIOStatus();//
            ucDoPanel.UpdateIOStatus();
            tbInfo.Text = "IO Auto Flashing " + DateTime.Now.ToString("HH:mm:ss");

        }

        public override void UpdateSrc2UI()
        {
            UpdateIOStatus();
        }

        bool _isDioNameEdittting = false;
    }
}
