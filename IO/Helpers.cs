using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Brahmastra.FoursquareAPI.IO
{
    class Helpers
    {
        public static Dictionary<string, object> extractDictionary(Dictionary<string, object> JSONDictionary, string DictionaryPath)
        {
            string Key = "";
            Dictionary<string, object> DictionaryObject = JSONDictionary;

            while (DictionaryPath.Length > 0)
            {
                if (DictionaryPath.Contains(":"))
                {
                    Key = DictionaryPath.Substring(0, DictionaryPath.IndexOf(":"));
                    DictionaryPath = DictionaryPath.Substring(DictionaryPath.IndexOf(":") + 1);
                    if (DictionaryObject.ContainsKey(Key))
                    {
                        DictionaryObject = (Dictionary<string, object>)DictionaryObject[Key];
                    }
                    else
                    {
                        return DictionaryObject;
                    }
                }
                else
                {
                    Key = DictionaryPath;
                    DictionaryPath = "";
                    if (DictionaryObject.ContainsKey(Key))
                    {
                        DictionaryObject = (Dictionary<string, object>)DictionaryObject[Key];
                    }
                    else
                    {
                        return DictionaryObject;
                    }
                }
            }
            return DictionaryObject;
        }

        public static string getDictionaryValue(Dictionary<string, object> JSONDictionary, string Key)
        {
            string ReturnString = "";
            if (JSONDictionary.ContainsKey(Key))
            {
                ReturnString = JSONDictionary[Key].ToString();
            }
            return ReturnString;
        }

        public static string JSONSerializer(Dictionary<string, object> DictionaryObject)
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            return JSONSerializer.Serialize(DictionaryObject);
        }

        public static Dictionary<string, object> JSONDeserializer(string JSON)
        {
            if (JSON.StartsWith("XXX("))
            {
                JSON = JSON.Substring(4, JSON.Length - 6);
            }
            if (JSON.Equals("No Server Response"))
            {
                return new Dictionary<string, object>();
            }

            JavaScriptSerializer JSONDeserializer = new JavaScriptSerializer();

            return (Dictionary<string, object>)JSONDeserializer.Deserialize(JSON, typeof(object));
        }
    }
}
