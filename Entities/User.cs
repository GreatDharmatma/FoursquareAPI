using System;
using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class User : Response
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string HomeCity { get; private set; }
        public string Photo { get; private set; }
        public string Gender { get; private set; }
        public string Relationship { get; private set; }
        public string Type { get; private set; }
        public Contact Contact { get; private set; }
        public string Pings { get; private set; }
        public string Badges { get; private set; }
        public Checkins Checkins { get; private set; }
        public string Mayorships { get; private set; }
        public List<Venue> MayorshipItems { get; private set; }
        public string Tips { get; private set; }
        public string Friends { get; private set; }
        public string Todos { get; private set; }
        public string Followers { get; private set; }
        public string Requests { get; private set; }
        public Score Scores { get; private set; }
        
        public User(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Requests = "0";
            Followers = "0";
            Todos = "0";
            Friends = "0";
            Tips = "0";
            MayorshipItems = new List<Venue>();
            Mayorships = "0";
            Badges = "0";
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:user");

            Id = Helpers.GetDictionaryValue(jsonDictionary, "id");

            FirstName = Helpers.GetDictionaryValue(jsonDictionary, "firstName");
            LastName = Helpers.GetDictionaryValue(jsonDictionary, "lastName");
            HomeCity = Helpers.GetDictionaryValue(jsonDictionary, "homeCity");

            Photo = Helpers.GetDictionaryValue(jsonDictionary, "photo");
            Gender = Helpers.GetDictionaryValue(jsonDictionary, "gender");
            Relationship = Helpers.GetDictionaryValue(jsonDictionary, "relationship");

            Photo = Helpers.GetDictionaryValue(jsonDictionary, "photo");
            Gender = Helpers.GetDictionaryValue(jsonDictionary, "gender");
            Relationship = Helpers.GetDictionaryValue(jsonDictionary, "relationship");

            if (jsonDictionary.ContainsKey("badges"))
                Badges = Helpers.ExtractDictionary(jsonDictionary, "badges")["count"].ToString();

            if (jsonDictionary.ContainsKey("mayorships"))
            {
                Mayorships = Helpers.ExtractDictionary(jsonDictionary, "mayorships")["count"].ToString();
                foreach (var obj in (Object[]) Helpers.ExtractDictionary(jsonDictionary, "mayorships")["items"])
                    MayorshipItems.Add(new Venue((Dictionary<string, object>) obj));
            }
            if (jsonDictionary.ContainsKey("checkins"))
                Checkins = new Checkins(Helpers.ExtractDictionary(jsonDictionary, "checkins"));

            if (jsonDictionary.ContainsKey("friends"))
            {
                Friends = Helpers.ExtractDictionary(jsonDictionary, "friends")["count"].ToString();
                //Todo: if the count >0, get the items
            }
            if (jsonDictionary.ContainsKey("followers"))
            {
                Followers = Helpers.ExtractDictionary(jsonDictionary, "followers")["count"].ToString();
                //Todo: if the count >0, get the items
            }
            if (jsonDictionary.ContainsKey("requests"))
            {
                Requests = Helpers.ExtractDictionary(jsonDictionary, "requests")["count"].ToString();
                //Todo: if the count >0, get the items
            }
            if (jsonDictionary.ContainsKey("tips"))
            {
                Tips = Helpers.ExtractDictionary(jsonDictionary, "tips")["count"].ToString();
                //Todo: if the count >0, get the items
            }
            if (jsonDictionary.ContainsKey("todos"))
            {
                Todos = Helpers.ExtractDictionary(jsonDictionary, "todos")["count"].ToString();
                //Todo: if the count >0, get the items
            }

            Type = Helpers.GetDictionaryValue(jsonDictionary, "type");
            if (jsonDictionary.ContainsKey("contact"))
                Contact = new Contact(Helpers.ExtractDictionary(jsonDictionary, "contact"));

            Pings = Helpers.GetDictionaryValue(jsonDictionary, "pings");
            if (jsonDictionary.ContainsKey("scores"))
                Scores = new Score(Helpers.ExtractDictionary(jsonDictionary, "scores"));
        }
    }
}
