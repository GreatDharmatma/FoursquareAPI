
using System;
using System.Collections.Generic;
using Brahmastra.FoursquareApi.Entities;
using Brahmastra.FoursquareApi.Entities.Notifications;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi
{
    public class Response
    {
        protected internal string Json;

        public string MetaCode { get; private set; }
        public string MetaErrorType { get; private set; }
        public string MetaErrorDetail { get; private set; }
        //public Notification<BadgeNotification> BadgeNotification { get; private set; }
        public Notification<LeaderboardNotification> LeaderboardNotification { get; private set; }
        public Notification<MayorshipNotification> MayorshipNotification { get; private set; }
        public Notification<MessageNotification> MessageNotification { get; private set; }
        public Notification<ScoreNotification> ScoreNotification { get; private set; }
        //public Notification<TipAlertNotification> TipAlertNotification { get; private set; }
        //public Notification<TipNotification> TipNotification { get; private set; }

        public Response(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);
            var meta = Helpers.ExtractDictionary(jsonDictionary, "meta");
            MetaCode = Helpers.GetDictionaryValue(meta, "code");
            MetaErrorType = Helpers.GetDictionaryValue(meta, "errorType");
            if (MetaErrorType.Contains("deprecated"))
            {
                throw new Exception("deprecated Call");
                //todo - handle this somehow.
            }
            MetaErrorDetail = Helpers.GetDictionaryValue(meta, "errorDetail");
            if (jsonDictionary.ContainsKey("notifications"))
                foreach (object obj in (object[])jsonDictionary["notifications"])
                {
                    var v = (Dictionary<string, object>) obj;
                    string type = v["type"].ToString();
                    switch (type)
                    {
                        case"badge":
                            //Implement some day!
                            break;
                        case "message":
                            MessageNotification =
                                new Notification<MessageNotification>(new MessageNotification((Dictionary<string, object>)v["item"]), type);
                            break;
                        case "mayorship":
                            MayorshipNotification =
                                new Notification<MayorshipNotification>(new MayorshipNotification((Dictionary<string, object>)v["item"]), type);
                            break;

                        case "leaderboard":
                            LeaderboardNotification =
                                new Notification<LeaderboardNotification>(new LeaderboardNotification((Dictionary<string, object>)v["item"]), type);
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
                            ScoreNotification = new Notification<ScoreNotification>(new ScoreNotification((Dictionary<string, object>)v["item"]), type);
                            break;

                        case "notificationTray":
                            //NotificationTrayunreadCount = ((Dictionary<string, object>)jsonDictionary["item"])["unreadCount"].ToString() + "\r\n";
                            break;
                    }
                }
        }
    }
}
