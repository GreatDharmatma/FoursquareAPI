using System;
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Venue : Response
    {
        public string id = "";
        public string name = "";
        public bool verified = false;
        //public List<FourSquareNotification> Notifications = new List<FourSquareNotification>();
        public Contact contact;
        public Location location;
        public List<Category> categories = new List<Category>();
        public List<Special> specials = new List<Special>();
        public object hereNow;
        public string description = "";
        public Stats stats;
        public Mayorship mayor;
        public Dictionary<string, List<Tip>> tips = new Dictionary<string, List<Tip>>();
        public List<ToDo> todos = new List<ToDo>();
        public List<string> tags = new List<string>();
        public int beenHere = 0;
        public string shortUrl = "";
        public string url = "";
        public string timeZone = "";
        public List<Special> specialsNearby = new List<Special>();
        public object photos = "";

        public Venue(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            if (JSONDictionary.ContainsKey("response"))
            {
                JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:venue");
            }
            else
            {
                JSONDictionary = Helpers.extractDictionary(JSONDictionary, "venue");
            }
            id = Helpers.getDictionaryValue(JSONDictionary, "id");
            name = Helpers.getDictionaryValue(JSONDictionary, "name");
            verified = Helpers.getDictionaryValue(JSONDictionary, "verified").Equals("True");

            if (JSONDictionary.ContainsKey("contact"))
            {
                contact = new Contact((Dictionary<string, object>)JSONDictionary["contact"]);
            }

            if (JSONDictionary.ContainsKey("location"))
            {
                location = new Location((Dictionary<string, object>)JSONDictionary["location"]);
            }

            if (JSONDictionary.ContainsKey("categories"))
            {
                foreach (object obj in ((object[])JSONDictionary["categories"]))
                {
                    categories.Add(new Category((Dictionary<string, object>)obj));
                }
            }

            if (JSONDictionary.ContainsKey("specials"))
            {
                foreach (object Obj in (object[])JSONDictionary["specials"])
                {
                    specials.Add(new Special((Dictionary<string, object>)Obj));
                }
            }

            if (JSONDictionary.ContainsKey("hereNow"))
            {

                if (((int)Helpers.extractDictionary(JSONDictionary, "hereNow")["count"]) > 0)
                {
                    //TODO here now
                    //throw new Exception("hereNow");
                }
            }

            description = Helpers.getDictionaryValue(JSONDictionary, "description");

            if (JSONDictionary.ContainsKey("stats"))
            {
                stats = new Stats((Dictionary<string, object>)JSONDictionary["stats"]);
            }

            if (JSONDictionary.ContainsKey("mayor"))
            {
                mayor = new Mayorship(Helpers.extractDictionary(JSONDictionary, "mayor"));
                mayor.Checkins = Helpers.extractDictionary(JSONDictionary, "mayor")["count"].ToString();
            }

            if (JSONDictionary.ContainsKey("tips"))
            {
                foreach (object Obj in (object[])Helpers.extractDictionary(JSONDictionary, "tips")["groups"])
                {
                    Dictionary<string, object> Group = ((Dictionary<string, object>)Obj);
                    List<Tip> tipList = new List<Tip>();
                    foreach (object Tip in (object[])((Dictionary<string, object>)Obj)["items"])
                    {
                        tipList.Add(new Tip((Dictionary<string, object>)Tip));
                    }
                    tips.Add(Helpers.getDictionaryValue(Group,"type"),tipList);
                }
            }

            if (JSONDictionary.ContainsKey("todos"))
            {
                //TODO: Todos
                //CRUEL IRONY
                if ((int)((Dictionary<string, Object>)JSONDictionary["todos"])["count"] > 0)
                {
                    //throw new Exception("todos");
                }
            }

            if (JSONDictionary.ContainsKey("tags"))
            {
                foreach (object Obj in (object[])JSONDictionary["tags"])
                {
                    tags.Add((string)Obj);
                }
            }

            if (JSONDictionary.ContainsKey("beenHere"))
            {
                Int32.TryParse(((Dictionary<string, Object>)JSONDictionary["beenHere"])["count"].ToString(), out beenHere);
            }
            shortUrl = Helpers.getDictionaryValue(JSONDictionary, "shortUrl");
            url = Helpers.getDictionaryValue(JSONDictionary, "url");
            timeZone = Helpers.getDictionaryValue(JSONDictionary, "timeZone");

            if (JSONDictionary.ContainsKey("specialsNearby"))
            {
                foreach (object Obj in (object[])JSONDictionary["specialsNearby"])
                {
                    specialsNearby.Add(new Special((Dictionary<string, object>)Obj));
                    throw new Exception("See if this actually worlks");
                }
            }

            if (JSONDictionary.ContainsKey("photos"))
            {
                if ((int)((Dictionary<string, object>)JSONDictionary["photos"])["count"] > 0)
                {
                    //throw new Exception("To Do Item for this class");
                    //Todo
                }
            }
        }
    }
}
