using System;
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Tips : Response
    {
        public int Count { get; private set; }
        public List<Tip> Tip { get; private set; }

        public Tips(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Tip = new List<Tip>();
            Count = 0;
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response");
            if (jsonDictionary["tips"].GetType() == typeof(Dictionary<string, object>))
            {
                jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "tips");
                if (jsonDictionary.ContainsKey("count"))
                    Count = (int) jsonDictionary["count"];
                var items = (object[])jsonDictionary["items"];

                foreach (object obj in items)
                    Tip.Add(new Tip(((Dictionary<string, object>) obj)));
            }
            if (jsonDictionary["items"].GetType() == typeof(Object[]))
            {
                var items = (object[])jsonDictionary["items"];

                foreach (var obj in items)
                    Tip.Add(new Tip(((Dictionary<string, object>) obj)));
                Count = Tip.Count;
            }
        }
    }
}
