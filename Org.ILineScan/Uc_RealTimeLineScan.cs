using Cell.DataModel;
using Cell.Interface;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.ILineScan
{
    /// <summary>
    /// 通用相机测试界面  需要重新设置界面
    /// </summary>
    public partial class Uc_RealTimeLineScan : UcRealTimeUI
    {

        public Uc_RealTimeLineScan()
        {
            InitializeComponent();
        }

        private void UcCmr_Load(object sender, EventArgs e)
        {
            AdjustView();

            grfPicBox = picBox.CreateGraphics();//用于显示BitMap的
            picWnd = picBox.Handle;
        }

        enum ImgShowMode //显示模式
        {
            sdk, //SDK中的显示功能
            halcon, //将图片转为Halcon对象后显示
            bitmap,//将图片专为bitmap对象后再显示
        }
        IPlatDevice_LineScan _cmr = null;
        HWindow hcWnd = new HWindow(); //用于Halcon显示
        Graphics grfPicBox = null;
        Bitmap _currBmp = null;
        HObject _currHo = null;
        IPlat_Image _currImage = null;
        ImgShowMode _imgShowMode = ImgShowMode.sdk;
        IntPtr picWnd = IntPtr.Zero;//用于供IJFImage显示图像的句柄


        /// <summary>
        /// 更新整个界面（在改变相机对象之后需要调用）
        /// </summary>
        void AdjustView()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(AdjustView));
                return;
            }

            if (null == _cmr)
            {
                Enabled = false;
                ShowTips("相机未设置");
                return;

            }
            Enabled = true;
            btDev.Enabled = true;
            UpdateSrc2UI();
        }

        public void SetLineScan(IPlatDevice_LineScan cmr)
        {
            _cmr = cmr;
            if (Created)
                AdjustView();
        }


        public override void UpdateSrc2UI()//JFRealtimeUI's API
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateSrc2UI));
                return;
            }
            if (null == _cmr)
                return;

            if (!_cmr.IsDeviceOpen)         
            {
                btDev.Text = "打开相机";
                cbImgDispMode.Enabled = false; //
                cbImgDispMode.SelectedIndex = 2; //默认使用bitmap模式显示

                btGrab.Enabled = false;
                btGrab.Text = "开始采集";
                btn_getJob.Enabled =
                btn_setJob.Enabled =
                txt_jobname.Enabled =
                btGrabOne.Enabled = false;

                cbImgFileFormat.Enabled = false;
                btSave.Enabled = false;
                return;
            }
            btDev.Text = "关闭相机";


            btn_getJob.Enabled =
            btn_setJob.Enabled =
            txt_jobname.Enabled =
            btGrab.Enabled = true;
            cbImgDispMode.Enabled = !_cmr.IsGrabbing;
            if (_cmr.IsGrabbing) //采集图像中
            {
                btGrab.Text = "停止采集";
                btGrabOne.Enabled = false;
            }
            else //此时未采集
            {
                btGrab.Text = "开始采集";
                btGrabOne.Enabled = true;
            }

            if (cbImgDispMode.SelectedIndex == 0) //使用SDK模式显示图片
            {
                //添加代码
            }

            cbImgFileFormat.Enabled = _currImage != null;
            btSave.Enabled = _currImage != null;
        }

        int maxTips = 100;
        delegate void dgShowTips(string txt);
        void ShowTips(string txt)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new dgShowTips(ShowTips), new object[] { txt });
                return;
            }

            if (this.rbTips.Lines.Length > maxTips)
            {
                string[] sLines = rbTips.Lines;
                string[] sNewLines = new string[maxTips];
                Array.Copy(sLines, this.rbTips.Lines.Length - maxTips, sNewLines, 0, maxTips);
                rbTips.Lines = sNewLines;
            }

            this.rbTips.AppendText(string.Format("{0} \r\n", txt));

            rbTips.Select(rbTips.TextLength, 0); //滚到最后一行
            rbTips.ScrollToCaret();//滚动到控件光标处 
        }

        private void btClearTips_Click(object sender, EventArgs e)
        {
            rbTips.Text = "";
        }

        /// <summary>打开/关闭相机</summary>
        private void btDev_Click(object sender, EventArgs e)
        {
            if (null == _cmr)
                return;
            if (!_cmr.IsDeviceOpen)
            {
                int err = _cmr.OpenDevice();
                if (err != 0)
                {
                    MessageBox.Show("打开相机失败，错误信息 :" + _cmr.GetErrorInfo(err));
                    return;
                }
                UpdateSrc2UI();
                ShowTips("相机已打开/连接！");
            }
            else
            {
                int err = 0;
                if (_cmr.IsGrabbing)
                {
                    err = _cmr.StopGrab();
                    if (0 == err)
                    {
                        ShowTips("相机已停止图像采集");
                        return;
                    }
                    else
                    {
                        ShowTips("未能停止相机采集,错误信息：" + _cmr.GetErrorInfo(err));
                        MessageBox.Show("未能停止相机采集,错误信息：" + _cmr.GetErrorInfo(err));
                        return;
                    }

                }

                err = _cmr.CloseDevice();
                if (err != 0)
                {
                    ShowTips("关闭相机失败，错误信息 :" + _cmr.GetErrorInfo(err));
                    return;
                }
                ShowTips("相机已关闭/断开！");
            }

        }

        void _CmrFrameCallback(IPlatDevice_Camera cmr, IPlat_Image frame) //相机回调函数
        {
            ShowImg(frame);
        }


        void ShowHalconImg(HObject hoImg, int picWidth, int picHeight)
        {

            if (null == hoImg)
            {
                ShowTips("显示Halcon图像失败！图像对象为空");
                return;
            }
            _imgShowMode = ImgShowMode.halcon;
            HOperatorSet.SetPart(hcWnd, 0, 0, picHeight - 1, picWidth - 1);// ch: 使图像显示适应窗口大小 || en: Make the image adapt the window size

            HOperatorSet.DispObj(hoImg, hcWnd);// ch 显示 || en: display
            if (hoImg == _currHo)
                return;
            if (_currHo != null)
            {
                _currHo.Dispose();
                _currHo = null;
            }
            _currHo = hoImg;

        }

        void ShowBitmap(Bitmap bmp) //待实现
        {
            if (null == bmp)
                return;
            grfPicBox.DrawImage(bmp, new Rectangle(0, 0, picBox.Width, picBox.Height)); //

        }


        /// <summary>
        /// 开始/停止采集图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGrab_Click(object sender, EventArgs e)
        {
            if (null == _cmr)
            {
                MessageBox.Show("开始采集失败:相机未设置");
                return;
            }
            int err = 0;
            if (_cmr.IsGrabbing)
            {
                err = _cmr.StopGrab();
                if (0 == err)
                {
                    ShowTips("相机已停止图像采集");
                    UpdateSrc2UI();
                    return;
                }
                else
                {
                    ShowTips("未能停止相机采集,错误信息：" + _cmr.GetErrorInfo(err));
                    MessageBox.Show("未能停止相机采集,错误信息：" + _cmr.GetErrorInfo(err));
                    return;
                }

            }
            else
            {
                err = _cmr.StartGrab();
                if (0 != err)
                {

                    ShowTips("开启相机采集失败,错误信息：" + _cmr.GetErrorInfo(err));
                    MessageBox.Show("开启相机采集失败,错误信息：" + _cmr.GetErrorInfo(err));
                    return;

                }

                if (0 == cbImgDispMode.SelectedIndex)
                {
                    hcWnd.CloseWindow();
                    if (null != _currHo)
                    {
                        _currHo.Dispose();
                        _currHo = null;
                    }
                    _imgShowMode = ImgShowMode.sdk;

                }
                else if (1 == cbImgDispMode.SelectedIndex) //halcon显示
                {
                    hcWnd.OpenWindow(picBox.Location.X, picBox.Location.Y, picBox.Width, picBox.Height, picBox.Handle, "visible", "");

                    _imgShowMode = ImgShowMode.halcon;
                    if (_currBmp != null)
                    {
                        _currBmp.Dispose();
                        _currBmp = null;
                    }
                }
                else if (2 == cbImgDispMode.SelectedIndex)//bitmap显示
                {
                    hcWnd.CloseWindow();
                    if (null != _currHo)
                    {
                        _currHo.Dispose();
                        _currHo = null;
                    }
                    _imgShowMode = ImgShowMode.bitmap;
                }

                ShowTips("相机开始图像采集...");
                UpdateSrc2UI();
                return;


            }
        }
        /// <summary>
        /// 采集一张图片并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGrabOne_Click(object sender, EventArgs e)
        {
            if (null == _cmr)
            {
                ShowTips("操作失败，相机未设置");
                return;
            }
            IPlat_Image img = null;
            int err = _cmr.GetOneImg(out img);
            if (err != 0)
            {
                ShowTips("抓图失败，错误信息:" + _cmr.GetErrorInfo(err) + " " + DateTime.Now.ToString("HH:mm:ss:ms"));

                return;
            }

            ShowTips("抓图操作成功，开始显示图像...");
            ShowImg(img);
        }


        delegate void dgShowImg(IPlat_Image img);
        void ShowImg(IPlat_Image img)
        {
            if (InvokeRequired)
            {
                Invoke(new dgShowImg(ShowImg), new object[] { img });
                return;
            }
            int err = 0;
            if (_imgShowMode == ImgShowMode.sdk) // 使用SDK内部自带的图片显示功能
            {
                picWnd = picBox.Handle;
                err = img.DisplayTo(picWnd);
                if (err != 0)
                {
                    ShowTips("SDK显示图像失败，错误信息:" + img.GetErrorInfo(err) + " " + DateTime.Now.ToString("HH:mm:ss:ms"));
                }
                else
                    ShowTips("SDK显示图像完成");
            }
            else if (_imgShowMode == ImgShowMode.halcon) //Halcon显示图片
            {
                if (_currHo != null)
                {
                    _currHo.Dispose();
                    _currHo = null;
                }
                object hoImage = null;
                ShowTips("Halcon转换开始：" + DateTime.Now.ToString("HH:mm:ss:ms"));
                err = img.GenHalcon(out hoImage);
                if (err != 0)
                {
                    ShowTips("显示图片失败，未能将图片转化为Halcon对象,错误信息：" + img.GetErrorInfo(err));
                    return;
                }
                ShowTips("Halcon转换完成：" + DateTime.Now.ToString("HH:mm:ss:ms"));
                ShowHalconImg((HObject)hoImage, img.PicWidth, img.PicHeight);
                _currHo = (HObject)hoImage;
                ShowTips("图像显示完成：" + DateTime.Now.ToString("HH:mm:ss:ms"));
            }
            else if (_imgShowMode == ImgShowMode.bitmap) //
            {

                ShowTips("Bitmap转换开始：" + DateTime.Now.ToString("HH:mm:ss:ms"));
                Bitmap bmp = null;
                err = img.GenBmp(out bmp);
                if (err != 0)
                {
                    ShowTips("显示图片失败，未能将图片转化为Bitmap对象,错误信息：" + img.GetErrorInfo(err));

                }
                else
                {
                    ShowTips("Bitmap转换完成：" + DateTime.Now.ToString("HH:mm:ss:ms"));
                    ShowBitmap(bmp);////
                    if (null != _currBmp)
                    {

                        _currBmp.Dispose();
                        _currBmp = bmp;
                    }
                }
            }
            if (null != _currImage)
            {
                _currImage.Dispose();
            }
            _currImage = img;
        }


        private void btSaveImage_Click(object sender, EventArgs e)
        {
            if (null == _cmr)
            {
                ShowTips("操作失败，相机未设置");
                return;
            }
            IPlat_Image img = _currImage;
            if (null == img)
            {

                MessageBox.Show("保存图像失败:当前未采集图像");
                return;
            }
            if (cbImgFileFormat.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择文件格式！");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            switch (cbImgFileFormat.SelectedIndex)
            {
                case 0:
                    sfd.Filter = "BMP files(*.BMP)| *.BMP ";
                    break;
                case 1:
                    sfd.Filter = "JPG files(*.JPG) | *.JPG ";
                    break;
                case 2:
                    sfd.Filter = " PNG files(*.PNG) | *.PNG ";
                    break;
                case 3:
                    sfd.Filter = "TIF files(*.TIF) | *.TIF";
                    break;
                default:
                    throw new Exception("ImgFileFormat is not selected!");
                    //break;
            }

            sfd.FileName = "保存";//设置默认文件名
            sfd.DefaultExt = "BMP";//设置默认格式（可以不设）
            sfd.AddExtension = true;//设置自动在文件名中添加扩展名
            sfd.CheckFileExists = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                int err = img.Save(sfd.FileName, (IPlatImgSuffixType)cbImgFileFormat.SelectedIndex);
                if (err != 0)
                    MessageBox.Show("保存文件失败，ErrorInfo:" + img.GetErrorInfo(err));
                else
                    ShowTips("图片已保存至文件:" + sfd.FileName);

            }

        }

        private void UcCmr_SizeChanged(object sender, EventArgs e)
        {
            if (cbImgDispMode.SelectedIndex == 1)
            {
                hcWnd.OpenWindow(picBox.Location.X, picBox.Location.Y, picBox.Width, picBox.Height, picBox.Handle, "visible", "");
                if (_currHo != null)
                    HOperatorSet.DispObj(_currHo, hcWnd);
            }

        }

        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            if (_imgShowMode == ImgShowMode.bitmap)
            {
                if (null != _currBmp)
                {
                    e.Graphics.DrawImage(_currBmp, new Rectangle(0, 0, picBox.Width, picBox.Height));
                    return;
                }
            }
            else if (_imgShowMode == ImgShowMode.halcon)
            {
                //hcWnd.CloseWindow();
                //hcWnd.OpenWindow(picBox.Location.X, picBox.Location.Y, picBox.Width, picBox.Height, picBox.Handle, "visible", "");
                //if (null != _currHo)
                //    HOperatorSet.DispObj(_currHo, hcWnd);
            }
            else if (_imgShowMode == ImgShowMode.sdk)
            {
                if (null != _currImage)
                    _currImage.DisplayTo(picBox.Handle);
            }
        }


        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
        }



        private void UcCmr_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                AdjustView();
            else
            {
                //if (null != _cmr)
                //    _cmr.RemoveAcqFrameCallback(_CmrFrameCallback);
            }
        }



        //画图区域大小改变
        private void picBox_SizeChanged(object sender, EventArgs e)
        {

        }

        private void btn_getJob_Click(object sender, EventArgs e)
        {
            int err = _cmr.GetSeneorJob(out string JobName);
            if (err == 0)
            {
                MessageBox.Show(string.Format("当前设置的Job【{0}】", JobName));
            }
            else
                MessageBox.Show(string.Format("获取失败错误码：【{0}】", err));
        }

        private void btn_setJob_Click(object sender, EventArgs e)
        {
            string JobName = txt_jobname.Text.Trim();
            int err = _cmr.SetSeneorJob(JobName);
            if (err == 0)
            {
                MessageBox.Show(string.Format("设置的Job【{0}】 成功", JobName));
            }
            else
                MessageBox.Show(string.Format("设置Job失败错误码：【{0}】", err));
        }
    }
}
