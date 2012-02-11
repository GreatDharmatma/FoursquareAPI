using System;
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class User : Response
    {
        public string id = "";
        public string firstName = "";
        public string lastName = "";
        public string homeCity = "";
        public string photo = "";
        public string gender = "";
        public string relationship = "";
        public string type = "";
        public Contact contact;
        public string pings = "";
        public string badges = "0";
        public Checkins checkins;
        public string mayorships = "0";
        public List<Venue> mayorshipItems = new List<Venue>();
        public string tips = "0";
        public string todos = "0";
        public string friends = "0";
        public string followers = "0";
        public string requests = "0";
        public Score scores;

        public User(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:user");

            id = Helpers.getDictionaryValue(JSONDictionary, "id");

            firstName = Helpers.getDictionaryValue(JSONDictionary, "firstName");
            lastName = Helpers.getDictionaryValue(JSONDictionary, "lastName");
            homeCity = Helpers.getDictionaryValue(JSONDictionary, "homeCity");

            photo = Helpers.getDictionaryValue(JSONDictionary, "photo");
            gender = Helpers.getDictionaryValue(JSONDictionary, "gender");
            relationship = Helpers.getDictionaryValue(JSONDictionary, "relationship");

            photo = Helpers.getDictionaryValue(JSONDictionary, "photo");
            gender = Helpers.getDictionaryValue(JSONDictionary, "gender");
            relationship = Helpers.getDictionaryValue(JSONDictionary, "relationship");

            if (JSONDictionary.ContainsKey("badges"))
            {
                badges = Helpers.extractDictionary(JSONDictionary, "badges")["count"].ToString();
            }
            if (JSONDictionary.ContainsKey("mayorships"))
            {
                mayorships = Helpers.extractDictionary(JSONDictionary, "mayorships")["count"].ToString();
                foreach (object Obj in (Object[])Helpers.extractDictionary(JSONDictionary, "mayorships")["items"])
                {
                    mayorshipItems.Add(new Venue((Dictionary<string, object>)Obj));
                }
            }
            if (JSONDictionary.ContainsKey("checkins"))
            {
                checkins = new Checkins(Helpers.extractDictionary(JSONDictionary, "checkins"));
            }
            if (JSONDictionary.ContainsKey("friends"))
            {
                friends = Helpers.extractDictionary(JSONDictionary, "friends")["count"].ToString();
                //Todo: if the count >0, get the items
            }
            if (JSONDictionary.ContainsKey("followers"))
            {
                followers = Helpers.extractDictionary(JSONDictionary, "followers")["count"].ToString();
                //Todo: if the count >0, get the items
            }
            if (JSONDictionary.ContainsKey("requests"))
            {
                requests = Helpers.extractDictionary(JSONDictionary, "requests")["count"].ToString();
                //Todo: if the count >0, get the items
            }
            if (JSONDictionary.ContainsKey("tips"))
            {
                tips = Helpers.extractDictionary(JSONDictionary, "tips")["count"].ToString();
                //Todo: if the count >0, get the items
            }
            if (JSONDictionary.ContainsKey("todos"))
            {
                todos = Helpers.extractDictionary(JSONDictionary, "todos")["count"].ToString();
                //Todo: if the count >0, get the items
            }

            type = Helpers.getDictionaryValue(JSONDictionary, "type");
            if (JSONDictionary.ContainsKey("contact"))
            {
                contact = new Contact(Helpers.extractDictionary(JSONDictionary,"contact"));
            }
            pings = Helpers.getDictionaryValue(JSONDictionary, "pings");

            if (JSONDictionary.ContainsKey("scores"))
            {
                scores = new Score(Helpers.extractDictionary(JSONDictionary, "scores"));
            }
        }
    }
}
