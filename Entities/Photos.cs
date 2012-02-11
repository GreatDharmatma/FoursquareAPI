using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Photos : Response
    {
        public int count = 0;
        public List<Photo> photos = new List<Photo>();

        public Photos(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:photos");
            if (JSONDictionary.ContainsKey("count"))
            {
                count = (int)JSONDictionary["count"];
            }
            foreach (object Obj in (object[])JSONDictionary["items"])
            {
                photos.Add(new Photo((Dictionary<string, object>)Obj));
            }
        }
    }
}
