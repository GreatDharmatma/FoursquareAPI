using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Brahmastra.FoursquareApi.IO
{
    class Helpers
    {
        public static Dictionary<string, object> ExtractDictionary(Dictionary<string, object> jsonDictionary, string dictionaryPath)
        {
            var dictionaryObject = jsonDictionary;

            while (dictionaryPath.Length > 0)
            {
                string key;
                if (dictionaryPath.Contains(":"))
                {
                    key = dictionaryPath.Substring(0, dictionaryPath.IndexOf(":", System.StringComparison.Ordinal));
                    dictionaryPath = dictionaryPath.Substring(dictionaryPath.IndexOf(":", System.StringComparison.Ordinal) + 1);
                    if (dictionaryObject.ContainsKey(key))
                        dictionaryObject = (Dictionary<string, object>) dictionaryObject[key];
                    else
                        return dictionaryObject;
                }
                else
                {
                    key = dictionaryPath;
                    dictionaryPath = "";
                    if (dictionaryObject.ContainsKey(key))
                        dictionaryObject = (Dictionary<string, object>) dictionaryObject[key];
                    else
                        return dictionaryObject;
                }
            }
            return dictionaryObject;
        }

        public static string GetDictionaryValue(Dictionary<string, object> jsonDictionary, string key)
        {
            var returnString = "";
            if (jsonDictionary.ContainsKey(key))
            {
                returnString = jsonDictionary[key].ToString();
            }
            return returnString;
        }

        public static string JsonSerializer(Dictionary<string, object> dictionaryObject)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(dictionaryObject);
        }

        public static Dictionary<string, object> JsonDeserializer(string json)
        {
            if (json.StartsWith("XXX("))
                json = json.Substring(4, json.Length - 6);
            if (json.Equals("No Server Response"))
                return new Dictionary<string, object>();

            var jsonDeserializer = new JavaScriptSerializer();

            return (Dictionary<string, object>)jsonDeserializer.Deserialize(json, typeof(object));
        }
    }
}
