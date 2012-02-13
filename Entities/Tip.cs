using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Tip : Response
    {
        public object Done { get; private set; }
        public int Todocount { get; private set; }
        public User User { get; private set; }
        public Venue Venue { get; private set; }
        public object Photo { get; private set; }
        public string Url { get; private set; }
        public string Status { get; private set; }
        public string CreatedAt { get; private set; }
        public string Text { get; private set; }
        public string Id { get; private set; }
        
        public Tip(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Photo = "";
            Todocount = 0;
            Done = "";
            if (jsonDictionary.ContainsKey("response"))
                jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response");
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "tip");
            Id = Helpers.GetDictionaryValue(jsonDictionary, "id");
            Text = Helpers.GetDictionaryValue(jsonDictionary, "text");
            CreatedAt = Helpers.GetDictionaryValue(jsonDictionary, "createdAt");
            Status = Helpers.GetDictionaryValue(jsonDictionary, "status");
            Url = Helpers.GetDictionaryValue(jsonDictionary, "url");

            if (jsonDictionary.ContainsKey("photo"))
            {
                //throw new Exception("To Do Item for this class");
                //todo
            }

            if (jsonDictionary.ContainsKey("user"))
                User = new User((Dictionary<string, object>) jsonDictionary["user"]);
            if (jsonDictionary.ContainsKey("venue"))
                Venue = new Venue((Dictionary<string, object>) jsonDictionary["venue"]);
            if (jsonDictionary.ContainsKey("todo"))
                if (((int) ((Dictionary<string, object>) jsonDictionary["todo"])["count"]) > 0)
                    Todocount = ((int) ((Dictionary<string, object>) jsonDictionary["todo"])["count"]);
            if (jsonDictionary.ContainsKey("done"))
            {
                if (((Dictionary<string, object>)jsonDictionary["done"]).ContainsKey("groups"))
                {
                    //throw new Exception("To Do Item for this class");
                    //todo
                }
                if (((Dictionary<string, object>)jsonDictionary["done"]).ContainsKey("friends"))
                {
                    //throw new Exception("To Do Item for this class");
                    //todo
                }
            }
        }

    }
}
