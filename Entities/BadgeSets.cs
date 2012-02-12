using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class BadgeSets : Response
    {
        public List<BadgeSet> BadgeSet { get; private set; }
        public List<Badge> Badges { get; private set; }
        public string DefaultSetType { get; private set; }

        public BadgeSets(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            DefaultSetType = "";
            Badges = new List<Badge>();
            BadgeSet = new List<BadgeSet>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response");

            if (jsonDictionary.ContainsKey("defaultSetType"))
                DefaultSetType = jsonDictionary["defaultSetType"].ToString();

            if (jsonDictionary.ContainsKey("badges"))
                foreach (var obj in (Dictionary<string, object>) jsonDictionary["badges"])
                {
                    Badges.Add(new Badge((Dictionary<string, object>) obj.Value));
                }

            if (jsonDictionary.ContainsKey("sets"))
                foreach (var obj in (object[]) ((Dictionary<string, object>) jsonDictionary["sets"])["groups"])
                {
                    BadgeSet.Add(new BadgeSet((Dictionary<string, object>) obj));
                }
        }
    }
}
