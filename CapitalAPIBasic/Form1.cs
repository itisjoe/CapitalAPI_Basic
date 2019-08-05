using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKCOMLib;

namespace CapitalAPIBasic
{
    public partial class Form1 : Form
    {
        SKCenterLib skc;
        SKReplyLib skr;
        SKQuoteLib skq;

        private string acc = "";
        private string pw = "";
        private int connect_status = 999;
        private string stockNo = "TX00";

        public Form1()
        {
            InitializeComponent();

            skc = new SKCenterLib();
            skr = new SKReplyLib();
            skr.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(OnAnnouncement);
            skq = new SKQuoteLib();
            skq.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(OnConnection);
            skq.OnNotifyTicks += new _ISKQuoteLibEvents_OnNotifyTicksEventHandler(OnNotifyTicks);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            acc = textBox1.Text;
            pw = textBox2.Text;
            connect_status = skc.SKCenterLib_Login(acc, pw);
            if (connect_status == 0)
            {
                msg_update("登入成功");
                connect_status = skq.SKQuoteLib_EnterMonitor();
                if (connect_status == 0)
                {
                    msg_update("連線成功");
                }
                else
                {
                    msg_update("連線錯誤代碼： " + Convert.ToString(connect_status));
                }
            }
            else
            {
                msg_update("登入錯誤代碼： " + Convert.ToString(connect_status));
            }
        }

        void OnConnection(int nKind, int nCode)
        {
            if (nKind == 3001)
            {
                if (nCode == 0)
                {
                    msg_update("連線中");
                }
            }
            else if (nKind == 3002)
            {
                msg_update("斷線");
            }
            else if (nKind == 3003)
            {
                msg_update("報價商品載入完成");
                StartRequest();
            }
            else if (nKind == 3021)//網路斷線
            {
                msg_update("連線失敗");
            }
        }

        void StartRequest()
        {
            connect_status = skq.SKQuoteLib_RequestTicks(1, stockNo);
            if (connect_status == 0)
            {
                msg_update(stockNo + " 成交明細與五檔設置成功");
            }
            else
            {
                msg_update("成交明細與五檔錯誤代碼： " + Convert.ToString(connect_status));
            }
        }

        void OnNotifyTicks(short sMarketNo, short sStockIdx, int nPtr, int nDate, int lTimehms, int lTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
        {
            string strData = "";
            strData = sStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
            msg_update(strData);
        }

        void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            nConfirmCode = -1;
        }

        private void msg_update(string msg)
        {
            textBox3.Text += msg + "\r\n";
        }

    }
}
