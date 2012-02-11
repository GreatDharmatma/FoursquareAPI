using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Photo : Response
    {
        public string id = "";
        public string createdAt = "";
        public string url = "";
        public List<PhotoSize> sizes = new List<PhotoSize>();
        public Source source;
        public User user;
        public Venue venue;
        public Tip tip;
        public Checkin checkin;

        public Photo(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:photo");
            id = JSONDictionary["id"].ToString();
            createdAt = JSONDictionary["createdAt"].ToString();
            url = JSONDictionary["url"].ToString();

            if (JSONDictionary.ContainsKey("sizes"))
            {
                foreach (object SizeObj in (object[])Helpers.extractDictionary(JSONDictionary, "sizes")["items"])
                {
                    sizes.Add(new PhotoSize((Dictionary<string, object>)SizeObj));
                }
            }

            if (JSONDictionary.ContainsKey("source"))
            {
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

            if (JSONDictionary.ContainsKey("tip"))
            {
                //todo
            }

            if (JSONDictionary.ContainsKey("checkin"))
            {
                checkin = new Checkin((Dictionary<string, object>)JSONDictionary["checkin"]);
            }
        }
    }
}
