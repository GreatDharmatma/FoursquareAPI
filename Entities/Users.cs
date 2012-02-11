using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Users : Response
    {
        public int count = 0;
        public List<User> users = new List<User>();

        public Users(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            Dictionary<string, object> itemsDictionary = new Dictionary<string, object>();
            itemsDictionary = Helpers.extractDictionary(JSONDictionary, "response");

            object[] items = new object[0];

            if (itemsDictionary.ContainsKey("requests"))
            {
                //User Friend requests
                count = ((object[])itemsDictionary["requests"]).Length;
                items = ((object[])itemsDictionary["requests"]);
            }

            if (itemsDictionary.ContainsKey("results"))
            {
                //User search results
                count = ((object[])itemsDictionary["results"]).Length;
                items = ((object[])itemsDictionary["results"]);
            }

            if (itemsDictionary.ContainsKey("friends"))
            {
                //User Friends
                if (((Dictionary<string, object>)itemsDictionary["friends"]).ContainsKey("count"))
                {
                    count = (int)((Dictionary<string, object>)itemsDictionary["friends"])["count"];
                }
                items = (object[])((Dictionary<string, object>)itemsDictionary["friends"])["items"];
            }

            for (int x = 0; x < items.Length; x++)
            {
                users.Add(new User(((Dictionary<string, object>)items[x])));
            }
        }
    }
}
