using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class ToDo : Response
    {
        public string CreatedAt { get; private set; }
        public Tip Tip { get; private set; }
        public string Id { get; private set; }

        public ToDo(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:todo");
            Id = Helpers.GetDictionaryValue(jsonDictionary, "id");
            CreatedAt = Helpers.GetDictionaryValue(jsonDictionary, "createdAt");
            Tip = new Tip(jsonDictionary);
        }
    }
}
