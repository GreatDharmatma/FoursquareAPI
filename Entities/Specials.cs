using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Specials : Response
    {
        int count = 0;
        List<Special> specials = new List<Special>();

        public Specials(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:specials");
            foreach (object Obj in (object[])JSONDictionary["items"])
            {
                specials.Add(new Special((Dictionary<string, object>)Obj));
            }
            if (JSONDictionary.ContainsKey("count"))
            {
                count = (int)JSONDictionary["count"];
            }
            else
            {
                count = specials.Count;
            }
        }
    }
}
