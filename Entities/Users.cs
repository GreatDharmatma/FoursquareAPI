using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Users : Response
    {
        public List<User> User { get; private set; }
        public int Count { get; private set; }

        public Users(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Count = 0;
            User = new List<User>();
            var itemsDictionary = Helpers.ExtractDictionary(jsonDictionary, "response");
            var items = new object[0];

            if (itemsDictionary.ContainsKey("requests"))
            {
                //User Friend requests
                Count = ((object[])itemsDictionary["requests"]).Length;
                items = ((object[])itemsDictionary["requests"]);
            }

            if (itemsDictionary.ContainsKey("results"))
            {
                //User search results
                Count = ((object[])itemsDictionary["results"]).Length;
                items = ((object[])itemsDictionary["results"]);
            }

            if (itemsDictionary.ContainsKey("friends"))
            {
                //User Friends
                if (((Dictionary<string, object>) itemsDictionary["friends"]).ContainsKey("count"))
                    Count = (int) ((Dictionary<string, object>) itemsDictionary["friends"])["count"];

                items = (object[])((Dictionary<string, object>)itemsDictionary["friends"])["items"];
            }

            foreach (var obj in items)
                User.Add(new User(((Dictionary<string, object>) obj)));
        }
    }
}
