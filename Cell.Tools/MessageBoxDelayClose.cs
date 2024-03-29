using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.Tools
{
    public class MessageBoxDelayClose
    {

        /// <summary>
        /// 方法有问题 不是延时的
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="SecondCount"></param>
        public static void Show(string msg, int SecondCount)
        {
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                MessageBox.Show(msg);
                timer.Dispose();
            }, null, SecondCount * 1000, System.Threading.Timeout.Infinite);

        }

        public static void Show(string msg, string caption, int SecondCount)
        {
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                MessageBox.Show(msg, caption);
                timer.Dispose();
            }, null, SecondCount * 1000, System.Threading.Timeout.Infinite);

        }



    }
}
