using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Notification
    {
        public string Type = "";
        public string Message = "";
        private string JSON = "";
        public Mayorship Mayor;
        public string notificationTrayunreadCount = "";
        public string TotalScore = "0";

        public Notification(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            Type = JSONDictionary["type"].ToString();
            switch (Type)
            {
                case "message":
                    Message += ((Dictionary<string, object>)JSONDictionary["item"])["message"].ToString() + "\r\n";
                    break;
                case "mayorship":
                    Message += ((Dictionary<string, object>)JSONDictionary["item"])["message"].ToString() + "\r\n";
                    Mayor = new Mayorship((Dictionary<string, object>)JSONDictionary["item"]);
                    break;

                case "leaderboard":
                    Message += ((Dictionary<string, object>)JSONDictionary["item"])["message"].ToString() + "\r\n";
                    break;

                case "tip":
                    //TODO: Notification Tips
                    //throw new Exception("Finish FourSquareNotification");
                    break;

                case "tipAlert":
                    //TODO: Notification Tips
                    //throw new Exception("Finish FourSquareNotification");
                    break;

                case "score":
                    int loc = JSON.IndexOf("points");
                    string newJSON = JSON.Substring(loc - 2, JSON.LastIndexOf(']') - loc + 2);
                    if (newJSON.LastIndexOf('}').Equals(newJSON.Length - 1))
                    {
                        List<string> JSONList = new List<string>();
                        while (newJSON.Contains("},{"))
                        {
                            JSONList.Add(newJSON.Substring(0, newJSON.IndexOf("},{") + 1));
                            newJSON = newJSON.Substring(newJSON.IndexOf("},{") + 2);
                        }
                        JSONList.Add(newJSON);
                        foreach (string str in JSONList)
                        {
                            Dictionary<string, object> dict = Helpers.JSONDeserializer(str);
                            Message += dict["message"].ToString() + " +" + dict["points"].ToString() + "|";
                        }
                        Message += "\r\n";
                    }
                    else
                    {
                        Dictionary<string, object> dict = Helpers.JSONDeserializer(newJSON);
                        Message += dict["message"].ToString() + " +" + dict["points"].ToString() + "\r\n";
                    }
                    TotalScore = ((Dictionary<string, object>)JSONDictionary["item"])["total"].ToString();
                    break;

                case "notificationTray":
                    notificationTrayunreadCount = ((Dictionary<string, object>)JSONDictionary["item"])["unreadCount"].ToString() + "\r\n";
                    break;

                default:
                    // throw new Exception("New Type of Notification");
                    break;
            }

        }
    }
}
