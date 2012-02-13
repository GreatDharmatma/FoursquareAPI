using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Checkins : Response
    {
        public List<Checkin> CheckinsList { get; private set; }
        public int Count { get; private set; }

        public Checkins(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Count = 0;
            CheckinsList = new List<Checkin>();
            var itemsDictionary = Helpers.ExtractDictionary(jsonDictionary, "response");
            if (itemsDictionary.ContainsKey("recent"))
            {
                var items = ((object[])itemsDictionary["recent"]);
                foreach (var obj in items)
                    CheckinsList.Add(new Checkin((Dictionary<string, object>) obj));

                Count = CheckinsList.Count;
            }
            if (itemsDictionary.ContainsKey("hereNow"))
                itemsDictionary = Helpers.ExtractDictionary(itemsDictionary, "hereNow");

            if (itemsDictionary.ContainsKey("checkins"))
                itemsDictionary = Helpers.ExtractDictionary(itemsDictionary, "checkins");

            if (itemsDictionary.ContainsKey("count"))
                Count = (int) itemsDictionary["count"];

            if (itemsDictionary.ContainsKey("items"))
            {
                var items = ((object[])itemsDictionary["items"]);

                foreach (var obj in items)
                    CheckinsList.Add(new Checkin((Dictionary<string, object>) obj));
            }
        }
    }
}
