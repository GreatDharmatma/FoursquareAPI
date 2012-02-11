using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class RecommendedVenues : Response
    {
        public Dictionary<string, string> keywords = new Dictionary<string, string>();
        public string warning = "";
        public Dictionary<string, List<Recommends>> places = new Dictionary<string, List<Recommends>>();

        public RecommendedVenues(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response");
            foreach (object Obj in (object[])((Dictionary<string, object>)JSONDictionary["keywords"])["items"])
            {
                keywords.Add(((Dictionary<string, object>)Obj)["displayName"].ToString(), ((Dictionary<string, object>)Obj)["keyword"].ToString());
            }
            if (JSONDictionary.ContainsKey("warning"))
            {
                warning = ((Dictionary<string, object>)JSONDictionary["warning"])["text"].ToString();
            }
            foreach (object GroupObj in ((object[])JSONDictionary["groups"]))
            {
                string Type = ((Dictionary<string, object>)GroupObj)["type"].ToString();

                List<Recommends> recs = new List<Recommends>();
                foreach (object ItemObj in (object[])((Dictionary<string, object>)GroupObj)["items"])
                {
                    Recommends r = new Recommends();
                    r.tips = new List<Tip>();
                    r.reasons = new List<Reason>();

                    r.venue = new Venue((Dictionary<string, object>)((Dictionary<string, object>)ItemObj)["venue"]);
                    if (((Dictionary<string, object>)ItemObj).ContainsKey("tips"))
                    {
                        foreach (object TipObj in (object[])((Dictionary<string, object>)ItemObj)["tips"])
                        {
                            r.tips.Add(new Tip((Dictionary<string, object>)TipObj));
                        }
                    }
                    foreach (object ReasonObj in (object[])Helpers.extractDictionary((Dictionary<string, object>)ItemObj, "reasons")["items"])
                    {
                        Reason reas = new Reason();
                        reas.type = ((Dictionary<string, object>)ReasonObj)["type"].ToString();
                        reas.message = ((Dictionary<string, object>)ReasonObj)["message"].ToString();
                        r.reasons.Add(reas);
                    }
                    recs.Add(r);
                }
                places.Add(Type, recs);
            }
        }

    }
}
