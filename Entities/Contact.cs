using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Contact
    {
        internal string Json = "";

        public string FormattedPhone { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Facebook { get; private set; }
        public string Twitter { get; private set; }

        public Contact(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);

            Twitter = jsonDictionary.ContainsKey("twitter") ? Helpers.GetDictionaryValue(jsonDictionary, "twitter") : "-";
            Facebook = jsonDictionary.ContainsKey("facebook") ? Helpers.GetDictionaryValue(jsonDictionary, "facebook") : "-";
            Email = jsonDictionary.ContainsKey("email") ? Helpers.GetDictionaryValue(jsonDictionary, "email") : "-";
            Phone = jsonDictionary.ContainsKey("phone") ? Helpers.GetDictionaryValue(jsonDictionary, "phone") : "-";
            FormattedPhone = jsonDictionary.ContainsKey("formattedPhone") ? Helpers.GetDictionaryValue(jsonDictionary, "formattedPhone") : "-";
        }
    }
}
