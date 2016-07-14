using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BSApi
{
    class HTTPRequester
    {
        #region Singelton
        private HTTPRequester _instance;

        public HTTPRequester GetInstance()
        {
            if (_instance == null)
                _instance = new HTTPRequester();

            return _instance;
        }
    #endregion

        public static string LaunchApiRequest(string uri)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://bs.to/api/"+uri);
            request.Method = "GET";
            request.Headers.Add("BS-Token", ApiKey.Generate(uri));
            request.UserAgent = "bs.android";

            using (System.IO.Stream s = request.GetResponse().GetResponseStream())
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
