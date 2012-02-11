using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Tip : Response
    {
        public string id = "";
        public string text = "";
        public string createdAt = "";
        public string status = "";
        public string url = "";
        public object photo = "";
        public User user;
        public Venue venue;
        public int todocount = 0;
        public object done = "";

        public Tip(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            if (JSONDictionary.ContainsKey("response"))
            {
                JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response");
            }
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "tip");
            id = Helpers.getDictionaryValue(JSONDictionary, "id");
            text = Helpers.getDictionaryValue(JSONDictionary, "text");
            createdAt = Helpers.getDictionaryValue(JSONDictionary, "createdAt");
            status = Helpers.getDictionaryValue(JSONDictionary, "status");
            url = Helpers.getDictionaryValue(JSONDictionary, "url");

            if (JSONDictionary.ContainsKey("photo"))
            {
                //throw new Exception("To Do Item for this class");
                //todo
            }

            if (JSONDictionary.ContainsKey("user"))
            {
                user = new User((Dictionary<string, object>)JSONDictionary["user"]);
            }
            if (JSONDictionary.ContainsKey("venue"))
            {
                venue = new Venue((Dictionary<string, object>)JSONDictionary["venue"]);
            }
            if (JSONDictionary.ContainsKey("todo"))
            {
                if (((int)((Dictionary<string, object>)JSONDictionary["todo"])["count"]) > 0)
                {
                    todocount = ((int)((Dictionary<string, object>)JSONDictionary["todo"])["count"]);
                }
            }
            if (JSONDictionary.ContainsKey("done"))
            {
                if (((Dictionary<string, object>)JSONDictionary["done"]).ContainsKey("groups"))
                {
                    //throw new Exception("To Do Item for this class");
                    //todo
                }
                if (((Dictionary<string, object>)JSONDictionary["done"]).ContainsKey("friends"))
                {
                    //throw new Exception("To Do Item for this class");
                    //todo
                }
            }
        }

    }
}
