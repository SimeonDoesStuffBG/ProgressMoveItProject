using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace ProgressTestApp.HTTP
{
    class UserToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }
        class HttpUserControls : HttpConnection
        {
            private static string tokenUrl = "api/v1/token";
            private static string accessToken;
            public static string AccessToken
            {
                get
                {
                    return accessToken;
                }
            }
            private static string refreshToken;

            public static string RefreshToken 
            {
                get 
                {
                    return refreshToken; 
                }
            }
            public static async Task Login(string username, string password)
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new Exception("Username is empty!");
                }

                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception("Password is empty!");
                }
                var data = new Dictionary<string, string>
            {
                { "grant_type",  "password" },
                { "username", username },
                { "password", password }
            };

                using FormUrlEncodedContent jsonContent = new FormUrlEncodedContent(data);

                using HttpResponseMessage response = await sharedClient.PostAsync(tokenUrl, jsonContent);

                //response.EnsureSuccessStatusCode()
                //    .WriteRequestToConsole();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Username or password is incorrect");
                }

                //var jsonResponse = await response.Content.ReadFromJsonAsync<UserToken>();
                var jsonResponse = await HttpContentJsonExtensions.ReadFromJsonAsync<UserToken>(response.Content);
                accessToken = jsonResponse?.access_token;
                refreshToken = jsonResponse?.refresh_token;

                Debug.WriteLine($"{jsonResponse?.access_token}; {jsonResponse?.refresh_token}\n");
            }
        }
    
}
