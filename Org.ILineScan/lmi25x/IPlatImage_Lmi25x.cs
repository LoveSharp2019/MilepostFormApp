using Cell.DataModel;
using Cell.Interface;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Org.ILineScan
{
    public class IPlatImage_Lmi25x : IPlat_Image
    {
        double[] _dataDouble = null;

        HObject hoImage = null;
        /// <summary>图像帧序号</summary>
        public int SequenceIndex { get; private set; }
        /// <summary>图像宽度（X方向）</summary>
        public int PicWidth { get; private set; }
        /// <summary>图像高度（Y方向）</summary>
        public int PicHeight { get; private set; }
        /// <summary>图像数据行的宽度</summary>
        public int StrideWidth { get; private set; }
        public IPlatImgPixFormat PixerFormat { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPtr">指向图像数据</param>
        /// <param name="frameInfo"></param>
        public IPlatImage_Lmi25x(double[] dataDouble, int imgHeight, int imgWidth, IPlatImgPixFormat pixerFormat)
        {
            _dataDouble = dataDouble;
            PicWidth = imgWidth;
            PicHeight = imgHeight;
            PixerFormat = pixerFormat;
        }
        ~IPlatImage_Lmi25x()
        {
            Dispose(false);
        }

        public int DisplayTo(IntPtr pWndHandle)
        {
            if (null == pWndHandle)
                return (int)ErrorCode.ParamError;
            HOperatorSet.DispImage(hoImage, pWndHandle);
            return (int)ErrorCode.Success;
        }

        /// <summary>
        /// HObject -> Bitmap
        /// </summary>
        /// <param name="bmp_width"></param>
        /// <param name="bmp_height"></param>
        /// <param name="pointer">指针</param>
        /// <returns></returns>
        public static Bitmap PointerToBitmap(int bmp_width, int bmp_height, IntPtr pointer)
        {
            Bitmap bmp = new Bitmap(bmp_width, bmp_height, PixelFormat.Format8bppIndexed);
            ColorPalette tempPalette = bmp.Palette;
            const int Alpha = 255;
            for (int i = 0; i <= 255; i++)
            { tempPalette.Entries[i] = Color.FromArgb(Alpha, i, i, i); }

            bmp.Palette = tempPalette; Rectangle rect = new Rectangle(0, 0, bmp_width, bmp_height);
            BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
            int stride = bitmapData.Stride;
            IntPtr ptr = bitmapData.Scan0;
            if (bmp_width % 4 == 0)
                CopyMemory(ptr, pointer, bmp_width * bmp_height * PixelSize);
            else
            {
                for (int i = 0; i < bmp_height; i++)
                {
                    CopyMemory(ptr, pointer, bmp_width * PixelSize);
                    pointer += bmp_width;
                    ptr += bitmapData.Stride;
                }
            }
            bmp.UnlockBits(bitmapData);
            return bmp;

        }

        public Bitmap DoubleArrayToRGBBitmap(double[] pixelData, int width, int height)
        {
            // 假设double数组中的值是0.0到1.0的范围内的浮点数
            // 创建一个内存区块来存储像素数据
            int size = width * height * 3; // 每个像素3个字节：B, G, R
            byte[] pixelBytes = new byte[size];

            // 将double值转换为字节
            for (int i = 0, k = 0; i < pixelData.Length; i += 3, k += 3)
            {
                // 假设RGB分量都在double数组中按顺序存储
                pixelBytes[k] = (byte)(pixelData[i] * 255);     // B
                pixelBytes[k + 1] = (byte)(pixelData[i + 1] * 255); // G
                pixelBytes[k + 2] = (byte)(pixelData[i + 2] * 255); // R
            }

            // 使用Bitmap构造函数创建Bitmap图像
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            try
            {
                // 拷贝像素数据到Bitmap
                Marshal.Copy(pixelBytes, 0, bmpData.Scan0, size);
            }
            finally
            {
                // 解锁位图并释放资源
                bmp.UnlockBits(bmpData);
            }

            return bmp;
        }

        public static Bitmap DoubleArrayToGrayscaleBitmap(double[] doubleArray, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            // 创建灰度色表
            ColorPalette customPalette = bmp.Palette;
            for (int i = 0; i < 256; i++)
            {
                customPalette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bmp.Palette = customPalette;

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            try
            {
                int length = width * height;
                if (doubleArray.Length >= length)
                {
                    unsafe
                    {
                        byte* ptr = (byte*)bmpData.Scan0;
                        for (int i = 0; i < length; i++)
                        {
                            // 确保灰度值在0到255之间
                            byte value = (byte)Math.Max(0, Math.Min(255, doubleArray[i]));
                            *ptr++ = value;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }

            return bmp;
        }

        public int GenBmp(out Bitmap bmp)
        {
            // double[] 转灰度图 待测试
            bmp = DoubleArrayToGrayscaleBitmap(_dataDouble, PicWidth, PicHeight);
            return (int)ErrorCode.PixelFormatError;

        }

        public int GenHalcon(out object image)
        {
            HObject hImg = new HObject();
            HObject domain;
            HTuple RowS, cols;

            if (PixerFormat == IPlatImgPixFormat.Mono8)
            {
                HOperatorSet.GenImageConst(out hImg, "real", PicHeight, PicWidth);
                HOperatorSet.GetDomain(hImg, out domain);
                HOperatorSet.GetRegionPoints(domain, out RowS, out cols);
                HOperatorSet.SetGrayval(hImg, RowS, cols, _dataDouble);
            }
            else
            {
                image = null;
                return (int)ErrorCode.Unsupported;
            }
            image = hImg;
            return (int)ErrorCode.Success;
        }

        /// <summary>
        /// 生成指定类型的的图像对象
        /// </summary>
        /// <param name="imgObj"></param>
        /// <param name="imgType">"Halcon","Bitmap"或其他可能的类型</param>
        /// <returns></returns>
        public int GenImgObject(out object imgObj, string imgType)
        {
            imgObj = null;
            if ("Halcon" == imgType)
                return GenHalcon(out imgObj);
            else if ("Bitmap" == imgType)
            {
                Bitmap bmp;
                int ret = GenBmp(out bmp);
                imgObj = bmp;
                return ret;
            }
            return (int)ErrorCode.ParamError;
        }
        [DllImport("Kernel32.dll")]
        internal static extern void CopyMemory(int dest, int source, int size);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi)]
        public extern static long CopyMemory(IntPtr dest, IntPtr source, int size);

        [DllImport("Kernel32.dll")]
        internal static extern void CopyMemory(IntPtr dest, IntPtr source, IntPtr size);

        enum ErrorCode
        {
            Success = 0,
            InvokeFailed = -1,
            MemoryExcp = -2,//内存操作异常
            FileExc = -3, //文件操作异常
            Unsupported = -4,//不支持的功能
            PixelFormatError = -5, //暂不支持的像素格式
            ParamError = -6,
        }

        public string GetErrorInfo(int errorCode)
        {
            string ret = "Undefined ErrorCode = " + errorCode;
            switch (errorCode)
            {
                case (int)ErrorCode.Success:
                    ret = "Success";
                    break;
                case (int)ErrorCode.MemoryExcp:
                    ret = "Memory Exception";
                    break;
                case (int)ErrorCode.InvokeFailed:
                    ret = "Invoke failed";
                    break;
                case (int)ErrorCode.FileExc:
                    ret = "File Exception";
                    break;
                case (int)ErrorCode.Unsupported://= -4,//不支持的功能
                    ret = "Unsupported";
                    break;
                case (int)ErrorCode.PixelFormatError: //= -5, //暂不支持的像素格式
                    ret = "Pixel's format unspupported";
                    break;
                case (int)ErrorCode.ParamError:
                    ret = "Param Error";
                    break;
                default:
                    break;

            }
            return ret;
        }

        public int GetRowData(out byte[] rowData)
        {
            rowData = null;
            return (int)ErrorCode.Unsupported;
        }

        public int Save(string filePath, IPlatImgSuffixType fileType = IPlatImgSuffixType.Bmp)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return (int)ErrorCode.ParamError;
            string extension = "bmp";
            switch (fileType)
            {
                case IPlatImgSuffixType.Jpg:
                    extension = "jpeg";
                    break;
                case IPlatImgSuffixType.Png:
                    extension = "png";
                    break;
                case IPlatImgSuffixType.Tif:
                    extension = "tiff";
                    break;
                default:
                    break;
            }
            try
            {
                HOperatorSet.WriteImage(hoImage, extension, 0, filePath);
            }
            catch
            {
                return (int)ErrorCode.FileExc;
            }
            return (int)ErrorCode.Success;
        }


        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。

                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                if (null != hoImage)
                {
                    hoImage.Dispose();
                    hoImage = null;
                }
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }


        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
