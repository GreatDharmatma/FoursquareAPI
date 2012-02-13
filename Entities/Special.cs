using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;
using System;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Special : Response
    {
        public string Id { get; private set; }
        public string Type { get; private set; }
        public string Message { get; private set; }
        public string Description { get; private set; }
        public string FinePrint { get; private set; }
        public bool Unlocked { get; private set; }
        public string Icon { get; private set; }
        public string Title { get; private set; }
        public string State { get; private set; }
        public string Progress { get; private set; }
        public string ProgressDescription { get; private set; }
        public string Detail { get; private set; }
        public string Target { get; private set; }
        public List<User> FriendsHere { get; private set; }
        public Venue Venue { get; private set; }

        public Special(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            FriendsHere = new List<User>();
            Unlocked = false;

            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:special");
            Id = Helpers.GetDictionaryValue(jsonDictionary, "id");
            Type = Helpers.GetDictionaryValue(jsonDictionary, "type");
            Message = Helpers.GetDictionaryValue(jsonDictionary, "message");
            Description = Helpers.GetDictionaryValue(jsonDictionary, "description");
            FinePrint = Helpers.GetDictionaryValue(jsonDictionary, "finePrint");
            if (Helpers.GetDictionaryValue(jsonDictionary, "unlocked").ToLower().Equals("true"))
                Unlocked = true;
            Icon = Helpers.GetDictionaryValue(jsonDictionary, "icon");
            Title = Helpers.GetDictionaryValue(jsonDictionary, "title");
            State = Helpers.GetDictionaryValue(jsonDictionary, "state");
            Progress = Helpers.GetDictionaryValue(jsonDictionary, "progress");
            ProgressDescription = Helpers.GetDictionaryValue(jsonDictionary, "progressDescription");
            Detail = Helpers.GetDictionaryValue(jsonDictionary, "detail");
            Target = Helpers.GetDictionaryValue(jsonDictionary, "target");
            if (jsonDictionary.ContainsKey("friendsHere"))
                throw new Exception("Todo");
            if (jsonDictionary.ContainsKey("venue"))
                Venue = new Venue((Dictionary<string, object>) jsonDictionary["venue"]);
        }
    }
}
