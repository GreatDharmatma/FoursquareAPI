using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities.Notifications
{
    public class MayorshipNotification
    {
        internal string Json = "";

        public string Type { get; private set; }
        public string Checkins { get; private set; }
        public string DaysBehind { get; private set; }
        public string Message { get; private set; }
        public string ImageUrl { get; private set; }

        public MayorshipNotification(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);

            Type = Helpers.GetDictionaryValue(jsonDictionary, "type");
            Checkins = Helpers.GetDictionaryValue(jsonDictionary, "checkins");
            DaysBehind = Helpers.GetDictionaryValue(jsonDictionary, "daysBehind");
            Message = Helpers.GetDictionaryValue(jsonDictionary, "message");
            ImageUrl = Helpers.GetDictionaryValue(jsonDictionary, "image");
        }
    }
}
