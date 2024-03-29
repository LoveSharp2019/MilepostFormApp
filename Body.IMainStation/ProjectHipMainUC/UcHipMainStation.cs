using Cell.Interface;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Body.IMainStation.ProjectHipMainUC
{
    public partial class UcHipMainStation : UserControl
    {
        public static string CMC_ShowJFImage = "ShowIPlat_Image"; //显示IJFImage对象

        HWindow hcWnd = new HWindow(); //用于Halcon显示

        public UcHipMainStation()
        {
            InitializeComponent();
            // 将Halcon窗口与Panel控件关联
            // hcWnd.SetPart(pnl_hwindow, 0, 0, -1, -1);         
        }


        UcHipMainStationVM _ms = null;
        public void SetMainStation(UcHipMainStationVM ms)
        {
            _ms = ms;
        }

        string CMC_ShowImage = "ShowImage";

        /// <summary>
        /// 客制化显示的消息 
        /// </summary>
        /// <param name="msgCategory"></param>
        /// <param name="msgParams"></param>
        public void OnCustomizeMsg(string msgCategory, object[] msgParams)
        {
            BeginInvoke(new Action(() =>
            {
                if (msgCategory == CMC_ShowImage) //显示一个IJFImage对象
                {                    
                    object ob;
                    IPlat_Image img = msgParams[0] as IPlat_Image;
                    if (null == img)
                        return;
                    int err = img.GenHalcon(out ob);
                    if (err != 0)
                        return;
                    ShowHalconImg((HObject)ob, img.PicWidth, img.PicHeight);
                    img.Dispose();
                }
            }));
        }

        void ShowHalconImg(HObject hoImg, int picWidth, int picHeight)
        {
            HOperatorSet.ClearWindow(hcWnd);

            HOperatorSet.SetPart(hcWnd, 0, 0, picHeight - 1, picWidth - 1);// ch: 使图像显示适应窗口大小 || en: Make the image adapt the window size

            HOperatorSet.DispObj(hoImg, hcWnd);// ch 显示 || en: display          
        }

        private void pnl_hwindow_SizeChanged(object sender, EventArgs e)
        {
            hcWnd.CloseWindow();
            HOperatorSet.SetWindowAttr("background_color","sky blue");
            hcWnd.OpenWindow(0, 0, pnl_hwindow.Width, pnl_hwindow.Height, pnl_hwindow.Handle, "visible", "");
           
        }
    }
}
