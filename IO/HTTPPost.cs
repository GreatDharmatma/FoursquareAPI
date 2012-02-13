using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Brahmastra.FoursquareApi.IO
{
    class HttpPost
    {
        private readonly HttpWebRequest _request;
        private readonly HttpWebResponse _response;
        private string _escapedBody;

        public string Headers { get; private set; }
        public string StatusCode { get; private set; }
        public string ResponseBody { get; private set; }
        public string EscapedBody { get { return GetEscapedBody(); } }
        
        public HttpPost(Uri url, Dictionary<string, string> parameters)
        {
            var respBody = new StringBuilder();

            _request = (HttpWebRequest)WebRequest.Create(url);
            _request.UserAgent = "Mozilla/5.0 (iPhone; U; CPU like Mac OS X; en) AppleWebKit/420+ (KHTML, like Gecko) Version/3.0 Mobile/1C10 Safari/419.3";
            _request.Method = "POST";
            _request.ContentType = "application/x-www-form-urlencoded";
            var content = parameters.Keys.Aggregate("?", (current, k) => current + (k + "=" + parameters[k] + "&"));
            content = content.TrimEnd(new[] { '&' });
            var encoding = new UTF8Encoding();
            var byteArr = encoding.GetBytes(content);
            _request.ContentLength = byteArr.Length;
            var buf = new byte[8192];
            using (Stream rs = _request.GetRequestStream())
            {
                rs.Write(byteArr, 0, byteArr.Length);
                rs.Close();

                _response = (HttpWebResponse)_request.GetResponse();
                Stream respStream = _response.GetResponseStream();

                var count = 0;
                do
                {
                    if (respStream != null) count = respStream.Read(buf, 0, buf.Length);
                    if (count != 0)
                        respBody.Append(Encoding.ASCII.GetString(buf, 0, count));
                } while (count > 0);

                if (respStream != null) respStream.Close();
                ResponseBody = respBody.ToString();
                _escapedBody = GetEscapedBody();
                StatusCode = GetStatusLine();
                Headers = GetHeaders();

                _response.Close();
            }
        }

        private string GetEscapedBody()
        {  // HTML escaped chars
            _escapedBody = ResponseBody;
            _escapedBody = _escapedBody.Replace("&", "&amp;");
            _escapedBody = _escapedBody.Replace("<", "&lt;");
            _escapedBody = _escapedBody.Replace(">", "&gt;");
            _escapedBody = _escapedBody.Replace("'", "&apos;");
            _escapedBody = _escapedBody.Replace("\"", "&quot;");
            return _escapedBody;
        }

        private string GetHeaders()
        {
            if (_response == null)
                return "No Server Response";
            var headers = new StringBuilder();
            for (var i = 0; i < _response.Headers.Count; ++i)
                headers.Append(string.Format("{0}: {1}\n",
                                             _response.Headers.Keys[i], _response.Headers[i]));

            return headers.ToString();
        }

        private string GetStatusLine()
        {
            if (_response == null)
                return "No Server Response";
            return string.Format("HTTP/{0} {1} {2}", _response.ProtocolVersion,
                                 (int)_response.StatusCode, _response.StatusDescription);
        }
    }
}
