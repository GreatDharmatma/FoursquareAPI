using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Brahmastra.FoursquareApi.Entities;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi
{
    internal class FoursquareApiClass
    {
        private const string Version = "20110210";
        private readonly string _accessToken;

        public FoursquareApiClass(string accessToken)
        {
            _accessToken = accessToken;
        }

        #region Users

        /// <summary>
        /// Returns profile information for a given user, including selected badges and mayorships. 
        /// </summary>
        /// <param name="userId">Identity of the user to get details for. Pass self to get details of the acting user</param>
        public User GetUser(string userId)
        {
            if (userId.Equals(""))
                userId = "self";
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/users/" + userId + "?callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new User(jsonDictionary);
        }

        /// <summary>
        /// Returns the user's leaderboard.  
        /// </summary>
        public Leaderboard GetLeaderboard()
        {
            return GetLeaderboard(2);
        }

        /// <summary>
        /// Returns the user's leaderboard.  
        /// </summary>
        public Leaderboard GetLeaderboard(int neighbors)
        {
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/users/leaderboard?neighbors=" +
                              neighbors.ToString(CultureInfo.InvariantCulture) + "&callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Leaderboard(jsonDictionary);
        }

        /// <summary>
        /// Helps a user locate friends.   
        /// </summary>
        /// <param name="phone">A comma-delimited list of phone numbers to look for.</param>
        /// <param name="email">A comma-delimited list of email addresses to look for.</param>
        /// <param name="twitter">A comma-delimited list of Twitter handles to look for.</param>
        /// <param name="twitterSource">A single Twitter handle. Results will be friends of this user who use Foursquare.</param>
        /// <param name="fbid">A comma-delimited list of Facebook ID's to look for.</param>
        /// <param name="name">A single string to search for in users' names</param>
        public Users SearchUser(string phone, string email, string twitter, string twitterSource, string fbid,
                                string name)
        {
            var get = new HttpGet();
            string query = "";

            //Phone
            if (!phone.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "phone=" + phone;
            }

            //Email
            if (!email.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "email=" + email;
            }

            //Twitter
            if (!twitter.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "twitter=" + twitter;
            }

            //TwitterSource
            if (!twitterSource.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "twitterSource=" + twitterSource;
            }

            //Fbid
            if (!fbid.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "fbid=" + fbid;
            }

            //Name
            if (!name.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "name=" + name;
            }

            string endPoint = "https://api.foursquare.com/v2/users/search" + query + "&callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Users(jsonDictionary);
        }

        /// <summary>
        /// Shows a user the list of users with whom they have a pending friend request    
        /// </summary>
        public Users GetUserRequests()
        {
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/users/requests" + "?callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Users(jsonDictionary);
        }

        /// <summary>
        /// Returns badges for a given user.    
        /// </summary>
        /// <param name="userId">ID for user to view badges for..</param>
        public BadgeSets GetUserBadges(string userId)
        {
            if (userId.Equals(""))
                userId = "self";
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/users/" + userId + "/badges?callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new BadgeSets(jsonDictionary);
        }

        /// <summary>
        /// Returns a history of checkins for the authenticated user. 
        /// </summary>
        /// <param name="userId">For now, only "self" is supported</param>
        public Checkins GetUserCheckins(string userId)
        {
            return GetUserCheckins(userId, 100, 0, 0, 0);
        }

        /// <summary>
        /// Returns a history of checkins for the authenticated user. 
        /// </summary>
        /// <param name="userId">For now, only "self" is supported</param>
        /// <param name="limit">For now, only "self" is supported</param>
        /// <param name="offset">Used to page through results.</param>
        /// <param name="afterTimestamp">Retrieve the first results to follow these seconds since epoch.</param>
        /// <param name="beforeTimeStamp">Retrieve the first results prior to these seconds since epoch</param>
        public Checkins GetUserCheckins(string userId, int limit, int offset, double afterTimestamp,
                                        double beforeTimeStamp)
        {
            var get = new HttpGet();
            string query = "";

            if (userId.Equals(""))
                userId = "self";

            if (limit > 0)
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "limit=" + limit.ToString(CultureInfo.InvariantCulture);
            }

            if (offset > 0)
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "offset=" + offset.ToString(CultureInfo.InvariantCulture);
            }

            if (afterTimestamp > 0)
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "afterTimestamp=" + afterTimestamp.ToString(CultureInfo.InvariantCulture);
            }

            if (beforeTimeStamp > 0)
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "beforeTimeStamp=" + beforeTimeStamp.ToString(CultureInfo.InvariantCulture);
            }

            if (query.Equals(""))
                query = "?";
            else
                query += "&";
            string endPoint = "https://api.foursquare.com/v2/users/" + userId + "/checkins" + query + "callback=XXX&v=" +
                              Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Checkins(jsonDictionary);
        }

        /// <summary>
        /// Returns an array of a user's friends. 
        /// </summary>
        /// <param name="userId">Identity of the user to get friends of. Pass self to get friends of the acting user</param>
        public Users GetFriends(string userId)
        {
            return GetFriends(userId, 0, 0);
        }

        /// <summary>
        /// Returns an array of a user's friends. 
        /// </summary>
        /// <param name="userId">Identity of the user to get friends of. Pass self to get friends of the acting user</param>
        /// <param name="limit">Number of results to return, up to 500.</param>
        /// <param name="offset">Used to page through results</param>
        public Users GetFriends(string userId, int limit, int offset)
        {
            string query = "";
            if (userId.Equals(""))
                userId = "self";

            if (limit > 0)
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "limit=" + limit.ToString(CultureInfo.InvariantCulture);
            }

            if (offset > 0)
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "offset=" + offset.ToString(CultureInfo.InvariantCulture);
            }

            if (query.Equals(""))
                query = "?";
            else
                query += "&";

            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/users/" + userId + "/friends" + query + "callback=XXX&v=" +
                              Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Users(jsonDictionary);
        }

        /// <summary>
        /// Returns a user's mayorships 
        /// </summary>
        /// <param name="userId">Identity of the user to get mayorships for. Pass self to get friends of the acting user.</param>
        public Mayorships GetMayorships(string userId)
        {
            if (userId.Equals(""))
                userId = "self";

            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/users/" + userId + "/mayorships" + "?callback=XXX&v=" +
                              Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Mayorships(jsonDictionary);
        }

        /// <summary>
        /// Returns tips from a user.  
        /// </summary>
        /// <param name="userId">Identity of the user to get tips from. Pass self to get tips of the acting user.</param>
        /// <param name="sort">One of recent, nearby, or popular. Nearby requires geolat and geolong to be provided.</param>
        /// <param name="ll">Latitude and longitude of the user's location. (Comma separated)</param>
        /// <param name="limit">Number of results to return, up to 500.</param>
        /// <param name="offset">Used to page through results</param>
        public Tips GetTips(string userId, string sort, string ll, int limit, int offset)
        {
            if (userId.Equals(""))
                userId = "self";

            string query = "";

            if (!sort.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "sort=" + sort;
            }

            if (!ll.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "ll=" + ll;
            }

            if (limit > 0)
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "limit=" + limit.ToString(CultureInfo.InvariantCulture);
            }

            if (offset > 0)
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "offset=" + offset.ToString(CultureInfo.InvariantCulture);
            }

            if (query.Equals(""))
                query = "?";
            else
                query += "&";


            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/users/" + userId + "/tips" + query + "callback=XXX&v=" +
                              Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Tips(jsonDictionary);
        }

        /// <summary>
        /// Returns todos from a user. 
        /// </summary>
        /// <param name="userId">Identity of the user to get todos for. Pass self to get todos of the acting user.</param>
        /// <param name="sort">One of recent, nearby, or popular. Nearby requires geolat and geolong to be provided.</param>
        /// <param name="ll">Latitude and longitude of the user's location (Comma separated)</param>
        public ToDos GetTodos(string userId, string sort, string ll)
        {
            if (userId.Equals(""))
                userId = "self";

            string query = "";

            if (!sort.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "sort=" + sort;
            }

            if (!ll.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "ll=" + ll;
            }

            if (query.Equals(""))
                query = "?";
            else
                query += "&";


            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/users/" + userId + "/todos" + query + "callback=XXX&v=" +
                              Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new ToDos(jsonDictionary);
        }

        /// <summary>
        /// Returns a list of all venues visited by the specified user, along with how many visits and when they were last there.  
        /// </summary>
        /// <param name="userId">For now, only "self" is supported</param>
        /// <param name="beforeTimeStamp">Seconds since epoch.</param>
        /// <param name="afterTimeStamp">Seconds after epoch.</param>
        /// <param name="categoryID">Limits returned venues to those in this category. If specifying a top-level category, all sub-categories will also match the query.</param>
        public Venues GetVenueHistory(string userId, string beforeTimeStamp, string afterTimeStamp, string categoryID)
        {
            if (userId.Equals(""))
                userId = "self";

            string query = "";

            if (!beforeTimeStamp.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "beforeTimestamp=" + beforeTimeStamp;
            }

            if (!afterTimeStamp.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "afterTimestamp=" + afterTimeStamp;
            }

            if (!categoryID.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "categoryId=" + categoryID;
            }

            if (query.Equals(""))
                query = "?";
            else
                query += "&";


            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/users/" + userId + "/venuehistory" + query +
                              "callback=XXX&v=" + Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Venues(jsonDictionary);
        }

        /// <summary>
        /// Sends a friend request to another user.     
        /// </summary>
        /// <param name="userID">required The user ID to which a request will be sent</param>
        public User GetRequests(string userID)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/users/" + userID + "/request"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new User(jsonDictionary);
        }

        /// <summary>
        /// Cancels any relationship between the acting user and the specified user.    
        /// </summary>
        /// <param name="userID">Identity of the user to unfriend.</param>
        public User UnfriendUser(string userID)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/users/" + userID + "/unfriend"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new User(jsonDictionary);
        }

        /// <summary>
        /// Denies a pending friend request from another user.     
        /// </summary>
        /// <param name="userID">required The user ID of a pending friend.</param>
        public User DenyUserRequest(string userID)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/users/" + userID + "/deny"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new User(jsonDictionary);
        }

        /// <summary>
        /// Approves a pending friend request from another user.   
        /// </summary>
        /// <param name="userID">required The user ID of a pending friend.</param>
        public User ApproveUserRequest(string userID)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/users/" + userID + "/approve"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new User(jsonDictionary);
        }

        /// <summary>
        /// Changes whether the acting user will receive pings (phone notifications) when the specified user checks in.  
        /// </summary>
        /// <param name="userID">required The user ID of a friend.</param>
        /// <param name="value">required True or false.</param>
        public User SetUserPings(string userID, bool value)
        {
            var parameters = new Dictionary<string, string>
                                 {
                                     {"callback", "XXX"},
                                     {"v", Version},
                                     {"oauth_token", _accessToken},
                                     {"value", value ? "True" : "False"}
                                 };

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/users/" + userID + "/setpings"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new User(jsonDictionary);
        }

        /// <summary>
        /// Updates the user's profile photo.  
        /// </summary>
        /// <param>Photo under 100KB in multipart MIME encoding with content type image/jpeg, image/gif, or image/png. <name>photo</name> </param>
        /// <param name="filePath"> </param>
        public User UpdateUser(string filePath)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            var post = new HttpMultiPartPost("https://api.foursquare.com/v2/users/self/update", parameters, filePath);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new User(jsonDictionary);
        }

        /// <summary>
        /// Updates the user's profile photo.  
        /// </summary>
        /// <param>Photo under 100KB in multipart MIME encoding with content type image/jpeg, image/gif, or image/png. <name>photo</name> </param>
        /// <param name="fileName"> </param>
        /// <param name="fileStream"> </param>
        public User UpdateUser(string fileName, FileStream fileStream)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            var post = new HttpMultiPartPost("https://api.foursquare.com/v2/users/self/update", parameters, fileName,
                                             fileStream);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new User(jsonDictionary);
        }

        #endregion Users

        #region Venues

        /// <summary>
        /// Gives details about a venue, including location, mayorship, tags, tips, specials, and category.
        /// </summary>
        /// <param name="venueID">required ID of venue to retrieve</param>
        public Venue GetVenue(string venueID)
        {
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/venues/" + venueID + "?callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Venue(jsonDictionary);
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
        public Venue AddVenue(string name, string address, string crossStreet, string city, string state, string zip,
                              string phone, string ll, string primaryCategoryId)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            #region Parameter Conditioning

            //address
            if (!address.Equals(""))
                parameters.Add("address", address);

            //city
            if (!city.Equals(""))
                parameters.Add("city", city);

            //crossStreet
            if (!crossStreet.Equals(""))
                parameters.Add("crossStreet", crossStreet);

            //ll
            if (!ll.Equals(""))
                parameters.Add("ll", ll);

            //name
            if (!name.Equals(""))
                parameters.Add("name", name);

            //phone
            if (!phone.Equals(""))
                parameters.Add("phone", phone);

            //primaryCategoryId
            if (!primaryCategoryId.Equals(""))
                parameters.Add("primaryCategoryId", primaryCategoryId);

            //state
            if (!state.Equals(""))
                parameters.Add("state", state);

            //zip
            if (!zip.Equals(""))
                parameters.Add("zip", zip);

            #endregion Parameter Conditioning

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/venues/add"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Venue(jsonDictionary);
        }

        /// <summary>
        /// Returns a hierarchical list of categories applied to venues. By default, top-level categories do not have IDs. 
        /// </summary>
        public VenueCategories GetVenueCategories()
        {
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/venues/categories" + "?callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new VenueCategories(jsonDictionary);
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
        public RecommendedVenues ExploreVenue(string ll, string llAcc, string alt, string altAcc, string radius,
                                              string section, string query, string limit, string basis)
        {
            var get = new HttpGet();
            string localQuery = "";

            #region Parameters

            //ll
            if (!ll.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "ll=" + ll;
            }

            //llAcc
            if (!llAcc.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "llAcc=" + llAcc;
            }

            //alt
            if (!alt.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "alt=" + alt;
            }

            //altAcc
            if (!altAcc.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "altAcc=" + altAcc;
            }

            //radius
            if (!radius.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "radius=" + radius;
            }

            //section
            if (!section.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "section=" + section;
            }

            //query
            if (!localQuery.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "query=" + localQuery;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "limit=" + limit;
            }

            //basis
            if (!basis.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "basis=" + basis;
            }

            #endregion Parameters

            string endPoint = "https://api.foursquare.com/v2/venues/explore" + localQuery + "&callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new RecommendedVenues(jsonDictionary);
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
        public Venues FindVenues(string ll, string llAcc, string alt, string altAcc, string query, string limit,
                                 string intent, string categoryId, string url, string providerId, string linkedId)
        {
            var get = new HttpGet();
            string localQuery = "";

            #region Query Conditioning

            //ll
            if (!ll.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "ll=" + ll;
            }

            //llAcc
            if (!llAcc.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "llAcc=" + llAcc;
            }

            //alt
            if (!alt.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "alt=" + alt;
            }

            //altAcc
            if (!altAcc.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "altAcc=" + altAcc;
            }

            //query
            if (!query.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "query=" + query;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "limit=" + limit;
            }

            //intent
            if (!intent.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "intent=" + intent;
            }

            //categoryId
            if (!categoryId.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "categoryId=" + categoryId;
            }

            //url
            if (!url.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "url=" + url;
            }

            //providerId
            if (!providerId.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "providerId=" + providerId;
            }

            //linkedId
            if (!linkedId.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "linkedId=" + linkedId;
            }

            #endregion Query Conditioning

            string endPoint = "https://api.foursquare.com/v2/venues/search" + localQuery + "&callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Venues(jsonDictionary);
        }

        /// <summary>
        /// Returns a list of venues near the current location with the most people currently checked in.   
        /// </summary>
        /// <param name="ll">required Latitude and longitude of the user's location.</param>
        /// <param name="limit">Number of results to return, up to 50.</param>
        /// <param name="radius">Radius in meters, up to approximately 2000 meters.</param>
        public Venues GetTrendingVenues(string ll, string limit, string radius)
        {
            var get = new HttpGet();
            string query = "";

            #region Query Conditioning

            //ll
            if (!ll.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "ll=" + ll;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "limit=" + limit;
            }

            //radius
            if (!radius.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "radius=" + radius;
            }

            #endregion Query Conditioning

            string endPoint = "https://api.foursquare.com/v2/venues/trending" + query + "&callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Venues(jsonDictionary);
        }

        /// <summary>
        /// Provides a count of how many people are at a given venue. If the request is user authenticated, also returns a list of the users there, friends-first.    
        /// </summary>
        /// <param name="venueID">required ID of venue to retrieve</param>
        /// <param name="limit">Number of results to return, up to 500.</param>
        /// <param name="offset">Used to page through results.</param>
        /// <param name="afterTimestamp">Retrieve the first results to follow these seconds since epoch</param>
        public Checkins GetCurrentCheckinsAtVenue(string venueID, string limit, string offset, string afterTimestamp)
        {
            var get = new HttpGet();
            string query = "";

            #region Query Conditioning

            //limit
            if (!limit.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "limit=" + limit;
            }

            //offset
            if (!offset.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "offset=" + offset;
            }

            //afterTimestamp
            if (!afterTimestamp.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "afterTimestamp=" + afterTimestamp;
            }

            #endregion Query Conditioning

            string endPoint = "https://api.foursquare.com/v2/venues/" + venueID + "/herenow" + query +
                              "&callback=XXX&v=" + Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Checkins(jsonDictionary);
        }

        /// <summary>
        /// Returns tips for a venue.     
        /// </summary>
        /// <param name="venueID">required The venue you want tips for.</param>
        /// <param name="sort">One of recent or popular.</param>
        /// <param name="limit">Number of results to return, up to 500</param>
        /// <param name="offset">Used to page through results.</param>
        public Tips GetVenueTips(string venueID, string sort, string limit, string offset)
        {
            var get = new HttpGet();
            string query = "";

            #region Query Conditioning

            //sort
            if (!sort.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "sort=" + sort;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "limit=" + limit;
            }

            //offset
            if (!offset.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "offset=" + offset;
            }

            if (query.Equals(""))
                query = "?";
            else
                query += "&";

            #endregion Query Conditioning

            string endPoint = "https://api.foursquare.com/v2/venues/" + venueID + "/tips" + query + "callback=XXX&v=" +
                              Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Tips(jsonDictionary);
        }

        /// <summary>
        /// Returns photos for a venue    
        /// </summary>
        /// <param name="venueID">required The venue you want photos for.</param>
        /// <param name="group">required. Pass checkin for photos added by friends on their recent checkins. Pass venue for public photos added to the venue by anyone. Use multi to fetch both.</param>
        /// <param name="limit">Number of results to return, up to 500</param>
        /// <param name="offset">Used to page through results.</param>
        public Photos GetVenuePhotos(string venueID, string group, string limit, string offset)
        {
            var get = new HttpGet();
            string query = "";

            #region Query Conditioning

            //group
            if (!group.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "group=" + group;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "limit=" + limit;
            }

            //offset
            if (!offset.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "offset=" + offset;
            }

            if (query.Equals(""))
                query = "?";
            else
                query += "&";

            #endregion Query Conditioning

            string endPoint = "https://api.foursquare.com/v2/venues/" + venueID + "/photos" + query + "callback=XXX&v=" +
                              Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Photos(jsonDictionary);
        }

        /// <summary>
        ///Returns URLs or identifiers from third parties that have been applied to this venue, such as how the New York Times refers to this venue and a URL for additional information from nytimes.com.    
        /// </summary>
        /// <param name="venueID">required The venue you want annotations for..</param>
        public Links GetVenueLinks(string venueID)
        {
            var get = new HttpGet();

            string endPoint = "https://api.foursquare.com/v2/venues/" + venueID + "/links?callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Links(jsonDictionary);
        }

        /// <summary>
        /// Allows you to mark a venue to-do, with optional text.     
        /// </summary>
        /// <param name="venueID">required The venue you want to mark to-do.</param>
        /// <param name="text">The text of the tip.</param>
        public ToDo VenueMarkTodo(string venueID, string text)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            #region Parameter Conditioning

            //text
            if (!text.Equals(""))
                parameters.Add("text", text);

            #endregion Parameter Conditioning

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/venues/" + venueID + "/marktodo"),
                                    parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new ToDo(jsonDictionary);
        }

        /// <summary>
        /// Allows users to indicate a venue is incorrect in some way.      
        /// </summary>
        /// <param name="venueID">required The venue id for which an edit is being proposed.</param>
        /// <param name="problem">required One of mislocated, closed, duplicate.</param>
        public bool VenueFlag(string venueID, string problem)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            #region Parameter Conditioning

            //problem
            if (!problem.Equals(""))
                parameters.Add("problem", problem);

            #endregion Parameter Conditioning

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/venues/" + venueID + "/flag"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return ((Dictionary<string, object>) jsonDictionary["meta"])["code"].ToString().Equals("200");
        }

        /// <summary>
        /// Allows you to make a change to a venue. Requires Superuser privileges     
        /// </summary>
        /// <param name="venueID">required The venue id for which an edit is being proposed</param>
        /// <param name="name">The name of the venue.</param>
        /// <param name="address">The address of the venue.</param>
        /// <param name="crossStreet">The nearest intersecting street or streets</param>
        /// <param name="city">The city name where this venue is.</param>
        /// <param name="state">The nearest state or province to the venue.</param>
        /// <param name="zip">The zip or postal code for the venue.</param>
        /// <param name="phone">The phone number of the venue.</param>
        /// <param name="ll">Latitude and longitude of the user's location, as accurate as is known.</param>
        /// <param name="primaryCategoryId">The ID of the category to which you want to assign this venue.</param>
        public bool EditVenue(string venueID, string name, string address, string crossStreet, string city,
                              string state, string zip, string phone, string ll, string primaryCategoryId)
        {
            return VenueEditor("edit", venueID, name, address, crossStreet, city, state, zip, phone, ll,
                               primaryCategoryId);
        }

        /// <summary>
        /// Allows you to propose a change to a venue.      
        /// </summary>
        /// <param name="venueID">required The venue id for which an edit is being proposed</param>
        /// <param name="name">The name of the venue.</param>
        /// <param name="address">The address of the venue.</param>
        /// <param name="crossStreet">The nearest intersecting street or streets</param>
        /// <param name="city">The city name where this venue is.</param>
        /// <param name="state">The nearest state or province to the venue.</param>
        /// <param name="zip">The zip or postal code for the venue.</param>
        /// <param name="phone">The phone number of the venue.</param>
        /// <param name="ll">Latitude and longitude of the user's location, as accurate as is known.</param>
        /// <param name="primaryCategoryId">The ID of the category to which you want to assign this venue.</param>
        public bool ProposeVenueEdit(string venueID, string name, string address, string crossStreet, string city,
                                     string state, string zip, string phone, string ll, string primaryCategoryId)
        {
            return VenueEditor("proposeedit", venueID, name, address, crossStreet, city, state, zip, phone, ll,
                               primaryCategoryId);
        }

        /// <summary>
        /// Allows you to propose or make a change to a venue.      
        /// </summary>
        /// <param name="venueID">required The venue id for which an edit is being proposed</param>
        /// <param name="editType">either edit or proposeedit</param>
        /// <param name="name">The name of the venue.</param>
        /// <param name="address">The address of the venue.</param>
        /// <param name="crossStreet">The nearest intersecting street or streets</param>
        /// <param name="city">The city name where this venue is.</param>
        /// <param name="state">The nearest state or province to the venue.</param>
        /// <param name="zip">The zip or postal code for the venue.</param>
        /// <param name="phone">The phone number of the venue.</param>
        /// <param name="ll">Latitude and longitude of the user's location, as accurate as is known.</param>
        /// <param name="primaryCategoryId">The ID of the category to which you want to assign this venue.</param>
        private bool VenueEditor(string editType, string venueID, string name, string address, string crossStreet,
                                 string city, string state, string zip, string phone, string ll,
                                 string primaryCategoryId)
        {
            //Venue Edit and Venue ProposeEdit are essentially the same call. Edit requires Superuser privileges

            var parameters = new Dictionary<string, string> {{"callback", "XXX"}, {"v", Version}};

            #region Parameter Conditioning

            //address
            if (!address.Equals(""))
                parameters.Add("address", address);

            //city
            if (!city.Equals(""))
                parameters.Add("city", city);

            //crossStreet
            if (!crossStreet.Equals(""))
                parameters.Add("crossStreet", crossStreet);

            //ll
            if (!ll.Equals(""))
                parameters.Add("ll", ll);

            //name
            if (!name.Equals(""))
                parameters.Add("name", name);


            //phone
            if (!phone.Equals(""))
                parameters.Add("phone", phone);

            //primaryCategoryId
            if (!primaryCategoryId.Equals(""))
                parameters.Add("primaryCategoryId", primaryCategoryId);

            //state
            if (!state.Equals(""))
                parameters.Add("state", state);

            //zip
            if (!zip.Equals(""))
                parameters.Add("zip", zip);

            #endregion Parameter Conditioning

            parameters.Add("oauth_token", _accessToken);

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/venues/" + venueID + "/" + editType),
                                    parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return ((Dictionary<string, object>) jsonDictionary["meta"])["code"].ToString().Equals("200");
        }

        #endregion Venues

        #region Checkins

        /// <summary>
        /// Retrieves information on a specific checkin.
        /// </summary>
        /// <param name="checkinID">The ID of the checkin to retrieve specific information for.</param>
        public Checkin GetCheckinDetails(string checkinID)
        {
            return GetCheckinDetails(checkinID, "");
        }

        /// <summary>
        /// Retrieves information on a specific checkin.
        /// </summary>
        /// <param name="checkinID">The ID of the checkin to retrieve specific information for.</param>
        /// <param name="signature">When checkins are sent to public feeds such as Twitter, foursquare appends a signature (s=XXXXXX) allowing users to bypass the friends-only access check on checkins. The same value can be used here for programmatic access to otherwise inaccessible checkins. Callers should use the bit.ly API to first expand 4sq.com links.</param>
        public Checkin GetCheckinDetails(string checkinID, string signature)
        {
            string query = "";

            if (!signature.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "signature=" + signature;
            }
            if (query.Equals(""))
                query = "?";
            else
                query += "&";
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/checkins/" + checkinID + query + "callback=XXX&v=" +
                              Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Checkin(jsonDictionary);
        }

        /// <summary>
        /// Allows you to check in to a place.
        /// </summary>
        /// <param name="venueId">The venue where the user is checking in. No venueid is needed if shouting or just providing a venue name.</param>
        /// <param name="broadcast">Required. How much to broadcast this check-in, ranging from private (off-the-grid) to public,facebook,twitter. Can also be just public or public,facebook, for example. If no valid value is found, the default is public. Shouts cannot be private.</param>
        /// <param name="ll">Latitude and longitude of the user's location. Only specify this field if you have a GPS or other device reported location for the user at the time of check-in.</param>
        public Checkin AddCheckin(string venueId, string broadcast, string ll)
        {
            return AddCheckin(venueId, "", "", broadcast, ll, "1", "0", "1");
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
        public Checkin AddCheckin(string venueId, string venue, string shout, string broadcast, string ll, string llAcc,
                                  string alt, string altAcc)
        {
            var parameters = new Dictionary<string, string>();

            if (!alt.Equals(""))
                parameters.Add("alt", alt);
            if (!altAcc.Equals(""))
                parameters.Add("altAcc", altAcc);
            if (!broadcast.Equals(""))
                parameters.Add("broadcast", broadcast);
            if (!ll.Equals(""))
                parameters.Add("ll", ll);
            if (!llAcc.Equals(""))
                parameters.Add("llAcc", llAcc);
            if (!shout.Equals(""))
                parameters.Add("shout", shout);
            if (!venue.Equals(""))
                parameters.Add("venue", venue);
            if (!venueId.Equals(""))
                parameters.Add("venueId", venueId);
            parameters.Add("callback", "XXX");
            parameters.Add("v", Version);
            parameters.Add("oauth_token", _accessToken);


            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/checkins/add"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Checkin(jsonDictionary);
        }

        /// <summary>
        /// Recent checkins by friends 
        /// </summary>
        /// <param name="ll">Latitude and longitude of the user's location, so response can include distance. "44.3,37.2"</param>
        /// <param name="limit">Number of results to return, up to 100.</param>
        /// <param name="afterTimestamp">Seconds after which to look for checkins</param>
        public Checkins GetRecentCheckin(string ll, string limit, string afterTimestamp)
        {
            string query = "";

            if (!ll.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "ll=" + ll;
            }

            if (!limit.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "limit=" + limit;
            }

            if (!afterTimestamp.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "afterTimestamp=" + afterTimestamp;
            }

            if (query.Equals(""))
                query = "?";
            else
                query += "&";

            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/checkins/recent" + query + "callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Checkins(jsonDictionary);
        }

        /// <summary>
        /// Add a comment to a check-in  
        /// </summary>
        /// <param name="checkinID">The ID of the checkin to add a comment to.</param>
        /// <param name="text">The text of the comment, up to 200 characters.</param>
        public Comment AddComment(string checkinID, string text)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"text", text}, {"oauth_token", _accessToken}};

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/checkins/" + checkinID + "/addcomment"),
                                    parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Comment(jsonDictionary);
        }

        /// <summary>
        /// Remove commment from check-in   
        /// </summary>
        /// <param name="checkinID">The ID of the checkin to remove a comment from.</param>
        /// <param name="commentId">The id of the comment to remove.</param>
        public Checkin DeleteComment(string checkinID, string commentId)
        {
            var parameters = new Dictionary<string, string>
                                 {
                                     {"callback", "XXX"},
                                     {"commentId", commentId},
                                     {"v", Version},
                                     {"oauth_token", _accessToken}
                                 };


            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/checkins/" + checkinID + "/deletecomment"),
                                    parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Checkin(jsonDictionary);
        }

        #endregion Checkins

        #region Tips

        /// <summary>
        /// Gives details about a tip, including which users (especially friends) have marked the tip to-do or done.    
        /// </summary>
        /// <param name="tipID">required ID of tip to retrieve</param>
        public Tip GetTip(string tipID)
        {
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/tips/" + tipID + "?callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Tip(jsonDictionary);
        }

        /// <summary>
        /// Allows you to add a new tip at a venue.     
        /// </summary>
        /// <param name="venueId">required The venue where you want to add this tip.</param>
        /// <param name="text">required The text of the tip.</param>
        /// <param name="url">A URL related to this tip.</param>
        public Tip AddTip(string venueId, string text, string url)
        {
            var parameters = new Dictionary<string, string>
                                 {
                                     {"callback", "XXX"},
                                     {"text", text},
                                     {"url", url},
                                     {"v", Version},
                                     {"venueId", venueId},
                                     {"oauth_token", _accessToken}
                                 };


            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/tips/add"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Tip(jsonDictionary);
        }

        /// <summary>
        /// Returns a list of tips near the area specified.  
        /// </summary>
        /// <param name="ll">required Latitude and longitude of the user's location.</param>
        /// <param name="limit">optional Number of results to return, up to 500.</param>
        /// <param name="offset">optional Used to page through results.</param>
        /// <param name="filter">If set to friends, only show nearby tips from friends.</param>
        /// <param name="query">Only find tips matching the given term, cannot be used in conjunction with friends filter.</param>
        public Tips FindTip(string ll, string limit, string offset, string filter, string query)
        {
            #region QueryConditioning

            string localQuery = "";


            if (!ll.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "ll=" + ll;
            }

            if (!limit.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "limit=" + limit;
            }

            if (!offset.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "offset=" + offset;
            }

            if (!filter.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "filter=" + filter;
            }

            if (!query.Equals(""))
            {
                if (localQuery.Equals(""))
                    localQuery = "?";
                else
                    localQuery += "&";
                localQuery += "query=" + query;
            }

            if (localQuery.Equals(""))
                localQuery = "?";
            else
                localQuery += "&";

            #endregion QueryConditioning

            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/tips/search" + localQuery + "callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Tips(jsonDictionary);
        }

        /// <summary>
        /// Allows you to mark a tip to-do.   
        /// </summary>
        /// <param name="tipID">required The tip you want to mark to-do.</param>
        public ToDo MarkTipAsToDo(string tipID)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/tips/" + tipID + "/marktodo"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new ToDo(jsonDictionary);
        }

        /// <summary>
        /// Allows the acting user to mark a tip done.   
        /// </summary>
        /// <param name="tipID">required The tip you want to mark done.</param>
        public Tip MarkTipAsDone(string tipID)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/tips/" + tipID + "/markdone"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Tip(jsonDictionary);
        }

        /// <summary>
        /// Allows you to remove a tip from your to-do list or done list.    
        /// </summary>
        /// <param name="tipID">required The tip you want to unmark.</param>
        public Tip UnmarkTip(string tipID)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/tips/" + tipID + "/unmark"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Tip(jsonDictionary);
        }

        #endregion Tips

        #region Photos

        /// <summary>
        /// Get details of a photo. 
        /// </summary>
        /// <param name="photoID">The ID of the photo to retrieve additional information for.</param>
        public Photo GetPhoto(string photoID)
        {
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/photos/" + photoID + "?callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Photo(jsonDictionary);
        }

        /// <summary>
        /// Allows users to add a new photo to a checkin, tip, or a venue in general.
        /// All fields are optional, but exactly one of the id fields (checkinId, tipId, venueId) must be passed in. 
        /// </summary>
        /// <param name="checkinId">the ID of a checkin owned by the user</param>
        /// <param name="tipId">the ID of a tip owned by the user</param>
        /// <param name="venueId">the ID of a venue, provided only when adding a public photo of the venue in general, rather than a private checkin or tip photo using the parameters above</param>
        /// <param name="filePath">The full path to the photo. Should be an image/jpeg</param>
        /// <param name="broadcast">Whether to broadcast this photo, ranging from twitter if you want to send to twitter, facebook if you want to send to facebook, or twitter,facebook if you want to send to both.</param>
        /// <param name="ll">Latitude and longitude of the user's location.</param>
        /// <param name="llAcc">Accuracy of the user's latitude and longitude, in meters.</param>
        /// <param name="alt">Altitude of the user's location, in meters.</param>
        /// <param name="altAcc">Vertical accuracy of the user's location, in meters.</param>
        public Photo AddPhoto(string checkinId, string tipId, string venueId, string filePath, string broadcast,
                              string ll, string llAcc, string alt, string altAcc)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            #region Parameter Conditioning

            //Only one ID. Use the first one found.

            if (!checkinId.Equals(""))
                parameters.Add("checkinId", checkinId);
            else
            {
                if (!tipId.Equals(""))
                    parameters.Add("tipId", tipId);
                else
                    parameters.Add("venueId", venueId);
            }

            if (!broadcast.Equals(""))
                parameters.Add("broadcast", broadcast);

            if (!ll.Equals(""))
                parameters.Add("ll", ll);

            if (!llAcc.Equals(""))
                parameters.Add("llAcc", llAcc);

            if (!alt.Equals(""))
                parameters.Add("alt", alt);

            if (!altAcc.Equals(""))
                parameters.Add("altAcc", altAcc);

            #endregion Parameter Conditioning

            var post = new HttpMultiPartPost("https://api.foursquare.com/v2/photos/add", parameters, filePath);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Photo(jsonDictionary);
        }

        /// <summary>
        /// Allows users to add a new photo to a checkin, tip, or a venue in general.
        /// All fields are optional, but exactly one of the id fields (checkinId, tipId, venueId) must be passed in. 
        /// </summary>
        /// <param name="checkinId">the ID of a checkin owned by the user</param>
        /// <param name="tipId">the ID of a tip owned by the user</param>
        /// <param name="venueId">the ID of a venue, provided only when adding a public photo of the venue in general, rather than a private checkin or tip photo using the parameters above</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="fileStream">The FileStream to the photo. Should be an image/jpeg</param>
        /// <param name="broadcast">Whether to broadcast this photo, ranging from twitter if you want to send to twitter, facebook if you want to send to facebook, or twitter,facebook if you want to send to both.</param>
        /// <param name="ll">Latitude and longitude of the user's location.</param>
        /// <param name="llAcc">Accuracy of the user's latitude and longitude, in meters.</param>
        /// <param name="alt">Altitude of the user's location, in meters.</param>
        /// <param name="altAcc">Vertical accuracy of the user's location, in meters.</param>
        public Photo PhotoAdd(string checkinId, string tipId, string venueId, string fileName, FileStream fileStream,
                              string broadcast, string ll, string llAcc, string alt, string altAcc)
        {
            var parameters = new Dictionary<string, string>
                                 {{"callback", "XXX"}, {"v", Version}, {"oauth_token", _accessToken}};

            #region Parameter Conditioning

            //Only one ID. Use the first one found.

            if (!checkinId.Equals(""))
                parameters.Add("checkinId", checkinId);
            else
            {
                if (!tipId.Equals(""))
                    parameters.Add("tipId", tipId);
                else
                    parameters.Add("venueId", venueId);
            }

            if (!broadcast.Equals(""))
                parameters.Add("broadcast", broadcast);

            if (!ll.Equals(""))
                parameters.Add("ll", ll);

            if (!llAcc.Equals(""))
                parameters.Add("llAcc", llAcc);

            if (!alt.Equals(""))
                parameters.Add("alt", alt);

            if (!altAcc.Equals(""))
                parameters.Add("altAcc", altAcc);

            #endregion Parameter Conditioning

            var post = new HttpMultiPartPost("https://api.foursquare.com/v2/photos/add", parameters, fileName,
                                             fileStream);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Photo(jsonDictionary);
        }

        #endregion Photos

        #region Settings

        /// <summary>
        /// Returns a setting for the acting user.   
        /// </summary>
        public Settings GetSettings()
        {
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/settings/all?callback=XXX&v=" + Version +
                              "&callback=XXX&v=" + Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Settings(jsonDictionary);
        }

        /// <summary>
        /// Change a setting for the given user.    
        /// </summary>
        /// <param name="setting">The name of a setting</param>
        /// <param name="value">True or False</param>
        public Settings ModifySettings(string setting, bool value)
        {
            string strValue = value ? "1" : "0";

            var parameters = new Dictionary<string, string>
                                 {
                                     {"callback", "XXX"},
                                     {"v", Version},
                                     {"oauth_token", _accessToken},
                                     {"value", strValue}
                                 };

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/settings/" + setting + "/set"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return new Settings(jsonDictionary);
        }

        #endregion Settings

        #region Specials

        /// <summary>
        /// Gives details about a special, including text and whether it is unlocked for the current user. 
        /// </summary>
        /// <param name="specialID">required ID of special to retrieve</param>
        /// <param name="venueId">required ID of a venue the special is running at</param>
        public Special GetSpecial(string specialID, string venueId)
        {
            var get = new HttpGet();
            string endPoint = "https://api.foursquare.com/v2/specials/" + specialID + "?venueId=" + venueId +
                              "&callback=XXX&v=" + Version + "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);

            return new Special(jsonDictionary);
        }


        /// <summary>
        /// Returns a list of specials near the current location.  
        /// </summary>
        /// <param name="ll">Required. Latitude and longitude to search near.</param>
        /// <param name="llAcc">Accuracy of latitude and longitude, in meters.</param>
        /// <param name="alt">Altitude of the user's location, in meters.</param>
        /// <param name="altAcc">Accuracy of the user's altitude, in meters.</param>
        /// <param name="limit">Number of results to return, up to 50.</param>
        public Specials FindSpecialsNearby(string ll, string llAcc, string alt, string altAcc, string limit)
        {
            var get = new HttpGet();

            #region Query Conditioning

            string query = "";

            //ll
            if (!ll.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "ll=" + ll;
            }

            //llAcc
            if (!llAcc.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "llAcc=" + llAcc;
            }

            //alt
            if (!alt.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "alt=" + alt;
            }

            //altAcc
            if (!altAcc.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "altAcc=" + altAcc;
            }

            //limit
            if (!limit.Equals(""))
            {
                if (query.Equals(""))
                    query = "?";
                else
                    query += "&";
                query += "limit=" + limit;
            }

            if (query.Equals(""))
                query = "?";
            else
                query += "&";

            #endregion Query Conditioning

            string endPoint = "https://api.foursquare.com/v2/specials/search" + query + "callback=XXX&v=" + Version +
                              "&oauth_token=" + _accessToken;
            get.Request(endPoint);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(get.ResponseBody);
            return new Specials(jsonDictionary);
        }

        /// <summary>
        /// Allows users to indicate a Special is improper in some way.     
        /// </summary>
        /// <param name="id">required The id of the special being flagged</param>
        /// <param name="venueId">required The id of the venue running the special.</param>
        /// <param name="problem">required One of not_redeemable, not_valuable, other</param>
        /// <param name="text">Additional text about why the user has flagged this special</param>
        public bool FlagSpecial(string id, string venueId, string problem, string text)
        {
            var parameters = new Dictionary<string, string>
                                 {
                                     {"callback", "XXX"},
                                     {"v", Version},
                                     {"oauth_token", _accessToken},
                                     {"venueId", venueId},
                                     {"problem", problem}
                                 };

            if (!text.Equals(""))
                parameters.Add("text", text);

            var post = new HttpPost(new Uri("https://api.foursquare.com/v2/specials/" + id + "/flag"), parameters);
            Dictionary<string, object> jsonDictionary = Helpers.JsonDeserializer(post.ResponseBody);
            return ((Dictionary<string, object>) jsonDictionary["meta"])["code"].ToString().Equals("200");
        }

        #endregion Specials
    }
}