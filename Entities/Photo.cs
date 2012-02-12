using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Photo : Response
    {
        public string Id { get; private set; }
        public string CreatedAt { get; private set; }
        public string Url { get; private set; }
        public List<PhotoSize> Sizes { get; private set; }
        public Source Source { get; private set; }
        public User User { get; private set; }
        public Venue Venue { get; private set; }
        public Tip Tip { get; private set; }
        public Checkin Checkin { get; private set; }

        public Photo(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Sizes = new List<PhotoSize>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:photo");
            Id = jsonDictionary["id"].ToString();
            CreatedAt = jsonDictionary["createdAt"].ToString();
            Url = jsonDictionary["url"].ToString();

            if (jsonDictionary.ContainsKey("sizes"))
                foreach (var sizeObj in (object[]) Helpers.ExtractDictionary(jsonDictionary, "sizes")["items"])
                    Sizes.Add(new PhotoSize((Dictionary<string, object>) sizeObj));

            if (jsonDictionary.ContainsKey("source"))
            {
                Source = new Source((Dictionary<string,object>) jsonDictionary["source"]);
            }

            if (jsonDictionary.ContainsKey("user"))
                User = new User((Dictionary<string, object>) jsonDictionary["user"]);

            if (jsonDictionary.ContainsKey("venue"))
                Venue = new Venue((Dictionary<string, object>) jsonDictionary["venue"]);

            if (jsonDictionary.ContainsKey("tip"))
            {
                Tip = new Tip((Dictionary<string, object>)jsonDictionary["tip"]);
            }

            if (jsonDictionary.ContainsKey("checkin"))
                Checkin = new Checkin((Dictionary<string, object>) jsonDictionary["checkin"]);
        }
    }
}
