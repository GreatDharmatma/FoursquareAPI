using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
        public class ToDos : Response
        {
            public int count = 0;
            List<ToDo> todos = new List<ToDo>();

            public ToDos(Dictionary<string, object> JSONDictionary)
                : base(JSONDictionary)
            {
                JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:todos");
                if (JSONDictionary.ContainsKey("count"))
                {
                    count = (int)JSONDictionary["count"];
                }
                object[] Items = (object[])JSONDictionary["items"];

                for (int x = 0; x < Items.Length; x++)
                {
                    todos.Add(new ToDo(((Dictionary<string, object>)Items[x])));
                }
            }
        }
}
