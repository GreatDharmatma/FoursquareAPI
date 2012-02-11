using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Links : Response
    {
        public int count = 0;
        List<Link> links = new List<Link>();

        public Links(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:links");
            if (JSONDictionary.ContainsKey("count"))
            {
                count = (int)JSONDictionary["count"];
            }
            foreach (object Obj in (object[])JSONDictionary["items"])
            {
                links.Add(new Link((Dictionary<string, object>)Obj));
            }
        }
    }
}
