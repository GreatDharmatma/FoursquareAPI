using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class RecommendedVenues : Response
    {
        public Dictionary<string, string> keywords = new Dictionary<string, string>();
        public string warning = "";
        public Dictionary<string, List<recommends>> places = new Dictionary<string, List<recommends>>();


        public struct reason
        {
            public string type;
            public string message;
        }

        public struct recommends
        {
            public List<reason> reasons;
            public FourSquareVenue venue;
            public List<FourSquareTip> tips;
        }

        public FourSquareRecommendedVenues(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = ExtractDictionary(JSONDictionary, "response");
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

                List<recommends> recs = new List<recommends>();
                foreach (object ItemObj in (object[])((Dictionary<string, object>)GroupObj)["items"])
                {
                    recommends r = new recommends();
                    r.tips = new List<FourSquareTip>();
                    r.reasons = new List<reason>();

                    r.venue = new FourSquareVenue((Dictionary<string, object>)((Dictionary<string, object>)ItemObj)["venue"]);
                    if (((Dictionary<string, object>)ItemObj).ContainsKey("tips"))
                    {
                        foreach (object TipObj in (object[])((Dictionary<string, object>)ItemObj)["tips"])
                        {
                            r.tips.Add(new FourSquareTip((Dictionary<string, object>)TipObj));
                        }
                    }
                    foreach (object ReasonObj in (object[])ExtractDictionary((Dictionary<string, object>)ItemObj, "reasons")["items"])
                    {
                        reason reas = new reason();
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
