using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tissue.UI
{
    public partial class UcFolderEdit : UserControl
    {

        // 创建一个delegate类型，该类型定义了需要在UI线程上调用的方法
        private delegate void UpdateControlDelegate(string value);
        public Action<string> UcFolderEdit_ValueChange;
        public UcFolderEdit()
        {
            InitializeComponent();
        }

        //文件路径
        public string FolderPath
        {
            get { return this.ucTextBoxPop1.Text; }
            set
            {
                if (this.ucTextBoxPop1.Text != value)
                    this.ucTextBoxPop1.BeginInvoke(
                        new Action(() =>
                        {
                            this.ucTextBoxPop1.Text = value;
                            UcFolderEdit_ValueChange?.Invoke(value);
                        }));
            }
        }


        private void iconBtn1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            // 设置对话框标题
            folderDialog.Description = "选择文件夹";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                if (folderDialog.SelectedPath != "")
                    FolderPath = folderDialog.SelectedPath;
            }
        }
    }
}
