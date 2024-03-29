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

namespace Org.ICamera
{
    /// <summary>
    /// 通用相机测试界面  需要重新设置界面
    /// </summary>
    public partial class Uc_RealTimeCmr : UcRealTimeUI
    {

        public Uc_RealTimeCmr()
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
        IPlatDevice_Camera _cmr = null;
        HWindow hcWnd = new HWindow(); //用于Halcon显示
        Graphics grfPicBox = null;
        Bitmap _currBmp = null;
        HObject _currHo = null;
        IPlat_Image _currImage = null;
        ImgShowMode _imgShowMode = ImgShowMode.sdk;
        IntPtr picWnd = IntPtr.Zero;//用于供IJFImage显示图像的句柄


        bool _isTrigModeUpdating = false;//正在向界面上更新参数
        bool _isBuffCountUpdating = false;
        bool _isExposureUpdating = false;
        bool _isGainUpdating = false;
        bool _isReverseXUpdating = false;
        bool _isReverseYUpdating = false;

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

        public void SetCamera(IPlatDevice_Camera cmr)
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
                cbTrigSrc.Enabled = false;
                _isTrigModeUpdating = true;
                cbTrigSrc.Text = "未知";
                _isTrigModeUpdating = false;

                numBuffSize.Enabled = false;
                _isBuffCountUpdating = true;
                numBuffSize.Value = 0;
                _isBuffCountUpdating = false;
                numExposure.Enabled = false;
                _isExposureUpdating = true;
                numExposure.Value = 0;
                _isExposureUpdating = false;
                numGain.Enabled = false;
                _isGainUpdating = true;
                numGain.Value = 0;
                _isGainUpdating = false;
                chkReverseX.Enabled = false;
                _isReverseXUpdating = true;
                chkReverseX.Checked = false;
                _isReverseXUpdating = false;
                chkReverseY.Enabled = false;
                _isReverseYUpdating = true;
                chkReverseY.Checked = false;
                _isReverseYUpdating = false;
                btGrab.Enabled = false;
                btGrab.Text = "开始采集";
                chkContinueGrab.Enabled = false;
                chkContinueGrab.Checked = false;
                chkCallBack.Enabled = false;
                chkCallBack.Checked = false;
                btGrabOne.Enabled = false;
                btSoftwareTrig.Enabled = false;

                cbImgFileFormat.Enabled = false;
                btSave.Enabled = false;
                return;
            }
            btDev.Text = "关闭相机";
            numBuffSize.Enabled = true;
            numExposure.Enabled = true;
            numGain.Enabled = true;
            chkReverseX.Enabled = true;
            chkReverseY.Enabled = true;


            int err = 0;
            cCmrTrigMode tm = cCmrTrigMode.disable;
            if (!cbTrigSrc.Focused)
            {
                _isTrigModeUpdating = true;
                err = _cmr.GetTrigMode(out tm);
                if (err != 0)
                    cbTrigSrc.Text = "未知";
                else
                {
                    cbTrigSrc.SelectedIndex = (int)tm;
                    cbTrigSrc.Text = cbTrigSrc.Items[(int)tm].ToString();

                }
                _isTrigModeUpdating = false;
            }

            if (!numBuffSize.Focused)
            {
                _isBuffCountUpdating = true;
                //int bf = 0;
                //err = _cmr.GetBuffSize(out bf);
                //numBuffSize.Value = bf;
                //_isBuffCountUpdating = false;
            }

            if (!numExposure.Focused)
            {
                _isExposureUpdating = true;
                double ex = 0;
                //err = _cmr.GetExposure(out ex);
                //numExposure.Value = Convert.ToDecimal(ex);
                //_isExposureUpdating = false;
            }

            if (!numGain.Focused)
            {
                _isGainUpdating = true;
                double gn = 0;
                //err = _cmr.GetGain(out gn);
                //numGain.Value = Convert.ToDecimal(gn);
                //_isGainUpdating = false;
            }

            if (!chkReverseX.Focused)
            {
                //_isReverseXUpdating = true;
                //bool rx = false;
                //err = _cmr.GetReverseX(out rx);
                //chkReverseX.Checked = rx;
                //_isReverseXUpdating = false;
            }

            if (!chkReverseY.Focused)
            {
                //_isReverseYUpdating = true;
                //bool ry = false;
                //err = _cmr.GetReverseY(out ry);
                //chkReverseY.Checked = ry;
                //_isReverseYUpdating = false;
            }

            btGrab.Enabled = true;
            cbImgDispMode.Enabled = !_cmr.IsGrabbing;
            if (_cmr.IsGrabbing) //采集图像中
            {
                cbTrigSrc.Enabled = false; //采集图像时不能更改触发模式
                btGrab.Text = "停止采集";
                chkContinueGrab.Enabled = false;
                chkCallBack.Enabled = false;
                if (tm == cCmrTrigMode.disable)
                {
                    if (chkContinueGrab.Checked)
                        btGrabOne.Enabled = false;
                    else
                        btGrabOne.Enabled = true;
                    btSoftwareTrig.Enabled = false;
                }
                else
                {
                    //if (_cmr.IsRegistAcqFrameCallback) //已经注册了回调函数
                    //    btGrabOne.Enabled = false;
                    //else
                    //    btGrabOne.Enabled = true;
                    if (tm == cCmrTrigMode.software && !chkContinueGrab.Checked)
                        btSoftwareTrig.Enabled = true;
                    else
                        btSoftwareTrig.Enabled = false;
                }


            }
            else //此时未采集
            {
                cbTrigSrc.Enabled = true;
                btGrab.Text = "开始采集";
                chkContinueGrab.Enabled = true;
                chkCallBack.Enabled = true;

                btGrabOne.Enabled = false;
                btSoftwareTrig.Enabled = false;
            }




            if (cbImgDispMode.SelectedIndex == 0) //使用SDK模式显示图片
            {
                //添加代码
            }


            cbImgFileFormat.Enabled = _currImage != null;
            btSave.Enabled = _currImage != null;

        }


        //void 




        int maxTips = 100;
        delegate void dgShowTips(string txt);
        void ShowTips(string txt)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new dgShowTips(ShowTips), new object[] { txt });
                return;
            }
            rbTips.AppendText(txt + "\n");
            string[] lines = rbTips.Text.Split('\n');
            if (lines.Length >= maxTips)
            {
                int rmvChars = 0;
                for (int i = 0; i < lines.Length - maxTips; i++)
                    rmvChars += lines[i].Length + 1;
                rbTips.Text = rbTips.Text.Substring(rmvChars);
            }
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
                    if (_isAutoGrabRunning)
                        _StopAutoGrab();
                    err = _cmr.StopGrab();
                    if (0 == err)
                    {
                        ShowTips("相机已停止图像采集");
                        //UpdateSrc2UI();
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

        /// <summary>触发模式改变</summary>
        private void cbTrigSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isTrigModeUpdating)
                return;
            if (_cmr == null)
            {
                MessageBox.Show("操作失败，相机未设置！");
                cbTrigSrc.Text = "Error";
                return;
            }

            int err = _cmr.SetTrigMode((cCmrTrigMode)cbTrigSrc.SelectedIndex);
            if (0 != err)
            {
                MessageBox.Show("操作失败，ErrorInfo:" + _cmr.GetErrorInfo(err));
                return;
            }
            ShowTips("触发模式设置成功:" + (cCmrTrigMode)cbTrigSrc.SelectedIndex);

        }

        /// <summary>X方向镜像使能/禁用</summary>
        private void chkReverseX_CheckedChanged(object sender, EventArgs e)
        {
            if (_isReverseXUpdating)
                return;
            if (null == _cmr)
            {
                ShowTips("操作失败，相机未设置");
                return;
            }
            //int err = _cmr.SetReverseX(chkReverseX.Checked);
            //if (0 == err)
            //{
            //    ShowTips((chkReverseX.Checked ? "使能" : "禁用") + "X镜像成功");
            //    return;
            //}
            //else
            //{
            //    MessageBox.Show((chkReverseX.Checked ? "使能" : "禁用") + "X镜像失败，错误信息：" + _cmr.GetErrorInfo(err));
            //    return;
            //}

        }

        private void chkReverseY_CheckedChanged(object sender, EventArgs e)
        {
            if (_isReverseYUpdating)
                return;
            if (null == _cmr)
            {
                ShowTips("操作失败，相机未设置");
                return;
            }
            //int err = _cmr.SetReverseY(chkReverseY.Checked);
            //if (0 == err)
            //{
            //    ShowTips((chkReverseY.Checked ? "使能" : "禁用") + "Y镜像成功");
            //    return;
            //}
            //else
            //{
            //    MessageBox.Show((chkReverseY.Checked ? "使能" : "禁用") + "Y镜像失败，错误信息：" + _cmr.GetErrorInfo(err));
            //    return;
            //}
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
                if (_isAutoGrabRunning)
                    _StopAutoGrab();
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
                cCmrTrigMode tm = cCmrTrigMode.disable;
                _cmr.GetTrigMode(out tm);

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



                if (chkContinueGrab.Checked)
                {
                    _StartAutoGrb();
                    btGrabOne.Enabled = false;
                    btSoftwareTrig.Enabled = false;
                }
                else //非连续采图模式
                {
                    if (tm == cCmrTrigMode.disable)
                    {
                        btGrabOne.Enabled = true;
                        btSoftwareTrig.Enabled = false;
                    }
                    else
                    {
                        btGrabOne.Enabled = false;
                        if (tm == cCmrTrigMode.software)
                            btSoftwareTrig.Enabled = true;
                        else
                            btSoftwareTrig.Enabled = false;
                    }

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
            int err = _cmr.GrabOne(out img);
            if (err != 0)
            {
                if (!chkContinueGrab.Checked)
                    MessageBox.Show("抓图失败，错误信息:" + _cmr.GetErrorInfo(err) + " " + DateTime.Now.ToString("HH:mm:ss:ms"));
                else
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
                    if (!chkContinueGrab.Checked)
                        MessageBox.Show("SDK显示图像失败，错误信息:" + img.GetErrorInfo(err) + " " + DateTime.Now.ToString("HH:mm:ss:ms"));
                    else
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
                    if (!chkContinueGrab.Checked)
                        MessageBox.Show("显示图片失败，未能将图片转化为Halcon对象,错误信息：" + img.GetErrorInfo(err));
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
                    if (!chkContinueGrab.Checked)
                        MessageBox.Show("显示图片失败，未能将图片转化为Bitmap对象,错误信息：" + img.GetErrorInfo(err));
                    //return;
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


        bool _isAutoGrabRunning = false;
        Thread _threadGrab = null;
        void _FuncAutoGrab()
        {
            while (_isAutoGrabRunning)
            {
                if (!chkCallBack.Checked)
                {
                    IPlat_Image img = null;
                    int err = _cmr.GrabOne(out img);

                    if (err != 0)
                    {
                        if (!chkContinueGrab.Checked)
                            MessageBox.Show("抓图失败，错误信息:" + _cmr.GetErrorInfo(err) + " " + DateTime.Now.ToString("HH:mm:ss:ms"));
                        else
                            ShowTips("抓图失败，错误信息:" + _cmr.GetErrorInfo(err) + " " + DateTime.Now.ToString("HH:mm:ss:ms"));

                        return;
                    }

                    ShowTips("抓图操作成功，开始显示图像...");
                    ShowImg(img);
                    _currImage = img;
                    //Thread.Sleep(40);
                }
                else //使用回调 + 连续
                {

                }

            }
        }

        void _StartAutoGrb()
        {
            if (_isAutoGrabRunning)
                return;
            _isAutoGrabRunning = true;
            _threadGrab = new Thread(_FuncAutoGrab);
            _threadGrab.Start();
        }

        void _StopAutoGrab()
        {
            if (!_isAutoGrabRunning)
                return;
            _isAutoGrabRunning = false;
            if (!_threadGrab.Join(1000))
                _threadGrab.Abort();
        }

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSoftwareTrig_Click(object sender, EventArgs e)
        {
            if (null == _cmr)
            {
                ShowTips("操作失败，相机未设置");
                return;
            }

            int err = _cmr.SoftwareTrig();
            if (0 != err)
            {
                ShowTips("软触发拍照失败，错误信息：" + _cmr.GetErrorInfo(err));
                MessageBox.Show("软触发拍照失败，错误信息：" + _cmr.GetErrorInfo(err));
                return;
            }

            ShowTips("软触发拍照成功！");
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
                //if(null != _currHo)
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
            _StopAutoGrab();
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

        private void numBuffSize_ValueChanged(object sender, EventArgs e)
        {
            if (_isBuffCountUpdating)
                return;
            if (_cmr == null)
                return;
            int nBuff = Convert.ToInt32(numBuffSize.Value);
            //int err = _cmr.SetBuffSize(nBuff);
            //if(0 != err)
            //{
            //    MessageBox.Show("设置缓存帧数失败，ErrorInfo:" + _cmr.GetErrorInfo(err));
            //    return;
            //}
            ShowTips("设置缓存帧数：" + nBuff + " 完成");
        }

        private void numExposure_ValueChanged(object sender, EventArgs e)
        {
            if (_isExposureUpdating)
                return;
            if (_cmr == null)
                return;
            double dVal = Convert.ToDouble(numExposure.Value);
            //int err = _cmr.SetExposure(dVal);
            //if (0 != err)
            //{
            //    MessageBox.Show("设置曝光时间参数失败，ErrorInfo:" + _cmr.GetErrorInfo(err));
            //    return;
            //}
            ShowTips("设置曝光时间：" + dVal + " 完成");
        }

        private void numGain_ValueChanged(object sender, EventArgs e)
        {
            if (_isGainUpdating)
                return;
            if (_cmr == null)
                return;
            double dVal = Convert.ToDouble(numGain.Value);
            //int err = _cmr.SetGain(dVal);
            //if (0 != err)
            //{
            //    MessageBox.Show("设置增益参数失败，ErrorInfo:" + _cmr.GetErrorInfo(err));
            //    return;
            //}
            ShowTips("设置增益参数：" + dVal + " 完成");
        }

        //画图区域大小改变
        private void picBox_SizeChanged(object sender, EventArgs e)
        {

        }


    }
}
