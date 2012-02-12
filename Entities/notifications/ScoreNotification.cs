
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities.Notifications
{
    public class ScoreNotification
    {
        internal string Json;

        public List<string> Points { get; set; }
        public List<string> Message { get; set; }
        public List<string> Icon { get; set; }
        public string Total { get; set; }

        public ScoreNotification(Dictionary<string,object> jsonDicitonary)
        {
            Json = Helpers.JsonSerializer(jsonDicitonary);
            Points = new List<string>();
            Message = new List<string>();
            Icon = new List<string>();
            Total = jsonDicitonary["total"].ToString();
            foreach(object obj in (object[])jsonDicitonary["scores"])
            {
                var v = (Dictionary<string, object>) obj;
                Points.Add(Helpers.GetDictionaryValue(v, "points"));
                Message.Add(Helpers.GetDictionaryValue(v, "message"));
                Icon.Add(Helpers.GetDictionaryValue(v, "icon"));
            }
            
        }
    }
}
