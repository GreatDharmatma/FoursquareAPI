using System.Collections.Generic;
using System.Linq;
using Brahmastra.FoursquareAPI.Entities.Notifications;
using Brahmastra.FoursquareAPI.IO;
using Brahmastra.FoursquareAPI.Entities;

namespace Brahmastra.FoursquareAPI
{
    public class Response
    {
        protected internal string Json;

        public string MetaCode { get; private set; }
        public string MetaErrorType { get; private set; }
        public string MetaErrorDetail { get; private set; }
        public Notification<BadgeNotification> BadgeNotification { get; private set; }
        public Notification<LeaderboardNotification> LeaderboardNotification { get; private set; }
        public Notification<MayorshipNotification> MayorshipNotification { get; private set; }
        public Notification<MessageNotification> MessageNotification { get; private set; }
        public Notification<ScoreNotification> ScoreNotification { get; private set; }
        public Notification<TipAlertNotification> TipAlertNotification { get; private set; }
        public Notification<TipNotification> TipNotification { get; private set; }

        public Response(Dictionary<string, object> jsonDictionary)
        {
            if (jsonDictionary.ContainsKey("notifications"))
                foreach ( var type in from obj in (object[]) jsonDictionary["notifications"] select jsonDictionary["type"].ToString())
                {
                    switch (type)
                    {
                        case "message":
                            MessageNotification =
                                new Notification<MessageNotification>(new MessageNotification(jsonDictionary),type);
                            break;
                        case "mayorship":
                            MayorshipNotification =
                                new Notification<MayorshipNotification>(new MayorshipNotification(jsonDictionary), type);
                            break;

                        case "leaderboard":
                            LeaderboardNotification =
                                new Notification<LeaderboardNotification>(new LeaderboardNotification(jsonDictionary),type);
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
                            /*int loc = JSON.IndexOf("points");
                            string newJSON = JSON.Substring(loc - 2, JSON.LastIndexOf(']') - loc + 2);
                            if (newJSON.LastIndexOf('}').Equals(newJSON.Length - 1))
                            {
                                var jsonList = new List<string>();
                                while (newJSON.Contains("},{"))
                                {
                                    jsonList.Add(newJSON.Substring(0, newJSON.IndexOf("},{", System.StringComparison.Ordinal) + 1));
                                    newJSON = newJSON.Substring(newJSON.IndexOf("},{", System.StringComparison.Ordinal) + 2);
                                }
                                jsonList.Add(newJSON);
                                foreach (var str in jsonList)
                                {
                                    var dict = Helpers.JsonDeserializer(str);
                                    //_message += dict["message"].ToString() + " +" + dict["points"].ToString() + "|";
                                    //_message += "\r\n";
                                }
                            }
                            else
                            {
                                Dictionary<string, object> dict = Helpers.JsonDeserializer(newJSON);
                                _message += dict["message"].ToString() + " +" + dict["points"].ToString() + "\r\n";
                            }
                            TotalScore = ((Dictionary<string, object>)jsonDictionary["item"])["total"].ToString();*/
                            break;

                        case "notificationTray":
                            //NotificationTrayunreadCount = ((Dictionary<string, object>)jsonDictionary["item"])["unreadCount"].ToString() + "\r\n";
                            break;

                            // throw new Exception("New Type of Notification");
                    }
                }
        }
    }
}
