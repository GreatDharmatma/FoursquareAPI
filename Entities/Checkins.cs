using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Checkins : Response
    {
        public int count = 0;
        public List<Checkin> checkins = new List<Checkin>();

        public Checkins(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            Dictionary<string, object> itemsDictionary = Helpers.extractDictionary(JSONDictionary, "response");
            if (itemsDictionary.ContainsKey("recent"))
            {
                object[] Items = ((object[])itemsDictionary["recent"]);

                foreach (object obj in Items)
                {
                    checkins.Add(new Checkin((Dictionary<string, object>)obj));
                }
                count = checkins.Count;
            }
            if (itemsDictionary.ContainsKey("hereNow"))
            {
                itemsDictionary = Helpers.extractDictionary(itemsDictionary, "hereNow");
            }
            if (itemsDictionary.ContainsKey("checkins"))
            {
                itemsDictionary = Helpers.extractDictionary(itemsDictionary, "checkins");
            }
            if (itemsDictionary.ContainsKey("count"))
            {
                count = (int)itemsDictionary["count"];
            }
            if (itemsDictionary.ContainsKey("items"))
            {
                object[] Items = ((object[])itemsDictionary["items"]);

                foreach (object obj in Items)
                {
                    checkins.Add(new Checkin((Dictionary<string, object>)obj));
                }
            }
        }
    }
}
