using System;
using System.Collections.Generic;
using System.Linq;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Venue : Response
    {
        private int _beenHere;

        public string Id { get; private set; }
        public string Name { get; private set; }
        public bool Verified { get; private set; }
        public Contact Contact { get; private set; }
        public Location Location { get; private set; }
        public List<Category> Categories { get; private set; }
        public List<Special> Specials { get; private set; }
        public object HereNow { get; private set; }
        public string Description { get; private set; }
        public Stats Stats { get; private set; }
        public Mayorship Mayor { get; private set; }
        public Dictionary<string, List<Tip>> Tips { get; private set; }
        public List<ToDo> Todos { get; private set; }
        public List<string> Tags { get; private set; }
        public string ShortUrl { get; private set; }
        public string Url { get; private set; }
        public string TimeZone { get; private set; }
        public List<Special> SpecialsNearby { get; private set; }
        public object Photos { get; private set; }
        
        public int BeenHere
        {
            get { return _beenHere; }
            set { _beenHere = value; }
        }

        public Venue(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Photos = "";
            SpecialsNearby = new List<Special>();
            Tags = new List<string>();
            Todos = new List<ToDo>();
            Tips = new Dictionary<string, List<Tip>>();
            Specials = new List<Special>();
            Categories = new List<Category>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, jsonDictionary.ContainsKey("response") ? "response:venue" : "venue");
            Id = Helpers.GetDictionaryValue(jsonDictionary, "id");
            Name = Helpers.GetDictionaryValue(jsonDictionary, "name");
            Verified = Helpers.GetDictionaryValue(jsonDictionary, "verified").Equals("True");

            if (jsonDictionary.ContainsKey("contact"))
                Contact = new Contact((Dictionary<string, object>) jsonDictionary["contact"]);

            if (jsonDictionary.ContainsKey("location"))
                Location = new Location((Dictionary<string, object>) jsonDictionary["location"]);

            if (jsonDictionary.ContainsKey("categories"))
                foreach (var obj in ((object[]) jsonDictionary["categories"]))
                    Categories.Add(new Category((Dictionary<string, object>) obj));

            if (jsonDictionary.ContainsKey("specials"))
                foreach (var obj in (object[]) jsonDictionary["specials"])
                    Specials.Add(new Special((Dictionary<string, object>) obj));

            if (jsonDictionary.ContainsKey("hereNow"))
            {

                if (((int)Helpers.ExtractDictionary(jsonDictionary, "hereNow")["count"]) > 0)
                {
                    //TODO here now
                    //throw new Exception("hereNow");
                }
            }

            Description = Helpers.GetDictionaryValue(jsonDictionary, "description");

            if (jsonDictionary.ContainsKey("stats"))
                Stats = new Stats((Dictionary<string, object>) jsonDictionary["stats"]);

            if (jsonDictionary.ContainsKey("mayor"))
            {
                Mayor = new Mayorship(Helpers.ExtractDictionary(jsonDictionary, "mayor"));
               // mayor.Checkins = Helpers.ExtractDictionary(jsonDictionary, "mayor")["count"].ToString();
            }

            if (jsonDictionary.ContainsKey("tips"))
            {
                foreach (var obj in (object[])Helpers.ExtractDictionary(jsonDictionary, "tips")["groups"])
                {
                    var group = ((Dictionary<string, object>)obj);
                    var tipList = (from tip in (object[]) ((Dictionary<string, object>) obj)["items"] select new Tip((Dictionary<string, object>) tip)).ToList();
                    Tips.Add(Helpers.GetDictionaryValue(group,"type"),tipList);
                }
            }

            if (jsonDictionary.ContainsKey("todos"))
            {
                //TODO: Todos
                //CRUEL IRONY
                if ((int)((Dictionary<string, Object>)jsonDictionary["todos"])["count"] > 0)
                {
                    //throw new Exception("todos");
                }
            }

            if (jsonDictionary.ContainsKey("tags"))
                foreach (var obj in (object[]) jsonDictionary["tags"])
                    Tags.Add((string) obj);

            if (jsonDictionary.ContainsKey("beenHere"))
                Int32.TryParse(((Dictionary<string, Object>) jsonDictionary["beenHere"])["count"].ToString(),
                               out _beenHere);
            ShortUrl = Helpers.GetDictionaryValue(jsonDictionary, "shortUrl");
            Url = Helpers.GetDictionaryValue(jsonDictionary, "url");
            TimeZone = Helpers.GetDictionaryValue(jsonDictionary, "timeZone");

            if (jsonDictionary.ContainsKey("specialsNearby"))
                foreach (var obj in (object[]) jsonDictionary["specialsNearby"])
                {
                    SpecialsNearby.Add(new Special((Dictionary<string, object>) obj));
                    throw new Exception("See if this actually worlks");
                }

            if (jsonDictionary.ContainsKey("photos"))
                if ((int) ((Dictionary<string, object>) jsonDictionary["photos"])["count"] > 0)
                {
                    //throw new Exception("To Do Item for this class");
                    //Todo
                }
        }
    }
}
