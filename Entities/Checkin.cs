using System;
using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Checkin : Response
    {
        public string Id { get; private set; }
        public string Type { get; private set; }
        public string Private { get; private set; }
        public User User { get; private set; }
        public string TimeZone { get; private set; }
        public Venue Venue { get; private set; }
        public Location Location { get; private set; }
        public string Shout { get; private set; }
        public string CreatedAt { get; private set; }
        public Source Source { get; private set; }
        public List<Photo> Photos { get; private set; }
        public List<Comment> Comments { get; private set; }
        public List<Overlaps> Overlaps { get; private set; }
        public bool IsMayor { get; private set; }


        public Checkin(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Photos = new List<Photo>();
            IsMayor = false;
            Overlaps = new List<Overlaps>();
            Comments = new List<Comment>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:checkin");
            Id = Helpers.GetDictionaryValue(jsonDictionary, "id");
            Type = Helpers.GetDictionaryValue(jsonDictionary, "type");
            Private = Helpers.GetDictionaryValue(jsonDictionary, "private");

            if (jsonDictionary.ContainsKey("user"))
                User = new User((Dictionary<string, object>) jsonDictionary["user"]);

            TimeZone = Helpers.GetDictionaryValue(jsonDictionary, "timeZone");
            if (jsonDictionary.ContainsKey("venue"))
                Venue = new Venue((Dictionary<string, object>) jsonDictionary["venue"]);

            if (jsonDictionary.ContainsKey("location"))
                Location = new Location((Dictionary<string, object>) jsonDictionary["location"]);

            Shout = Helpers.GetDictionaryValue(jsonDictionary, "shout");
            CreatedAt = Helpers.GetDictionaryValue(jsonDictionary, "createdAt");
            if (jsonDictionary.ContainsKey("source"))
                Source = new Source((Dictionary<string, object>) jsonDictionary["source"]);

            if (jsonDictionary.ContainsKey("photos"))
            {
                var photoThings = (Dictionary<string, object>)jsonDictionary["photos"];
                if (Int32.Parse(photoThings["count"].ToString()) > 0)
                    foreach (var photoObj in (object[]) photoThings["items"])
                        Photos.Add(new Photo((Dictionary<string, object>) photoObj));
            }
            if (jsonDictionary.ContainsKey("comments"))
                if (((object[]) Helpers.ExtractDictionary(jsonDictionary, "comments")["items"]).Length > 0)
                    foreach (var obj in ((object[]) Helpers.ExtractDictionary(jsonDictionary, "comments")["items"]))
                        Comments.Add(new Comment((Dictionary<string, object>) obj));

            if (jsonDictionary.ContainsKey("overlaps"))
                foreach (var obj in ((object[]) Helpers.ExtractDictionary(jsonDictionary, "overlaps")["items"]))
                    Overlaps.Add(new Overlaps((Dictionary<string, object>) obj));

            if (jsonDictionary.ContainsKey("isMayor"))
                if (jsonDictionary["isMayor"].ToString().Equals("True"))
                    IsMayor = true;
        }
    }
}
