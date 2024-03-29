using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tissue.UI
{
    public partial class UcFileEdit : UserControl
    {
        //
        public string _PubDir = "\\attachCfg";

        public Action<string> UcFileEdit_ValueChange;
        public UcFileEdit()
        {
            InitializeComponent();

        }

        //文件路径
        public string FilePath
        {
            get { return this.ucTextBoxPop1.Text; }
            set
            {
                if (this.ucTextBoxPop1.Text != value)
                    this.ucTextBoxPop1.BeginInvoke(
                        new Action(() =>
                        {
                            this.ucTextBoxPop1.Text = value;
                            UcFileEdit_ValueChange?.Invoke(value);
                        }));
            }
        }


        /// <summary>
        /// 这里创建的路径 应该都是相对路劲 如果想要绝对路劲 需要手动复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn1_Click(object sender, EventArgs e)
        {

            string currentDirectory = Environment.CurrentDirectory + _PubDir;
            if (!Directory.Exists(currentDirectory))
                Directory.CreateDirectory(currentDirectory);

            OpenFileDialog openFileDialog = new OpenFileDialog(); //创建打开文件对话框实例

            if (openFileDialog.ShowDialog() == DialogResult.OK) //显示对话框并判断用户点击了确定按钮
            {
                string selectedFilePath = openFileDialog.FileName; //获取所选文件的完整路径

                string currentFile = Environment.CurrentDirectory + _PubDir + "\\" + openFileDialog.SafeFileName; // 当前工作目录

                // 将文件复制到 相对文件夹
                if (!File.Exists(currentFile))
                    File.Copy(selectedFilePath, currentFile);
                else
                {
                    DialogResult dialogResult = MessageBox.Show("配置文件已存在，点击确认覆盖旧文件", "提示", MessageBoxButtons.OKCancel);

                    if (dialogResult == DialogResult.OK)
                        File.Copy(selectedFilePath, currentFile, true);
                }

                FilePath = "attachCfg\\" + openFileDialog.SafeFileName;
            }
        }

        private static string GetRelativePath(string basePath, string absolutePath)
        {
            Uri baseUri = new Uri(basePath);
            Uri absoluteUri = new Uri(absolutePath);

            Uri relativeUri = baseUri.MakeRelativeUri(absoluteUri);

            return Uri.UnescapeDataString(relativeUri.ToString()).Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
