using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Brahmastra.FoursquareApi.IO
{
    class HttpMultiPartPost
    {
        public string ResponseBody { get; private set; }

        public HttpMultiPartPost(string url, Dictionary<string, string> parameters, string filePath)
        {
            ResponseBody = "";
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            DoPost(url, parameters, filePath, fileStream);
        }

        public HttpMultiPartPost(string url, Dictionary<string, string> parameters, string fileName, FileStream fileStream)
        {
            ResponseBody = "";
            DoPost(url, parameters, fileName, fileStream);
        }

        private void DoPost(string url, Dictionary<string, string> parameters, string fileName, FileStream fileStream)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            var wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = CredentialCache.DefaultCredentials;
            Stream rs = wr.GetRequestStream();
            const string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in parameters.Keys)
            {
                rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                string formitem = string.Format(formdataTemplate, key, parameters[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundaryBytes, 0, boundaryBytes.Length);
            const string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "jpeg", fileName, "image/jpeg");
            byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerBytes, 0, headerBytes.Length);
            var buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();
            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();
            WebResponse wResponse = null;
            try
            {
                wResponse = wr.GetResponse();
                var stream2 = wResponse.GetResponseStream();
                if (stream2 != null)
                {
                    var reader2 = new StreamReader(stream2);
                    ResponseBody = reader2.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                if (wResponse != null)
                {
                    wResponse.Close();
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
