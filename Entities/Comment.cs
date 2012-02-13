using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Comment : Response
    {
        public string Text { get; private set; }
        public User User { get; private set; }
        public string CreatedAt { get; private set; }
        public string Id { get; private set; }

        public Comment(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:comment");
            Id = jsonDictionary["id"].ToString();
            CreatedAt = jsonDictionary["createdAt"].ToString();
            User = new User((Dictionary<string, object>)jsonDictionary["user"]);
            Text = jsonDictionary["text"].ToString();
        }
    }
}
