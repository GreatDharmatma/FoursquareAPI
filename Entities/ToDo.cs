using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class ToDo : Response
    {
        public string id = "";
        public string createdAt = "";
        public Tip tip;

        public ToDo(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:todo");
            id = Helpers.getDictionaryValue(JSONDictionary, "id");
            createdAt = Helpers.getDictionaryValue(JSONDictionary, "createdAt");
            tip = new Tip(JSONDictionary);
        }
    }
}
