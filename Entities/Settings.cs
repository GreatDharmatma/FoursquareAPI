using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Settings : Response
    {
        public List<Setting> Setting { get; private set; }

        public Settings(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Setting = new List<Setting>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:settings");
            foreach (var obj in jsonDictionary)
            {
                Setting.Add(new Setting(obj.Key, obj.Value.ToString()));
            }
        }
    }
}
