using System;
using System.IO;
using System.Net;
using System.Text;

namespace Brahmastra.FoursquareAPI.IO
{
    class HTTPGet
    {
        public class HTTPGet
        {
            private HttpWebRequest request;
            private HttpWebResponse response;

            private string responseBody;
            private string escapedBody;
            private int statusCode;

            public string ResponseBody { get { return responseBody; } }
            public string EscapedBody { get { return getEscapedBody(); } }
            public int StatusCode { get { return statusCode; } }
            public string Headers { get { return getHeaders(); } }
            public string StatusLine { get { return getStatusLine(); } }



            public void Request(string url)
            {
                StringBuilder respBody = new StringBuilder();

                this.request = (HttpWebRequest)WebRequest.Create(url);

                try
                {
                    this.response = (HttpWebResponse)this.request.GetResponse();
                    byte[] buf = new byte[8192];
                    Stream respStream = this.response.GetResponseStream();
                    int count = 0;
                    do
                    {
                        count = respStream.Read(buf, 0, buf.Length);
                        if (count != 0)
                            respBody.Append(Encoding.ASCII.GetString(buf, 0, count));
                    }
                    while (count > 0);

                    this.responseBody = respBody.ToString();
                    this.statusCode = (int)(HttpStatusCode)this.response.StatusCode;
                }
                catch (WebException ex)
                {
                    this.response = (HttpWebResponse)ex.Response;
                    this.responseBody = "No Server Response";
                    this.escapedBody = "No Server Response";
                }
            }

            private string getEscapedBody()
            {  
                // HTML escaped chars
                string escapedBody = responseBody;
                escapedBody = escapedBody.Replace("&", "&amp;");
                escapedBody = escapedBody.Replace("<", "&lt;");
                escapedBody = escapedBody.Replace(">", "&gt;");
                escapedBody = escapedBody.Replace("'", "&apos;");
                escapedBody = escapedBody.Replace("\"", "&quot;");
                this.escapedBody = escapedBody;

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
}
