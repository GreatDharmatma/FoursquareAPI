using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities.Notifications
{
    public class MessageNotification
    {
        public string Message { get; private set; }

        public MessageNotification(Dictionary<string,object> jsonDictionary)
        {
            Message = Helpers.GetDictionaryValue(jsonDictionary, "message");
        }
    }
}
