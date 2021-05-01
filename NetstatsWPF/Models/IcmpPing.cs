using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NetstatsWPF.Models
{
    public class IcmpPing
    {
        private Ping _client;
        public ManualResetEvent pingDone;
        private int _result;
        private int _storeNumber;
        private int _deviceNumber;


        public IcmpPing(ref int storeNumber, ref int deviceNumber)
        {
            client = new Ping();
            pingDone = new ManualResetEvent(false);
            _storeNumber = storeNumber;
            _deviceNumber = deviceNumber;
        }
        public double DoATest()
        {
            pingDone.Reset();
            DoPingClient(ref _storeNumber, ref _deviceNumber);
            pingDone.WaitOne();
            return (double)_result;
        }

        private void DoPingClient(ref int storeNumber, ref int deviceNumber)
        {
            string address;
            if (storeNumber.ToString()[2] != '0')
            {
                address = "10." + storeNumber.ToString().Substring(0, 2) + "." + storeNumber.ToString().Substring(2) + "." + deviceNumber.ToString();
            }
            else
            {
                address = "10." + storeNumber.ToString().Substring(0, 2) + "." + storeNumber.ToString()[3] + "." + deviceNumber.ToString();
            }
            try
            {
                _client.SendAsync(address, 1500, null);
            }
            catch (Exception e1)
            {

            }
        }

        public int Result
        {
            get
            {
                return _result;
            }
        }

        private void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("An error occured while pinging: " + e.Error.InnerException.Message);
            }
            else if (!e.Cancelled)
            {
                if (e.Reply.Status == IPStatus.Success)
                {
                    _result = Convert.ToInt32(e.Reply.RoundtripTime);
                }
                else
                {
                    _result = -1;
                }
            }
            pingDone.Set();
        }

        public bool Reset()
        {
            bool flag;
            try
            {
                pingDone.Reset();
                _result = 0;
                flag = true;
            }
            catch (Exception exception1)
            {
                return true;
            }
            return flag;
        }

        private Ping client
        {
            get
            {
                return _client;
            }
            set
            {
                PingCompletedEventHandler handler = new PingCompletedEventHandler(PingCompleted);
                if (_client != null)
                {
                    _client.PingCompleted -= handler;
                }
                _client = value;
                if (_client != null)
                {
                    _client.PingCompleted += handler;
                }
            }
        }
    }
}
