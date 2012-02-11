using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Contact
    {
        public string twitter = "";
        public string facebook = "";
        public string email = "";
        public string phone = "";
        public string formattedPhone = "";
        private string JSON = "";

        public Contact(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            if (JSONDictionary.ContainsKey("twitter"))
            {
                twitter = Helpers.getDictionaryValue(JSONDictionary, "twitter");
            }
            if (JSONDictionary.ContainsKey("facebook"))
            {
                twitter = Helpers.getDictionaryValue(JSONDictionary, "facebook");
            }
            if (JSONDictionary.ContainsKey("email"))
            {
                twitter = Helpers.getDictionaryValue(JSONDictionary, "email");
            }
            if (JSONDictionary.ContainsKey("phone"))
            {
            	phone = Helpers.getDictionaryValue(JSONDictionary, "phone");
            }
            if (JSONDictionary.ContainsKey("formattedPhone"))
            {
                phone = Helpers.getDictionaryValue(JSONDictionary, "formattedPhone");
            }
        }
    }
}
