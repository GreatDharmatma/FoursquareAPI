using System;
using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Category : Response
    {
        public string Id { get; private set; }
        public string Icon { get; private set; }
        public List<string> Parents { get; private set; }
        public bool Primary { get; private set; }
        public string Name { get; private set; }
        public string PluralName { get; private set; }

        public Category(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Parents = new List<string>();
            Id = Helpers.GetDictionaryValue(jsonDictionary, "id");
            Icon = Helpers.GetDictionaryValue(jsonDictionary, "icon");
            try
            {
                foreach (var obj in ((object[])jsonDictionary["parents"]))
                {
                    Parents.Add((string)obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Primary = Helpers.GetDictionaryValue(jsonDictionary, "primary").Equals("True");
            Name = Helpers.GetDictionaryValue(jsonDictionary, "name");
            PluralName = Helpers.GetDictionaryValue(jsonDictionary, "pluralName");
        }

    }
}
