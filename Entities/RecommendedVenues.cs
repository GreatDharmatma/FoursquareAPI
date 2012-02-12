using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class RecommendedVenues : Response
    {
        public Dictionary<string, string> Keywords { get; private set; }
        public string Warning { get; private set; }
        public Dictionary<string, List<Recommends>> Places { get; private set; }

        public RecommendedVenues(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Places = new Dictionary<string, List<Recommends>>();
            Warning = "";
            Keywords = new Dictionary<string, string>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response");
            foreach (var obj in (object[]) ((Dictionary<string, object>) jsonDictionary["keywords"])["items"])
                Keywords.Add(((Dictionary<string, object>) obj)["displayName"].ToString(),
                             ((Dictionary<string, object>) obj)["keyword"].ToString());
            if (jsonDictionary.ContainsKey("warning"))
                Warning = ((Dictionary<string, object>) jsonDictionary["warning"])["text"].ToString();
            foreach (var groupObj in ((object[])jsonDictionary["groups"]))
            {
                var type = ((Dictionary<string, object>)groupObj)["type"].ToString();
                var recs = new List<Recommends>();
                foreach (var itemObj in (object[])((Dictionary<string, object>)groupObj)["items"])
                {
                    var r = new Recommends {
                        Venue = new Venue((Dictionary<string, object>) ((Dictionary<string, object>) itemObj)["venue"])};

                    if (((Dictionary<string, object>) itemObj).ContainsKey("tips"))
                        foreach (var tipObj in (object[]) ((Dictionary<string, object>) itemObj)["tips"])
                            r.Tips.Add(new Tip((Dictionary<string, object>) tipObj));
                    foreach (var reasonObj in (object[])Helpers.ExtractDictionary((Dictionary<string, object>)itemObj, "reasons")["items"])
                    {
                        var reas = new Reason { 
                            Type = ((Dictionary<string, object>) reasonObj)["type"].ToString(),
                            Message = ((Dictionary<string, object>) reasonObj)["message"].ToString()};

                        r.Reasons.Add(reas);
                    }
                    recs.Add(r);
                }
                Places.Add(type, recs);
            }
        }

    }
}
