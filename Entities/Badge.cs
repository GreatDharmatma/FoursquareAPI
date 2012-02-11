using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Badge
    {
        public string id = "";
        public string badgeID = "";
        public string name = "";
        public string description = "";
        public string hint = "";
        public Image image;
        public List<Checkin> unlocks = new List<Checkin>();
        private string JSON = "";

        public Badge(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            id = JSONDictionary["id"].ToString();
            if (JSONDictionary.ContainsKey("badgeID"))
            {
                badgeID = JSONDictionary["badgeID"].ToString();
            }
            else
            {
                badgeID = id;
            }
            name = JSONDictionary["name"].ToString();
            if (JSONDictionary.ContainsKey("description"))
            {
                description = JSONDictionary["description"].ToString();
            }
            if (JSONDictionary.ContainsKey("hint"))
            {
                hint = JSONDictionary["hint"].ToString();
            }
            image = new Image(((Dictionary<string, object>)JSONDictionary["image"]));
            foreach (object Obj in (object[])Helpers.extractDictionary(JSONDictionary, "response")["unlocks"])
            {
                Dictionary<string, object> unlockCheckin = (Dictionary<string, object>)((object[])((Dictionary<string, object>)Obj)["checkins"])[0];
                unlocks.Add(new Checkin(unlockCheckin));
            }
        }
    }
}
