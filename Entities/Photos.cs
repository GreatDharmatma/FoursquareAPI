using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Photos : Response
    {
        public int Count { get; private set; }
        public List<Photo> Photo { get; private set; }

        public Photos(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Photo = new List<Photo>();
            Count = 0;
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:photos");
            if (jsonDictionary.ContainsKey("count"))
                Count = (int) jsonDictionary["count"];

            foreach (var obj in (object[]) jsonDictionary["items"])
                Photo.Add(new Photo((Dictionary<string, object>) obj));
        }
    }
}
