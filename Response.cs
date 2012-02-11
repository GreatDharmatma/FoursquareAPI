using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brahmastra.FoursquareAPI.IO;
using Brahmastra.FoursquareAPI.Entities;

namespace Brahmastra.FoursquareAPI
{
    public class Response
    {
        public string metaCode = "";
        public string metaErrorType = "";
        public string metaErrorDetail = "";
        public List<Notification> notifications = new List<Notification>();
        private string JSON = "";

        public Response(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            Dictionary<string, object> meta = Helpers.extractDictionary(JSONDictionary, "meta");
            metaCode = Helpers.getDictionaryValue(meta, "code");
            metaErrorType = Helpers.getDictionaryValue(meta, "errorType");
            if(metaErrorType.Contains("deprecated"))
            {
                throw new Exception("deprecated Call");
                //todo - handle this somehow.
            }
            metaErrorDetail = Helpers.getDictionaryValue(meta, "errorDetail");
            if (JSONDictionary.ContainsKey("notifications"))
            {
                foreach (object Obj in (object[])JSONDictionary["notifications"])
                {
                    Notification notification = new Notification((Dictionary<string, object>)Obj);
                    notifications.Add(notification);
                }
            }
        }
    }
}
