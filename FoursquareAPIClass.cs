using System;
using System.Collections.Generic;
using System.IO;
using Brahmastra.FoursquareAPI.Entities;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI
{

    class FoursquareAPIClass
    {
        private string version = "20110210";
        private string accessToken;

        public FoursquareAPIClass(string accessToken)
        {
            this.accessToken = accessToken;
        }
        
        #region Users

        /// <summary>
        /// Returns profile information for a given user, including selected badges and mayorships. 
        /// </summary>
        /// <param name="USERID">Identity of the user to get details for. Pass self to get details of the acting user</param>
        public User getUser(string USER_ID)
        {
            if (USER_ID.Equals(""))
            {
                USER_ID = "self";
            }
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/users/" + USER_ID + "?callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new User(JSONDictionary);
        }

        /// <summary>
        /// Returns the user's leaderboard.  
        /// </summary>
        /// <param name="neighbors">Number of friends' scores to return that are adjacent to your score, in ranked order. </param>
        public Leaderboard getLeaderboard()
        {
            return getLeaderboard(2);
        }

        /// <summary>
        /// Returns the user's leaderboard.  
        /// </summary>
        /// <param name="neighbors">Number of friends' scores to return that are adjacent to your score, in ranked order. </param>
        public Leaderboard getLeaderboard(int Neighbors)
        {
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/users/leaderboard?neighbors=" + Neighbors.ToString() + "&callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Leaderboard(JSONDictionary);
        }

        /// <summary>
        /// Helps a user locate friends.   
        /// </summary>
        /// <param name="Phone">A comma-delimited list of phone numbers to look for.</param>
        /// <param name="EMail">A comma-delimited list of email addresses to look for.</param>
        /// <param name="Twitter">A comma-delimited list of Twitter handles to look for.</param>
        /// <param name="TwitterSource">A single Twitter handle. Results will be friends of this user who use Foursquare.</param>
        /// <param name="Fbid">A comma-delimited list of Facebook ID's to look for.</param>
        /// <param name="Name">A single string to search for in users' names</param>
        public Users searchUser(string Phone, string Email, string Twitter, string TwitterSource, string Fbid, string Name)
        {
            HTTPGet GET = new HTTPGet();
            string Query = "";

            //Phone
            if (!Phone.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "phone=" + Phone;
            }

            //Email
            if (!Email.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "email=" + Email;
            }

            //Twitter
            if (!Twitter.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "twitter=" + Twitter;
            }

            //TwitterSource
            if (!TwitterSource.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "twitterSource=" + TwitterSource;
            }

            //Fbid
            if (!Fbid.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "fbid=" + Fbid;
            }

            //Name
            if (!Name.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "name=" + Name;
            }

            string EndPoint = "https://api.foursquare.com/v2/users/search" + Query + "&callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Users(JSONDictionary);
        }

        /// <summary>
        /// Shows a user the list of users with whom they have a pending friend request    
        /// </summary>
        public Users getUserRequests()
        {
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/users/requests" + "?callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Users(JSONDictionary);
        }

        /// <summary>
        /// Returns badges for a given user.    
        /// </summary>
        /// <param name="USER_ID">ID for user to view badges for..</param>
        public BadgeSets getUserBadges(string USER_ID)
        {
            if (USER_ID.Equals(""))
            {
                USER_ID = "self";
            }
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/users/" + USER_ID + "/badges?callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new BadgeSets(JSONDictionary);
        }

        /// <summary>
        /// Returns a history of checkins for the authenticated user. 
        /// </summary>
        /// <param name="USER_ID">For now, only "self" is supported</param>
        public Checkins getUserCheckins(string USER_ID)
        {
            return  getUserCheckins(USER_ID, 100, 0, 0, 0);
        }

        /// <summary>
        /// Returns a history of checkins for the authenticated user. 
        /// </summary>
        /// <param name="USER_ID">For now, only "self" is supported</param>
        /// <param name="Limit">For now, only "self" is supported</param>
        /// <param name="Offset">Used to page through results.</param>
        /// <param name="afterTimestamp">Retrieve the first results to follow these seconds since epoch.</param>
        /// <param name="beforeTimeStamp">Retrieve the first results prior to these seconds since epoch</param>
        public Checkins getUserCheckins(string USER_ID, int Limit, int Offset, double afterTimestamp, double beforeTimeStamp)
        {
            HTTPGet GET = new HTTPGet();
            string Query = "";

            if (USER_ID.Equals(""))
            {
                USER_ID = "self";
            }

            if (Limit > 0)
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + Limit.ToString();
            }

            if (Offset > 0)
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "offset=" + Offset.ToString();
            }

            if (afterTimestamp > 0)
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "afterTimestamp=" + afterTimestamp.ToString();
            }

            if (beforeTimeStamp > 0)
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "beforeTimeStamp=" + beforeTimeStamp.ToString();
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }
            string EndPoint = "https://api.foursquare.com/v2/users/" + USER_ID + "/checkins" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Checkins(JSONDictionary);
        }

        /// <summary>
        /// Returns an array of a user's friends. 
        /// </summary>
        /// <param name="USER_ID">Identity of the user to get friends of. Pass self to get friends of the acting user</param>
        public Users getFriends(string USER_ID)
        {
            return getFriends(USER_ID, 0, 0);
        }

        /// <summary>
        /// Returns an array of a user's friends. 
        /// </summary>
        /// <param name="USER_ID">Identity of the user to get friends of. Pass self to get friends of the acting user</param>
        /// <param name="Limit">Number of results to return, up to 500.</param>
        /// <param name="Offset">Used to page through results</param>
        public Users getFriends(string USER_ID, int Limit, int Offset)
        {
            List<User> friends = new List<User>();

            string Query = "";

            if (USER_ID.Equals(""))
            {
                USER_ID = "self";
            }

            if (Limit > 0)
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + Limit.ToString();
            }

            if (Offset > 0)
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "offset=" + Offset.ToString();
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }

            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/users/" + USER_ID + "/friends" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Users(JSONDictionary);
        }

        /// <summary>
        /// Returns a user's mayorships 
        /// </summary>
        /// <param name="USER_ID">Identity of the user to get mayorships for. Pass self to get friends of the acting user.</param>
        public Mayorships getMayorships(string USER_ID)
        {
            if (USER_ID.Equals(""))
            {
                USER_ID = "self";
            }

            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/users/" + USER_ID + "/mayorships" + "?callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Mayorships(JSONDictionary);
        }

        /// <summary>
        /// Returns tips from a user.  
        /// </summary>
        /// <param name="USER_ID">Identity of the user to get tips from. Pass self to get tips of the acting user.</param>
        /// <param name="Sort">One of recent, nearby, or popular. Nearby requires geolat and geolong to be provided.</param>
        /// <param name="LL">Latitude and longitude of the user's location. (Comma separated)</param>
        /// <param name="Limit">Number of results to return, up to 500.</param>
        /// <param name="Offset">Used to page through results</param>
        public Tips getTips(string USER_ID, string Sort, string LL, int Limit, int Offset)
        {
            if (USER_ID.Equals(""))
            {
                USER_ID = "self";
            }

            string Query = "";

            if (!Sort.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "sort=" + Sort;
            }

            if (!LL.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "ll=" + LL;
            }

            if (Limit > 0)
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + Limit.ToString();
            }

            if (Offset > 0)
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "offset=" + Offset.ToString();
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }


            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/users/" + USER_ID + "/tips" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Tips(JSONDictionary);
        }

        /// <summary>
        /// Returns todos from a user. 
        /// </summary>
        /// <param name="USER_ID">Identity of the user to get todos for. Pass self to get todos of the acting user.</param>
        /// <param name="Sort">One of recent, nearby, or popular. Nearby requires geolat and geolong to be provided.</param>
        /// <param name="LL">Latitude and longitude of the user's location (Comma separated)</param>
        public ToDos getTodos(string USER_ID, string Sort, string LL)
        {
            if (USER_ID.Equals(""))
            {
                USER_ID = "self";
            }

            string Query = "";

            if (!Sort.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "sort=" + Sort;
            }

            if (!LL.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "ll=" + LL;
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }


            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/users/" + USER_ID + "/todos" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new ToDos(JSONDictionary);
        }

        /// <summary>
        /// Returns a list of all venues visited by the specified user, along with how many visits and when they were last there.  
        /// </summary>
        /// <param name="USER_ID">For now, only "self" is supported</param>
        /// <param name="BeforeTimeStamp">Seconds since epoch.</param>
        /// <param name="AfterTimeStamp">Seconds after epoch.</param>
        /// <param name="CategoryID">Limits returned venues to those in this category. If specifying a top-level category, all sub-categories will also match the query.</param>
        public Venues getVenueHistory(string USER_ID, string BeforeTimeStamp, string AfterTimeStamp, string CategoryID)
        {
            if (USER_ID.Equals(""))
            {
                USER_ID = "self";
            }

            string Query = "";

            if (!BeforeTimeStamp.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "beforeTimestamp=" + BeforeTimeStamp;
            }

            if (!AfterTimeStamp.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "afterTimestamp=" + AfterTimeStamp;
            }

            if (!CategoryID.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "categoryId=" + CategoryID;
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }


            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/users/" + USER_ID + "/venuehistory" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Venues(JSONDictionary);
        }

        /// <summary>
        /// Sends a friend request to another user.     
        /// </summary>
        /// <param name="USER_ID">required The user ID to which a request will be sent</param>
        public User getRequests(string USER_ID)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();

            Parameters.Add("callback", "XXX");
            Parameters.Add("v", version);
            Parameters.Add("oauth_token", accessToken);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/users/" + USER_ID + "/request"), Parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new User(JSONDictionary);
        }

        /// <summary>
        /// Cancels any relationship between the acting user and the specified user.    
        /// </summary>
        /// <param name="USER_ID">Identity of the user to unfriend.</param>
        public User unfriendUser(string USER_ID)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();

            Parameters.Add("callback", "XXX");
            Parameters.Add("v", version);
            Parameters.Add("oauth_token", accessToken);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/users/" + USER_ID + "/unfriend"), Parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new User(JSONDictionary);
        }

        /// <summary>
        /// Denies a pending friend request from another user.     
        /// </summary>
        /// <param name="USER_ID">required The user ID of a pending friend.</param>
        public User denyUserRequest(string USER_ID)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();

            Parameters.Add("callback", "XXX");
            Parameters.Add("v", version);
            Parameters.Add("oauth_token", accessToken);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/users/" + USER_ID + "/deny"), Parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new User(JSONDictionary);
        }

        /// <summary>
        /// Approves a pending friend request from another user.   
        /// </summary>
        /// <param name="USER_ID">required The user ID of a pending friend.</param>
        public User approveUserRequest(string USER_ID)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();

            Parameters.Add("callback", "XXX");
            Parameters.Add("v", version);
            Parameters.Add("oauth_token", accessToken);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/users/" + USER_ID + "/approve"), Parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new User(JSONDictionary);
        }

        /// <summary>
        /// Changes whether the acting user will receive pings (phone notifications) when the specified user checks in.  
        /// </summary>
        /// <param name="USER_ID">required The user ID of a friend.</param>
        /// <param name="Value">required True or false.</param>
        public User setUserPings(string USER_ID, bool Value)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();

            Parameters.Add("callback", "XXX");
            Parameters.Add("v", version);
            Parameters.Add("oauth_token", accessToken);
            if (Value)
            {
                Parameters.Add("value", "True");
            }
            else
            {
                Parameters.Add("value", "False");
            }
            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/users/" + USER_ID + "/setpings"), Parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new User(JSONDictionary);
        }

        /// <summary>
        /// Updates the user's profile photo.  
        /// </summary>
        /// <param name="photo ">Photo under 100KB in multipart MIME encoding with content type image/jpeg, image/gif, or image/png.</param>
        public User updateUser(string filePath)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            HTTPMultiPartPost POST = new HTTPMultiPartPost("https://api.foursquare.com/v2/users/self/update", parameters, filePath);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new User(JSONDictionary);
        }

        /// <summary>
        /// Updates the user's profile photo.  
        /// </summary>
        /// <param name="photo ">Photo under 100KB in multipart MIME encoding with content type image/jpeg, image/gif, or image/png.</param>
        public User updateUser(string fileName, FileStream fileStream)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();

            Parameters.Add("callback", "XXX");
            Parameters.Add("v", version);
            Parameters.Add("oauth_token", accessToken);

            HTTPMultiPartPost POST = new HTTPMultiPartPost("https://api.foursquare.com/v2/users/self/update", Parameters, fileName, fileStream);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new User(JSONDictionary);
        }

        #endregion Users

        #region Venues

        /// <summary>
        /// Gives details about a venue, including location, mayorship, tags, tips, specials, and category.
        /// </summary>
        /// <param name="VENUE_ID">required ID of venue to retrieve</param>
        public Venue getVenue(string VENUE_ID)
        {
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/venues/" + VENUE_ID + "?callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            Venue Venue = new Venue(JSONDictionary);
            return Venue;
        }

        /// <summary>
        /// Allows users to add a new venue.  
        /// </summary>
        /// <param name="name">required the name of the venue NOTE: One of either a valid address or a geolat/geolong pair must be provided</param>
        /// <param name="address">The address of the venue.</param>
        /// <param name="crossStreet">The nearest intersecting street or streets.</param>
        /// <param name="city">The city name where this venue is.</param>
        /// <param name="state">The nearest state or province to the venue.</param>
        /// <param name="zip">The zip or postal code for the venue.</param>
        /// <param name="phone">The phone number of the venue.</param>
        /// <param name="ll">required Latitude and longitude of the venue, as accurate as is known. NOTE: One of either a valid address or a geolat/geolong pair must be provided</param>
        /// <param name="primaryCategoryId">The ID of the category to which you want to assign this venue.</param>
        public Venue addVenue(string name, string address, string crossStreet, string city, string state, string zip, string phone, string ll, string primaryCategoryId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            #region Parameter Conditioning

            //address
            if (!address.Equals(""))
            {
                parameters.Add("address", address);
            }

            //city
            if (!city.Equals(""))
            {
                parameters.Add("city", city);
            }

            //crossStreet
            if (!crossStreet.Equals(""))
            {
                parameters.Add("crossStreet", crossStreet);
            }

            //ll
            if (!ll.Equals(""))
            {
                parameters.Add("ll", ll);
            }

            //name
            if (!name.Equals(""))
            {
                parameters.Add("name", name);
            }

            //phone
            if (!phone.Equals(""))
            {
                parameters.Add("phone", phone);
            }

            //primaryCategoryId
            if (!primaryCategoryId.Equals(""))
            {
                parameters.Add("primaryCategoryId", primaryCategoryId);
            }

            //state
            if (!state.Equals(""))
            {
                parameters.Add("state", state);
            }

            //zip
            if (!zip.Equals(""))
            {
                parameters.Add("zip", zip);
            }

            #endregion Parameter Conditioning

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/venues/add"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Venue(JSONDictionary);
        }

        /// <summary>
        /// Returns a hierarchical list of categories applied to venues. By default, top-level categories do not have IDs. 
        /// </summary>
        public VenueCategories getVenueCategories()
        {
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/venues/categories" + "?callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new VenueCategories(JSONDictionary);
        }

        /// <summary>
        /// Returns a list of recommended venues near the current location. 
        /// </summary>
        /// <param name="ll">required Latitude and longitude of the user's location, so response can include distance.</param>
        /// <param name="llAcc">Accuracy of latitude and longitude, in meters.</param>
        /// <param name="alt">Altitude of the user's location, in meters.</param>
        /// <param name="altAcc">Accuracy of the user's altitude, in meters.</param>
        /// <param name="radius">Radius to search within, in meters.</param>
        /// <param name="section">One of food, drinks, coffee, shops, or arts.</param>
        /// <param name="query">A search term to be applied against tips, category, tips, etc. at a venue.</param>
        /// <param name="limit">Number of results to return, up to 50.</param>
        /// <param name="basis">If present and set to friends or me, limits results to only places where friends have visited or user has visited, respectively.</param>
        public RecommendedVenues exploreVenue(string ll, string llAcc, string alt, string altAcc, string radius, string section, string query, string limit, string basis)
        {
            HTTPGet GET = new HTTPGet();
            string Query = "";

            #region Parameters

            //ll
            if (!ll.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "ll=" + ll;
            }

            //llAcc
            if (!llAcc.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "llAcc=" + llAcc;
            }

            //alt
            if (!alt.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "alt=" + alt;
            }

            //altAcc
            if (!altAcc.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "altAcc=" + altAcc;
            }

            //radius
            if (!radius.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "radius=" + radius;
            }

            //section
            if (!section.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "section=" + section;
            }

            //query
            if (!query.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "query=" + query;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + limit;
            }

            //basis
            if (!basis.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "basis=" + basis;
            }

            #endregion Parameters

            string EndPoint = "https://api.foursquare.com/v2/venues/explore" + Query + "&callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new RecommendedVenues(JSONDictionary);
        }

        /// <summary>
        /// Returns a list of venues near the current location, optionally matching the search term.  
        /// </summary>
        /// <param name="ll">required Latitude and longitude of the user's location, so response can include distance.</param>
        /// <param name="llAcc">Accuracy of latitude and longitude, in meters.</param>
        /// <param name="alt">Altitude of the user's location, in meters.</param>
        /// <param name="altAcc">Accuracy of the user's altitude, in meters.</param>
        /// <param name="query">A search term to be applied against titles.</param>
        /// <param name="limit">Number of results to return, up to 50.</param>
        /// <param name="intent">Indicates your intent in performing the search. checkin, match, specials</param>
        /// <param name="categoryId">A category to limit results to. </param>
        /// <param name="url">A third-party URL which we will attempt to match against our map of venues to URLs.</param>
        /// <param name="providerId">Identifier for a known third party that is part of our map of venues to URLs, used in conjunction with linkedId</param>
        /// <param name="linkedId">Identifier used by third party specifed in providerId, which we will attempt to match against our map of venues to URLs.</param>
        public Venues findVenues(string ll, string llAcc, string alt, string altAcc, string query, string limit, string intent, string categoryId, string url, string providerId, string linkedId)
        {
            HTTPGet GET = new HTTPGet();
            string Query = "";

            #region Query Conditioning

            //ll
            if (!ll.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "ll=" + ll;
            }

            //llAcc
            if (!llAcc.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "llAcc=" + llAcc;
            }

            //alt
            if (!alt.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "alt=" + alt;
            }

            //altAcc
            if (!altAcc.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "altAcc=" + altAcc;
            }

            //query
            if (!query.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "query=" + query;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + limit;
            }

            //intent
            if (!intent.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "intent=" + intent;
            }

            //categoryId
            if (!categoryId.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "categoryId=" + categoryId;
            }

            //url
            if (!url.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "url=" + url;
            }

            //providerId
            if (!providerId.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "providerId=" + providerId;
            }

            //linkedId
            if (!linkedId.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "linkedId=" + linkedId;
            }

            #endregion Query Conditioning

            string EndPoint = "https://api.foursquare.com/v2/venues/search" + Query + "&callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
           Venues FoundVenues = new Venues(JSONDictionary);
            return FoundVenues;
        }

        /// <summary>
        /// Returns a list of venues near the current location with the most people currently checked in.   
        /// </summary>
        /// <param name="ll">required Latitude and longitude of the user's location.</param>
        /// <param name="limit">Number of results to return, up to 50.</param>
        /// <param name="radius">Radius in meters, up to approximately 2000 meters.</param>
        public Venues getTrendingVenues(string ll, string limit, string radius)
        {
            HTTPGet GET = new HTTPGet();
            string Query = "";

            #region Query Conditioning

            //ll
            if (!ll.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "ll=" + ll;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + limit;
            }

            //radius
            if (!radius.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "radius=" + radius;
            }

            #endregion Query Conditioning

            string EndPoint = "https://api.foursquare.com/v2/venues/trending" + Query + "&callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Venues(JSONDictionary);
        }

        /// <summary>
        /// Provides a count of how many people are at a given venue. If the request is user authenticated, also returns a list of the users there, friends-first.    
        /// </summary>
        /// <param name="VENUE_ID">required ID of venue to retrieve</param>
        /// <param name="limit">Number of results to return, up to 500.</param>
        /// <param name="offset">Used to page through results.</param>
        /// <param name="afterTimestamp">Retrieve the first results to follow these seconds since epoch</param>
        public Checkins getCurrentCheckinsAtVenue(string VENUE_ID, string limit, string offset, string afterTimestamp)
        {
            HTTPGet GET = new HTTPGet();
            string Query = "";

            #region Query Conditioning

            //limit
            if (!limit.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + limit;
            }

            //offset
            if (!offset.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "offset=" + offset;
            }

            //afterTimestamp
            if (!afterTimestamp.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "afterTimestamp=" + afterTimestamp;
            }

            #endregion Query Conditioning

            string EndPoint = "https://api.foursquare.com/v2/venues/" + VENUE_ID + "/herenow" + Query + "&callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Checkins(JSONDictionary);
        }

        /// <summary>
        /// Returns tips for a venue.     
        /// </summary>
        /// <param name="VENUE_ID">required The venue you want tips for.</param>
        /// <param name="sort">One of recent or popular.</param>
        /// <param name="limit">Number of results to return, up to 500</param>
        /// <param name="offset">Used to page through results.</param>
        public Tips getVenueTips(string VENUE_ID, string sort, string limit, string offset)
        {
            HTTPGet GET = new HTTPGet();
            string Query = "";

            #region Query Conditioning

            //sort
            if (!sort.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "sort=" + sort;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + limit;
            }

            //offset
            if (!offset.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "offset=" + offset;
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }

            #endregion Query Conditioning

            string EndPoint = "https://api.foursquare.com/v2/venues/" + VENUE_ID + "/tips" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
           return new Tips(JSONDictionary);
        }

        /// <summary>
        /// Returns photos for a venue    
        /// </summary>
        /// <param name="VENUE_ID">required The venue you want photos for.</param>
        /// <param name="group">required. Pass checkin for photos added by friends on their recent checkins. Pass venue for public photos added to the venue by anyone. Use multi to fetch both.</param>
        /// <param name="limit">Number of results to return, up to 500</param>
        /// <param name="offset">Used to page through results.</param>
        public Photos getVenuePhotos(string VENUE_ID, string group, string limit, string offset)
        {
            HTTPGet GET = new HTTPGet();
            string Query = "";

            #region Query Conditioning

            //group
            if (!group.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "group=" + group;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + limit;
            }

            //offset
            if (!offset.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "offset=" + offset;
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }

            #endregion Query Conditioning

            string EndPoint = "https://api.foursquare.com/v2/venues/" + VENUE_ID + "/photos" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Photos(JSONDictionary);
        }

        /// <summary>
        ///Returns URLs or identifiers from third parties that have been applied to this venue, such as how the New York Times refers to this venue and a URL for additional information from nytimes.com.    
        /// </summary>
        /// <param name="VENUE_ID">required The venue you want annotations for..</param>
        public Links getVenueLinks(string VENUE_ID)
        {
            HTTPGet GET = new HTTPGet();

            string EndPoint = "https://api.foursquare.com/v2/venues/" + VENUE_ID + "/links?callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Links(JSONDictionary);
        }

        /// <summary>
        /// Allows you to mark a venue to-do, with optional text.     
        /// </summary>
        /// <param name="VENUE_ID">required The venue you want to mark to-do.</param>
        /// <param name="text">The text of the tip.</param>
        public ToDo venueMarkTodo(string VENUE_ID, string text)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            #region Parameter Conditioning

            //text
            if (!text.Equals(""))
            {
                parameters.Add("text", text);
            }

            #endregion Parameter Conditioning

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/venues/" + VENUE_ID + "/marktodo"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new ToDo(JSONDictionary);
        }

        /// <summary>
        /// Allows users to indicate a venue is incorrect in some way.      
        /// </summary>
        /// <param name="VENUE_ID">required The venue id for which an edit is being proposed.</param>
        /// <param name="problem">required One of mislocated, closed, duplicate.</param>
        public bool venueFlag(string VENUE_ID, string problem)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            #region Parameter Conditioning

            //problem
            if (!problem.Equals(""))
            {
                parameters.Add("problem", problem);
            }

            #endregion Parameter Conditioning

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/venues/" + VENUE_ID + "/flag"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            if (((Dictionary<string, object>)JSONDictionary["meta"])["code"].ToString().Equals("200"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Allows you to make a change to a venue. Requires Superuser privileges     
        /// </summary>
        /// <param name="VENUE_ID">required The venue id for which an edit is being proposed</param>
        /// <param name="name">The name of the venue.</param>
        /// <param name="address">The address of the venue.</param>
        /// <param name="crossStreet">The nearest intersecting street or streets</param>
        /// <param name="city">The city name where this venue is.</param>
        /// <param name="state">The nearest state or province to the venue.</param>
        /// <param name="zip">The zip or postal code for the venue.</param>
        /// <param name="phone">The phone number of the venue.</param>
        /// <param name="ll">Latitude and longitude of the user's location, as accurate as is known.</param>
        /// <param name="primaryCategoryId">The ID of the category to which you want to assign this venue.</param>
        public bool editVenue(string VENUE_ID, string name, string address, string crossStreet, string city, string state, string zip, string phone, string ll, string primaryCategoryId)
        {
            return VenueEditor("edit", VENUE_ID, name, address, crossStreet, city, state, zip, phone, ll, primaryCategoryId);
        }

        /// <summary>
        /// Allows you to propose a change to a venue.      
        /// </summary>
        /// <param name="VENUE_ID">required The venue id for which an edit is being proposed</param>
        /// <param name="name">The name of the venue.</param>
        /// <param name="address">The address of the venue.</param>
        /// <param name="crossStreet">The nearest intersecting street or streets</param>
        /// <param name="city">The city name where this venue is.</param>
        /// <param name="state">The nearest state or province to the venue.</param>
        /// <param name="zip">The zip or postal code for the venue.</param>
        /// <param name="phone">The phone number of the venue.</param>
        /// <param name="ll">Latitude and longitude of the user's location, as accurate as is known.</param>
        /// <param name="primaryCategoryId">The ID of the category to which you want to assign this venue.</param>
        public bool proposeVenueEdit(string VENUE_ID, string name, string address, string crossStreet, string city, string state, string zip, string phone, string ll, string primaryCategoryId)
        {
            return VenueEditor("proposeedit", VENUE_ID, name, address, crossStreet, city, state, zip, phone, ll, primaryCategoryId);
        }

        /// <summary>
        /// Allows you to propose or make a change to a venue.      
        /// </summary>
        /// <param name="VENUE_ID">required The venue id for which an edit is being proposed</param>
        /// <param name="EditType">either edit or proposeedit</param>
        /// <param name="name">The name of the venue.</param>
        /// <param name="address">The address of the venue.</param>
        /// <param name="crossStreet">The nearest intersecting street or streets</param>
        /// <param name="city">The city name where this venue is.</param>
        /// <param name="state">The nearest state or province to the venue.</param>
        /// <param name="zip">The zip or postal code for the venue.</param>
        /// <param name="phone">The phone number of the venue.</param>
        /// <param name="ll">Latitude and longitude of the user's location, as accurate as is known.</param>
        /// <param name="primaryCategoryId">The ID of the category to which you want to assign this venue.</param>
        private bool VenueEditor(string EditType, string VENUE_ID, string name, string address, string crossStreet, string city, string state, string zip, string phone, string ll, string primaryCategoryId)
        {
            //Venue Edit and Venue ProposeEdit are essentially the same call. Edit requires Superuser privileges

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);

            #region Parameter Conditioning

            //address
            if (!address.Equals(""))
            {
                parameters.Add("address", address);
            }

            //city
            if (!city.Equals(""))
            {
                parameters.Add("city", city);
            }

            //crossStreet
            if (!crossStreet.Equals(""))
            {
                parameters.Add("crossStreet", crossStreet);
            }

            //ll
            if (!ll.Equals(""))
            {
                parameters.Add("ll", ll);
            }

            //name
            if (!name.Equals(""))
            {
                parameters.Add("name", name);
            }


            //phone
            if (!phone.Equals(""))
            {
                parameters.Add("phone", phone);
            }

            //primaryCategoryId
            if (!primaryCategoryId.Equals(""))
            {
                parameters.Add("primaryCategoryId", primaryCategoryId);
            }

            //state
            if (!state.Equals(""))
            {
                parameters.Add("state", state);
            }

            //zip
            if (!zip.Equals(""))
            {
                parameters.Add("zip", zip);
            }


            #endregion Parameter Conditioning


            parameters.Add("oauth_token", accessToken);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/venues/" + VENUE_ID + "/" + EditType), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            if (((Dictionary<string, object>)JSONDictionary["meta"])["code"].ToString().Equals("200"))
            {
                return true;
            }
            return false;
        }

        #endregion Venues

        #region Checkins

        /// <summary>
        /// Retrieves information on a specific checkin.
        /// </summary>
        /// <param name="CHECKIN_ID">The ID of the checkin to retrieve specific information for.</param>
        public Checkin getCheckinDetails(string CHECKIN_ID)
        {
            return getCheckinDetails(CHECKIN_ID, "");
        }

        /// <summary>
        /// Retrieves information on a specific checkin.
        /// </summary>
        /// <param name="CHECKIN_ID">The ID of the checkin to retrieve specific information for.</param>
        /// <param name="signature">When checkins are sent to public feeds such as Twitter, foursquare appends a signature (s=XXXXXX) allowing users to bypass the friends-only access check on checkins. The same value can be used here for programmatic access to otherwise inaccessible checkins. Callers should use the bit.ly API to first expand 4sq.com links.</param>
        public Checkin getCheckinDetails(string CHECKIN_ID, string signature)
        {
            string Query = "";

            if (!signature.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "signature=" + signature;
            }
            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/checkins/" + CHECKIN_ID + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Checkin(JSONDictionary);
        }

        /// <summary>
        /// Allows you to check in to a place.
        /// </summary>
        /// <param name="venueId">The venue where the user is checking in. No venueid is needed if shouting or just providing a venue name.</param>
        /// <param name="Broadcast">Required. How much to broadcast this check-in, ranging from private (off-the-grid) to public,facebook,twitter. Can also be just public or public,facebook, for example. If no valid value is found, the default is public. Shouts cannot be private.</param>
        /// <param name="LL">Latitude and longitude of the user's location. Only specify this field if you have a GPS or other device reported location for the user at the time of check-in.</param>
        public Checkin addCheckin(string venueId, string broadcast, string ll)
        {
            return addCheckin(venueId, "", "", broadcast, ll, "1", "0", "1");
        }

        /// <summary>
        /// Allows you to check in to a place.
        /// </summary>
        /// <param name="venueId">The venue where the user is checking in. No venueid is needed if shouting or just providing a venue name.</param>
        /// <param name="venue">If are not shouting, but you don't have a venue ID or would rather prefer a 'venueless' checkin</param>
        /// <param name="shout">A message about your check-in. The maximum length of this field is 140 characters.</param>
        /// <param name="broadcast">Required. How much to broadcast this check-in, ranging from private (off-the-grid) to public,facebook,twitter. Can also be just public or public,facebook, for example. If no valid value is found, the default is public. Shouts cannot be private.</param>
        /// <param name="ll">Latitude and longitude of the user's location. Only specify this field if you have a GPS or other device reported location for the user at the time of check-in.</param>
        /// <param name="llAcc">Accuracy of the user's latitude and longitude, in meters.</param>
        /// <param name="alt">Altitude of the user's location, in meters.</param>
        /// <param name="altAcc">Vertical accuracy of the user's location, in meters.</param>
        public Checkin addCheckin(string venueId, string venue, string shout, string broadcast, string ll, string llAcc, string alt, string altAcc)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (!alt.Equals(""))
            {
                parameters.Add("alt", alt);
            }
            if (!altAcc.Equals(""))
            {
                parameters.Add("altAcc", altAcc);
            }
            if (!broadcast.Equals(""))
            {
                parameters.Add("broadcast", broadcast);
            }
            if (!ll.Equals(""))
            {
                parameters.Add("ll", ll);
            }
            if (!llAcc.Equals(""))
            {
                parameters.Add("llAcc", llAcc);
            }
            if (!shout.Equals(""))
            {
                parameters.Add("shout", shout);
            }
            if (!venue.Equals(""))
            {
                parameters.Add("venue", venue);
            }
            if (!venueId.Equals(""))
            {
                parameters.Add("venueId", venueId);
            }
            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);


            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/checkins/add"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Checkin(JSONDictionary);
        }

        /// <summary>
        /// Recent checkins by friends 
        /// </summary>
        /// <param name="ll">Latitude and longitude of the user's location, so response can include distance. "44.3,37.2"</param>
        /// <param name="limit">Number of results to return, up to 100.</param>
        /// <param name="afterTimestamp">Seconds after which to look for checkins</param>
        public Checkins getRecentCheckin(string LL, string Limit, string AfterTimestamp)
        {
            string Query = "";

            if (!LL.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "ll=" + LL;
            }

            if (!Limit.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + Limit;
            }

            if (!AfterTimestamp.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "afterTimestamp=" + AfterTimestamp;
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }

            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/checkins/recent" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Checkins(JSONDictionary);
        }

        /// <summary>
        /// Add a comment to a check-in  
        /// </summary>
        /// <param name="CHECKIN_ID">The ID of the checkin to add a comment to.</param>
        /// <param name="text">The text of the comment, up to 200 characters.</param>
        public Comment addComment(string CHECKIN_ID, string Text)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("text", Text);
            parameters.Add("oauth_token", accessToken);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/checkins/" + CHECKIN_ID + "/addcomment"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Comment(JSONDictionary);
        }

        /// <summary>
        /// Remove commment from check-in   
        /// </summary>
        /// <param name="CHECKIN_ID">The ID of the checkin to remove a comment from.</param>
        /// <param name="commentId">The id of the comment to remove.</param>
        public Checkin deleteComment(string CHECKIN_ID, string commentId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("commentId", commentId);
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);


            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/checkins/" + CHECKIN_ID + "/deletecomment"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Checkin(JSONDictionary);
        }

        #endregion Checkins

        #region Tips

        /// <summary>
        /// Gives details about a tip, including which users (especially friends) have marked the tip to-do or done.    
        /// </summary>
        /// <param name="TIP_ID">required ID of tip to retrieve</param>
        public Tip getTip(string TIP_ID, string accessToken)
        {
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/tips/" + TIP_ID + "?callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Tip(JSONDictionary);
        }

        /// <summary>
        /// Allows you to add a new tip at a venue.     
        /// </summary>
        /// <param name="venueId">required The venue where you want to add this tip.</param>
        /// <param name="text">required The text of the tip.</param>
        /// <param name="url">A URL related to this tip.</param>
        public Tip addTip(string venueId, string text, string url)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("text", text);
            parameters.Add("url", url);
            parameters.Add("v", version);
            parameters.Add("venueId", venueId);
            parameters.Add("oauth_token", accessToken);


            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/tips/add"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Tip(JSONDictionary);
        }

        /// <summary>
        /// Returns a list of tips near the area specified.  
        /// </summary>
        /// <param name="ll">required Latitude and longitude of the user's location.</param>
        /// <param name="limit">optional Number of results to return, up to 500.</param>
        /// <param name="offset">optional Used to page through results.</param>
        /// <param name="filter">If set to friends, only show nearby tips from friends.</param>
        /// <param name="query">Only find tips matching the given term, cannot be used in conjunction with friends filter.</param>
        public Tips findTip(string ll, string limit, string offset, string filter, string query)
        {
            #region QueryConditioning

            string Query = "";


            if (!ll.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "ll=" + ll;
            }

            if (!limit.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + limit;
            }

            if (!offset.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "offset=" + offset;
            }

            if (!filter.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "filter=" + filter;
            }

            if (!query.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "query=" + query;
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }

            #endregion QueryConditioning

            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/tips/search" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Tips(JSONDictionary);
        }

        /// <summary>
        /// Allows you to mark a tip to-do.   
        /// </summary>
        /// <param name="TIP_ID">required The tip you want to mark to-do.</param>
        public ToDo markTipAsToDo(string TIP_ID)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/tips/" + TIP_ID + "/marktodo"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new ToDo(JSONDictionary);
        }

        /// <summary>
        /// Allows the acting user to mark a tip done.   
        /// </summary>
        /// <param name="TIP_ID">required The tip you want to mark done.</param>
        public Tip markTipAsDone(string TIP_ID)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/tips/" + TIP_ID + "/markdone"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Tip(JSONDictionary);
        }

        /// <summary>
        /// Allows you to remove a tip from your to-do list or done list.    
        /// </summary>
        /// <param name="TIP_ID">required The tip you want to unmark.</param>
        public Tip unmarkTip(string TIP_ID)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/tips/" + TIP_ID + "/unmark"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Tip(JSONDictionary);
        }

        #endregion Tips

        #region Photos

        /// <summary>
        /// Get details of a photo. 
        /// </summary>
        /// <param name="PHOTO_ID">The ID of the photo to retrieve additional information for.</param>
        public Photo getPhoto(string PHOTO_ID)
        {
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/photos/" + PHOTO_ID + "?callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Photo(JSONDictionary);
        }

        /// <summary>
        /// Allows users to add a new photo to a checkin, tip, or a venue in general.
        /// All fields are optional, but exactly one of the id fields (checkinId, tipId, venueId) must be passed in. 
        /// </summary>
        /// <param name="checkinId">the ID of a checkin owned by the user</param>
        /// <param name="tipId">the ID of a tip owned by the user</param>
        /// <param name="venueId">the ID of a venue, provided only when adding a public photo of the venue in general, rather than a private checkin or tip photo using the parameters above</param>
        /// <param name="FilePath">The full path to the photo. Should be an image/jpeg</param>
        /// <param name="broadcast">Whether to broadcast this photo, ranging from twitter if you want to send to twitter, facebook if you want to send to facebook, or twitter,facebook if you want to send to both.</param>
        /// <param name="ll">Latitude and longitude of the user's location.</param>
        /// <param name="llAcc">Accuracy of the user's latitude and longitude, in meters.</param>
        /// <param name="alt">Altitude of the user's location, in meters.</param>
        /// <param name="altAcc">Vertical accuracy of the user's location, in meters.</param>
        public Photo addPhoto(string checkinId, string tipId, string venueId, string FilePath, string broadcast, string ll, string llAcc, string alt, string altAcc)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            #region Parameter Conditioning

            //Only one ID. Use the first one found.

            if (!checkinId.Equals(""))
            {
                parameters.Add("checkinId", checkinId);
            }
            else
            {
                if (!tipId.Equals(""))
                {
                    parameters.Add("tipId", tipId);
                }
                else
                {
                    parameters.Add("venueId", venueId);
                }
            }

            if (!broadcast.Equals(""))
            {
                parameters.Add("broadcast", broadcast);
            }

            if (!ll.Equals(""))
            {
                parameters.Add("ll", ll);
            }

            if (!llAcc.Equals(""))
            {
                parameters.Add("llAcc", llAcc);
            }

            if (!alt.Equals(""))
            {
                parameters.Add("alt", alt);
            }

            if (!altAcc.Equals(""))
            {
                parameters.Add("altAcc", altAcc);
            }

            #endregion Parameter Conditioning

            HTTPMultiPartPost POST = new HTTPMultiPartPost("https://api.foursquare.com/v2/photos/add", parameters, FilePath);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Photo(JSONDictionary);
        }

        /// <summary>
        /// Allows users to add a new photo to a checkin, tip, or a venue in general.
        /// All fields are optional, but exactly one of the id fields (checkinId, tipId, venueId) must be passed in. 
        /// </summary>
        /// <param name="checkinId">the ID of a checkin owned by the user</param>
        /// <param name="tipId">the ID of a tip owned by the user</param>
        /// <param name="venueId">the ID of a venue, provided only when adding a public photo of the venue in general, rather than a private checkin or tip photo using the parameters above</param>
        /// <param name="FileName">The name of the file</param>
        /// <param name="fileStream">The FileStream to the photo. Should be an image/jpeg</param>
        /// <param name="broadcast">Whether to broadcast this photo, ranging from twitter if you want to send to twitter, facebook if you want to send to facebook, or twitter,facebook if you want to send to both.</param>
        /// <param name="ll">Latitude and longitude of the user's location.</param>
        /// <param name="llAcc">Accuracy of the user's latitude and longitude, in meters.</param>
        /// <param name="alt">Altitude of the user's location, in meters.</param>
        /// <param name="altAcc">Vertical accuracy of the user's location, in meters.</param>
        public Photo PhotoAdd(string checkinId, string tipId, string venueId, string FileName, FileStream fileStream, string broadcast, string ll, string llAcc, string alt, string altAcc)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();

            Parameters.Add("callback", "XXX");
            Parameters.Add("v", version);
            Parameters.Add("oauth_token", accessToken);


            #region Parameter Conditioning

            //Only one ID. Use the first one found.

            if (!checkinId.Equals(""))
            {
                Parameters.Add("checkinId", checkinId);
            }
            else
            {
                if (!tipId.Equals(""))
                {
                    Parameters.Add("tipId", tipId);
                }
                else
                {
                    Parameters.Add("venueId", venueId);
                }
            }

            if (!broadcast.Equals(""))
            {
                Parameters.Add("broadcast", broadcast);
            }

            if (!ll.Equals(""))
            {
                Parameters.Add("ll", ll);
            }

            if (!llAcc.Equals(""))
            {
                Parameters.Add("llAcc", llAcc);
            }

            if (!alt.Equals(""))
            {
                Parameters.Add("alt", alt);
            }

            if (!altAcc.Equals(""))
            {
                Parameters.Add("altAcc", altAcc);
            }

            #endregion Parameter Conditioning

            HTTPMultiPartPost POST = new HTTPMultiPartPost("https://api.foursquare.com/v2/photos/add", Parameters, FileName, fileStream);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Photo(JSONDictionary);
        }

        #endregion Photos

        #region Settings

        /// <summary>
        /// Returns a setting for the acting user.   
        /// </summary>
        /// <param name="Setting">The name of a setting</param>
        public Settings getSettings()
        {
            Dictionary<string, Object> settings = new Dictionary<string, Object>();

            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/settings/all?callback=XXX&v=" + version + "&callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Settings(JSONDictionary);
        }

        /// <summary>
        /// Change a setting for the given user.    
        /// </summary>
        /// <param name="Setting">The name of a setting</param>
        /// <param name="value">True or False</param>
        public Settings modifySettings(string Setting, bool Value)
        {
            Dictionary<string, Object> SettingDictionary = new Dictionary<string, Object>();

            string StrValue = "0";
            if (Value)
            {
                StrValue = "1";
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            parameters.Add("value", StrValue);

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/settings/" + Setting + "/set"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            return new Settings(JSONDictionary);
        }

        #endregion Settings

        #region Specials

        /// <summary>
        /// Gives details about a special, including text and whether it is unlocked for the current user. 
        /// </summary>
        /// <param name="SPECIAL_ID">required ID of special to retrieve</param>
        /// <param name="venueId">required ID of a venue the special is running at</param>
        public Special getSpecial(string SPECIAL_ID, string venueId)
        {
            HTTPGet GET = new HTTPGet();
            string EndPoint = "https://api.foursquare.com/v2/specials/" + SPECIAL_ID + "?venueId=" + venueId + "&callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);

            return new Special((Dictionary<string, object>)JSONDictionary);
        }


        /// <summary>
        /// Returns a list of specials near the current location.  
        /// </summary>
        /// <param name="ll">Required. Latitude and longitude to search near.</param>
        /// <param name="llAcc">Accuracy of latitude and longitude, in meters.</param>
        /// <param name="alt">Altitude of the user's location, in meters.</param>
        /// <param name="altAcc">Accuracy of the user's altitude, in meters.</param>
        /// <param name="limit">Number of results to return, up to 50.</param>
        public Specials findSpecialsNearby(string ll, string llAcc, string alt, string altAcc, string limit)
        {
            HTTPGet GET = new HTTPGet();

            #region Query Conditioning

            string Query = "";

            //ll
            if (!ll.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "ll=" + ll;
            }

            //llAcc
            if (!llAcc.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "llAcc=" + llAcc;
            }

            //alt
            if (!alt.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "alt=" + alt;
            }

            //altAcc
            if (!altAcc.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "altAcc=" + altAcc;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (Query.Equals(""))
                {
                    Query = "?";
                }
                else
                {
                    Query += "&";
                }
                Query += "limit=" + limit;
            }

            if (Query.Equals(""))
            {
                Query = "?";
            }
            else
            {
                Query += "&";
            }

            #endregion Query Conditioning

            string EndPoint = "https://api.foursquare.com/v2/specials/search" + Query + "callback=XXX&v=" + version + "&oauth_token=" + accessToken;
            GET.Request(EndPoint);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(GET.ResponseBody);
            return new Specials(JSONDictionary);
        }

        /// <summary>
        /// Allows users to indicate a Special is improper in some way.     
        /// </summary>
        /// <param name="ID">required The id of the special being flagged</param>
        /// <param name="venueId">required The id of the venue running the special.</param>
        /// <param name="problem">required One of not_redeemable, not_valuable, other</param>
        /// <param name="text">Additional text about why the user has flagged this special</param>
        public bool flagSpecial(string ID, string venueId, string problem, string text)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("callback", "XXX");
            parameters.Add("v", version);
            parameters.Add("oauth_token", accessToken);

            parameters.Add("venueId", venueId);
            parameters.Add("problem", problem);

            if (!text.Equals(""))
            {
                parameters.Add("text", text);
            }

            HTTPPost POST = new HTTPPost(new Uri("https://api.foursquare.com/v2/specials/" + ID + "/flag"), parameters);
            Dictionary<string, object> JSONDictionary = Helpers.JSONDeserializer(POST.responseBody);
            if (((Dictionary<string, object>)JSONDictionary["meta"])["code"].ToString().Equals("200"))
            {
                return true;
            }
            return false;
        }

        #endregion Specials
    }
}