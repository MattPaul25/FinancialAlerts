using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Globalization;


namespace FinancialAlerts
{
    class LookUp
    {
        //new branch of fin alerts
        public LookUp (string myTicker, string startDate, string endDate)
        {
            string sDate = yahooDateParser(startDate);
            string eDate = yahooDateParser(startDate);
            GrabFinanceAPI(myTicker, sDate, eDate);
            GetCurrentPrice(myTicker);
        }

        private void GetCurrentPrice(string myTicker)
        {
            string myURL = @"http://download.finance.yahoo.com/d/quotes.csv?s=%40%5EDJI,GOOG&f=nsl1op&e=.csv";
            string test = GetCSV(myURL);
            StaticStore.currentPrice = test;
        }

        private string yahooDateParser(string aDate)
        {
            //creates the date in a yahoo finance format (subtracts 1 from the month -- month index starts at 0)
            int slash1 = Search(aDate, "/");
            int slash2 = Search(aDate, "/", 2);
            string month = aDate.Substring(0, slash1 - 1);
            int x = Convert.ToInt32(month) - 1;
            month = x.ToString();
            string year = aDate.Substring(slash2, 4);
            string day = aDate.Substring(slash1, (slash2 - slash1)-1);
            return "&a=" + month + "&b=" + day + "&c=" + year;
        }

        private static void GrabFinanceAPI(string ticker, string sDate, string tDate)
        {
            string bseURL = @"http://ichart.yahoo.com/table.csv?s";
            string stckID = "=" + ticker;
            string interval = "&g=" + StaticStore.interval;
            string staticPort = "&ignore=.csv";

            string fullURL = bseURL + stckID + sDate + tDate + interval + staticPort;
            StaticStore.lookUpResults = GetCSV(fullURL);
        }
        static string GetCSV(string url)
        {
            string results = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                results = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception x)
            {
                results = x.Message;
            }
            return results;
            
        }
        static int Search(string yourString, string yourMarker, int yourInst = 1, bool caseSensitive = true)
        {
            //returns the placement of a string in another string
            int num = 0;
            int ginst = 1;
            int mlen = yourMarker.Length;
            int slen = yourString.Length;
            string tString = "";
            try
            {
                if (caseSensitive == false)
                {
                    yourString = yourString.ToLower();
                    yourMarker = yourMarker.ToLower();
                }
                while (num < slen)
                {
                    tString = yourString.Substring(num, mlen);

                    if (tString == yourMarker && ginst == yourInst)
                    {
                        return num + 1;
                    }
                    else if (tString == yourMarker && yourInst != ginst)
                    {
                        ginst += 1;
                        num += 1;
                    }
                    else
                    {
                        num += 1;
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
