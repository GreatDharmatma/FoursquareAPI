using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class BadgeSets : Response
    {
        public string defaultSetType = "";
        public List<BadgeSet> BadgeSets = new List<BadgeSet>();
        public List<Badge> Badges = new List<Badge>();

        public BadgeSets(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response");

            if (JSONDictionary.ContainsKey("defaultSetType"))
            {
                defaultSetType = JSONDictionary["defaultSetType"].ToString();
            }

            if (JSONDictionary.ContainsKey("badges"))
            {
                foreach (KeyValuePair<string, object> Obj in (Dictionary<string, object>)JSONDictionary["badges"])
                {
                    Badges.Add(new Badge((Dictionary<string, object>)Obj.Value));
                }
            }

            if (JSONDictionary.ContainsKey("sets"))
            {
                foreach (object Obj in (object[])((Dictionary<string, object>)JSONDictionary["sets"])["groups"])
                {
                    BadgeSets.Add(new BadgeSet((Dictionary<string, object>)Obj));
                }
            }
        }
    }
}
