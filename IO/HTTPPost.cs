using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Brahmastra.FoursquareAPI.IO
{
    class HTTPPost
    {
        private HttpWebRequest request;
        private HttpWebResponse response;

        public string responseBody;
        public string escapedBody;
        public string statusCode;
        public string headers;


        public HTTPPost(Uri url, Dictionary<string, string> Parameters)
        {
            StringBuilder respBody = new StringBuilder();

            request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (iPhone; U; CPU like Mac OS X; en) AppleWebKit/420+ (KHTML, like Gecko) Version/3.0 Mobile/1C10 Safari/419.3";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string content = "?";
            foreach (string k in Parameters.Keys)
            {
                content += k + "=" + Parameters[k] + "&";
            }
            content = content.TrimEnd(new char[] { '&' });
            UTF8Encoding encoding = new UTF8Encoding();
            //ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byteArr = encoding.GetBytes(content);
            request.ContentLength = byteArr.Length;
            byte[] buf = new byte[8192];
            using (Stream rs = request.GetRequestStream())
            {
                rs.Write(byteArr, 0, byteArr.Length);
                rs.Close();

                response = (HttpWebResponse)request.GetResponse();
                Stream respStream = response.GetResponseStream();

                int count = 0;
                do
                {
                    count = respStream.Read(buf, 0, buf.Length);
                    if (count != 0)
                        respBody.Append(Encoding.ASCII.GetString(buf, 0, count));
                }
                while (count > 0);

                respStream.Close();
                responseBody = respBody.ToString();
                escapedBody = getEscapedBody();
                statusCode = getStatusLine();
                headers = getHeaders();

                response.Close();
            }
        }

        private string getEscapedBody()
        {  // HTML escaped chars
            string escapedBody = responseBody;
            escapedBody = escapedBody.Replace("&", "&amp;");
            escapedBody = escapedBody.Replace("<", "&lt;");
            escapedBody = escapedBody.Replace(">", "&gt;");
            escapedBody = escapedBody.Replace("'", "&apos;");
            escapedBody = escapedBody.Replace("\"", "&quot;");
            return escapedBody;
        }

        private string getHeaders()
        {
            if (response == null)
                return "No Server Response";
            else
            {
                StringBuilder headers = new StringBuilder();
                for (int i = 0; i < this.response.Headers.Count; ++i)
                    headers.Append(string.Format("{0}: {1}\n",
                        response.Headers.Keys[i], response.Headers[i]));

                return headers.ToString();
            }
        }

        private string getStatusLine()
        {
            if (response == null)
                return "No Server Response";
            else
                return string.Format("HTTP/{0} {1} {2}", response.ProtocolVersion,
                    (int)response.StatusCode, response.StatusDescription);
        }
    }
}
