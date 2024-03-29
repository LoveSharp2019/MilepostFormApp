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

namespace Org.ICamera
{
    public class IPlatImage_Basler : IPlat_Image
    {
        byte[] _dataBytes = null;

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
        public IPlatImage_Basler(byte[] dataBytes, int imgHeight, int imgWidth, IPlatImgPixFormat pixerFormat)
        {
            _dataBytes = dataBytes;
            PicWidth = imgWidth;
            PicHeight = imgHeight;
            PixerFormat = pixerFormat;
        }
        ~IPlatImage_Basler()
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

        public int GenBmp(out Bitmap bmp)
        {
            IntPtr p = Marshal.UnsafeAddrOfPinnedArrayElement(_dataBytes, 0);
            bmp = PointerToBitmap(PicWidth, PicHeight, p);
            return (int)ErrorCode.PixelFormatError;

        }

        public int GenHalcon(out object image)
        {
            HImage hImg = new HImage();
            if (PixerFormat == IPlatImgPixFormat.Mono8)
            {
                IntPtr p = Marshal.UnsafeAddrOfPinnedArrayElement(_dataBytes, 0);
                hImg.GenImage1("byte", PicWidth, PicHeight, p);
            }
            else if (PixerFormat == IPlatImgPixFormat.RGB24)
            {
                IntPtr p = Marshal.UnsafeAddrOfPinnedArrayElement(_dataBytes, 0);
                hImg.GenImageInterleaved(p, "rgb", PicWidth, PicHeight, 0, "byte", PicWidth, PicHeight, 0, 0, -1, 0);
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

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~JFImage_Hlc()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

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
