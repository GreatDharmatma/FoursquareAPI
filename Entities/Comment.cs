using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Comment : Response
    {
        public string id;
        public string createdAt;
        public User user;
        public string text;

        public Comment(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:comment");
            id = JSONDictionary["id"].ToString();
            createdAt = JSONDictionary["createdAt"].ToString();
            user = new User((Dictionary<string, object>)JSONDictionary["user"]);
            text = JSONDictionary["text"].ToString();
        }
    }
}
