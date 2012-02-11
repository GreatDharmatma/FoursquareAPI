using System;
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Tips : Response
    {
        public int count = 0;
        List<Tip> tips = new List<Tip>();

        public Tips(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
           JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response");
            if (JSONDictionary["tips"].GetType() == typeof(Dictionary<string, object>))
            {
                JSONDictionary = Helpers.extractDictionary(JSONDictionary, "tips");
                if (JSONDictionary.ContainsKey("count"))
                {
                    count = (int)JSONDictionary["count"];
                }
                object[] Items = (object[])JSONDictionary["items"];

                for (int x = 0; x < Items.Length; x++)
                {
                    tips.Add(new Tip(((Dictionary<string, object>)Items[x])));
                }
            }
            if (JSONDictionary["items"].GetType() == typeof(Object[]))
            {
                object[] Items = (object[])JSONDictionary["items"];

                foreach (object Obj in Items)
                {
                    tips.Add(new Tip(((Dictionary<string, object>)Obj)));
                }
                count = tips.Count;
            }
        }
    }
}
