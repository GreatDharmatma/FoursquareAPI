
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities.Notifications
{
    public class ScoreNotification
    {
        internal string Json;

        public string Points { get; private set; }
        public string Message { get; private set; }
        public string Icon { get; private set; }

        public ScoreNotification(Dictionary<string,object> jsonDicitonary)
        {
            Json = Helpers.JsonSerializer(jsonDicitonary);

            Points = Helpers.GetDictionaryValue(jsonDicitonary, "points");
            Message = Helpers.GetDictionaryValue(jsonDicitonary, "message");
            Icon = Helpers.GetDictionaryValue(jsonDicitonary, "icon");
        }
    }
}
