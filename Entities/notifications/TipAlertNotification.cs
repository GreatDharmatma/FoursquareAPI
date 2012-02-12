
using System.Collections.Generic;

namespace Brahmastra.FoursquareAPI.Entities.Notifications
{
    public class TipAlertNotification
    {
        public Tip Tip { get; private set; }

        public TipAlertNotification(Dictionary<string,object> jsonDictionary)
        {
            //
        }
    }
}
