using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;
using System;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Special : Response
    {
        public string id = "";
        public string type = "";
        public string message = "";
        public string description = "";
        public string finePrint = "";
        public bool unlocked = false;
        public string icon = "";
        public string title = "";
        public string state = "";
        public string progress = "";
        public string progressDescription = "";
        public string detail = "";
        public string target = "";
        public List<User> friendsHere = new List<User>();
        public Venue venue;

        public Special(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {

            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:special");
            id = Helpers.getDictionaryValue(JSONDictionary, "id");
            type = Helpers.getDictionaryValue(JSONDictionary, "type");
            message = Helpers.getDictionaryValue(JSONDictionary, "message");
            description = Helpers.getDictionaryValue(JSONDictionary, "description");
            finePrint = Helpers.getDictionaryValue(JSONDictionary, "finePrint");
            if (Helpers.getDictionaryValue(JSONDictionary, "unlocked").ToLower().Equals("true"))
            {
                unlocked = true;
            }
            icon = Helpers.getDictionaryValue(JSONDictionary, "icon");
            title = Helpers.getDictionaryValue(JSONDictionary, "title");
            state = Helpers.getDictionaryValue(JSONDictionary, "state");
            progress = Helpers.getDictionaryValue(JSONDictionary, "progress");
            progressDescription = Helpers.getDictionaryValue(JSONDictionary, "progressDescription");
            detail = Helpers.getDictionaryValue(JSONDictionary, "detail");
            target = Helpers.getDictionaryValue(JSONDictionary, "target");
            if (JSONDictionary.ContainsKey("friendsHere"))
            {
                throw new Exception("Todo");
            }
            if (JSONDictionary.ContainsKey("venue"))
            {
                venue = new Venue((Dictionary<string, object>)JSONDictionary["venue"]);
            }
        }
    }
}
