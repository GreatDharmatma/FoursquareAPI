using System.Collections.Generic;

namespace Brahmastra.FoursquareAPI.Entities.Notifications
{
    public class TipNotification
    {
        public string Name { get; private set; }
        public Tip Tip { get; private set; }

        public TipNotification(Dictionary<string, object> jsonDictionary)
        {
            //
        }
    }
}

