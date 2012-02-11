using System;
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Category : Response
    {
        public string id = "";
        public string icon = "";
        public List<string> parents = new List<string>();
        public bool primary = false;
        public string name = "";
        public string pluralName = "";

        public Category(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            id = Helpers.getDictionaryValue(JSONDictionary, "id");
            icon = Helpers.getDictionaryValue(JSONDictionary, "icon");
            try
            {
                foreach (object obj in ((object[])JSONDictionary["parents"]))
                {
                    parents.Add((string)obj);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            primary = Helpers.getDictionaryValue(JSONDictionary, "primary").Equals("True");
            name = Helpers.getDictionaryValue(JSONDictionary, "name");
            pluralName = Helpers.getDictionaryValue(JSONDictionary, "pluralName");
        }
    }
}
