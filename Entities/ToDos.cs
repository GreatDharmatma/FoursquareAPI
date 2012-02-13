using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
        public class ToDos : Response
        {
            public List<ToDo> Todos { get; private set; }
            public int Count { get; private set; }

            public ToDos(Dictionary<string, object> jsonDictionary)
                : base(jsonDictionary)
            {
                Todos = new List<ToDo>();
                jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:todos");
                Count = jsonDictionary.ContainsKey("count") ? (int) jsonDictionary["count"] : 0;
                var items = (object[])jsonDictionary["items"];
                foreach (var obj in items)
                    Todos.Add(new ToDo(((Dictionary<string, object>) obj)));
            }
        }
}
