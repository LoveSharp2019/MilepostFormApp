using Cell.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{
    /// <summary>
    /// 被动模式下的扫码回调函数
    /// </summary>
    /// <param name="scanner">扫码设备</param>
    /// <param name="resultCode">错误码，0 == Success ， 可以通过GetErrorInfo（）转化为文本信息</param>
    /// <param name="barcode">扫到的条码（或其他返回字串）</param>
    public delegate void ucBarcodeDelegate(IPlatDevice_Barcode scanner, int resultCode, string barcode);


    public interface IPlatDevice_Barcode : IPlatDevice
    {
        /// <summary>
        /// 获取扫码枪当前工作模式
        /// </summary>
        /// <returns></returns>
        cBarcodeSanMode GetWorkMode();

        /// <summary>
        /// 设置工作模式
        /// 当设置为主动模式时，应屏蔽Callback
        /// 当设置为被动模式时，主动Scan函数无效
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        int SetWorkMode(cBarcodeSanMode mode);

        /// <summary>
        /// 被动工作模式下，扫码枪扫描完成后，会通过此回调函数返回条码字串
        /// </summary>
        event ucBarcodeDelegate ScanCallBack;


        /// <summary>
        /// 主动工作模式下，向扫码枪发送扫码指令，并返回扫描结果
        /// </summary>
        /// <returns></returns>
        int Scan(out string barcode);

        /// <summary>
        /// 清空数据缓冲区
        /// </summary>
        /// <returns></returns>
        int ClearBuff();

    }
}
