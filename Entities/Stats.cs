using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Stats
    {
        public string checkinsCount = "";
        public string usersCount = "";
        public string tipCount = "";
        private string JSON = "";

        public Stats(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            checkinsCount = Helpers.getDictionaryValue(JSONDictionary, "checkinsCount");
            usersCount = Helpers.getDictionaryValue(JSONDictionary, "usersCount");
            tipCount = Helpers.getDictionaryValue(JSONDictionary, "tipCount");

        }
    }
}
