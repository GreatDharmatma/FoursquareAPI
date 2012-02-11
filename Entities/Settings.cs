using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Settings : Response
    {
        List<Setting> settings = new List<Setting>();

        public Settings(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:settings");
            foreach (object Obj in JSONDictionary)
            {
                settings.Add(new Setting(((System.Collections.Generic.KeyValuePair<string, object>)Obj).Key, ((System.Collections.Generic.KeyValuePair<string, object>)Obj).Value.ToString()));
            }
        }
    }
}
