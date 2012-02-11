using System;
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Checkin : Response
    {
        public string id = "";
        public string type = "";
        public string _private = "";
        public User user;
        public string timeZone = "";
        public Venue venue;
        public Location location;
        public string shout = "";
        public string createdAt = "";
        public Source source;
        public List<Photo> photos = new List<Photo>();
        public List<Comment> comments = new List<Comment>();
        public List<Overlaps> overlaps = new List<Overlaps>();
        public bool IsMayor = false;

        public Checkin(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:checkin");
            id = Helpers.getDictionaryValue(JSONDictionary, "id");
            type = Helpers.getDictionaryValue(JSONDictionary, "type");
            _private = Helpers.getDictionaryValue(JSONDictionary, "private");
            if (JSONDictionary.ContainsKey("user"))
            {
                user = new User((Dictionary<string, object>)JSONDictionary["user"]);
            }

            timeZone = Helpers.getDictionaryValue(JSONDictionary, "timeZone");

            if (JSONDictionary.ContainsKey("venue"))
            {
                venue = new Venue((Dictionary<string, object>)JSONDictionary["venue"]);
            }

            if (JSONDictionary.ContainsKey("location"))
            {
                location = new Location((Dictionary<string, object>)JSONDictionary["location"]);
            }

            shout = Helpers.getDictionaryValue(JSONDictionary, "shout");
            createdAt = Helpers.getDictionaryValue(JSONDictionary, "createdAt");

            if (JSONDictionary.ContainsKey("source"))
            {
                source = new Source((Dictionary<string, object>)JSONDictionary["source"]);
            }

            if (JSONDictionary.ContainsKey("photos"))
            {
                Dictionary<string, object> photoThings = (Dictionary<string, object>)JSONDictionary["photos"];
                if (Int32.Parse(photoThings["count"].ToString()) > 0)
                {
                    foreach (object PhotoObj in (object[])photoThings["items"])
                    {
                        photos.Add(new Photo((Dictionary<string, object>)PhotoObj));
                    }
                }
            }

            if (JSONDictionary.ContainsKey("comments"))
            {
                if (((object[])Helpers.extractDictionary(JSONDictionary, "comments")["items"]).Length > 0)
                {
                    foreach (object obj in ((object[])Helpers.extractDictionary(JSONDictionary, "comments")["items"]))
                    {
                        comments.Add(new Comment((Dictionary<string, object>)obj));
                    }
                }
            }

            if (JSONDictionary.ContainsKey("overlaps"))
            {
                foreach (object obj in ((object[])Helpers.extractDictionary(JSONDictionary, "overlaps")["items"]))
                {
                    overlaps.Add(new Overlaps((Dictionary<string, object>)obj));
                }
            }

            if (JSONDictionary.ContainsKey("isMayor"))
            {
                if (JSONDictionary["isMayor"].ToString().Equals("True"))
                {
                    IsMayor = true;
                }
            }
        }
    }
}
