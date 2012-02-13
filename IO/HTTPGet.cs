using System.Net;
using System.Text;

namespace Brahmastra.FoursquareApi.IO
{
    class HttpGet
    {
        private HttpWebRequest _request;
        private HttpWebResponse _response;

        private string _escapedBody;

        public string ResponseBody { get; private set; }
        public string EscapedBody { get { return GetEscapedBody(); } }
        public int StatusCode { get; private set; }
        public string Headers { get { return GetHeaders(); } }
        public string StatusLine { get { return GetStatusLine(); } }

        public void Request(string url)
            {
                var respBody = new StringBuilder();

                _request = (HttpWebRequest)WebRequest.Create(url);

                try
                {
                    _response = (HttpWebResponse)_request.GetResponse();
                    var buf = new byte[8192];
                    var respStream = _response.GetResponseStream();
                    var count = 0;
                    do
                    {
                        if (respStream != null) count = respStream.Read(buf, 0, buf.Length);
                        if (count != 0)
                            respBody.Append(Encoding.ASCII.GetString(buf, 0, count));
                    } while (count > 0);

                    ResponseBody = respBody.ToString();
                    StatusCode = (int)_response.StatusCode;
                }
                catch (WebException ex)
                {
                    _response = (HttpWebResponse)ex.Response;
                    ResponseBody = "No Server Response";
                    _escapedBody = "No Server Response";
                }
            }
        
        public string GetEscapedBody()
            {  
                // HTML escaped chars
                _escapedBody = ResponseBody;
                _escapedBody = _escapedBody.Replace("&", "&amp;");
                _escapedBody = _escapedBody.Replace("<", "&lt;");
                _escapedBody = _escapedBody.Replace(">", "&gt;");
                _escapedBody = _escapedBody.Replace("'", "&apos;");
                _escapedBody = _escapedBody.Replace("\"", "&quot;");

                return _escapedBody;
            }

        public string GetHeaders()
            {
            if (_response == null)
                return "No Server Response";
            var headers = new StringBuilder();
            for (var i = 0; i < _response.Headers.Count; ++i)
                headers.Append(string.Format("{0}: {1}\n",
                                             _response.Headers.Keys[i], _response.Headers[i]));

            return headers.ToString();
            }

        public string GetStatusLine()
        {
            return _response == null
                       ? "No Server Response"
                       : string.Format("HTTP/{0} {1} {2}", _response.ProtocolVersion,
                                       (int) _response.StatusCode, _response.StatusDescription);
        }
    }
}
