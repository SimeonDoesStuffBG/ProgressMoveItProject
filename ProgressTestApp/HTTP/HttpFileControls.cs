using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Diagnostics;
using ProgressTestApp.Models;
using System.IO;
using System.Net.Http.Headers;
using ProgressTestApp.Functions;

namespace ProgressTestApp.HTTP
{
    class HttpFileControls :HttpConnection
    {
        const string selfUrl = "/api/v1/users/self";
        private static int homeFolderID;

        public static int HomeFolderID
        {
            get 
            {
                return homeFolderID;
            }
        }
        public static async Task GetHomeFolder()
        {
            using HttpResponseMessage response = await sharedClient.GetAsync(selfUrl);

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot get User information.");
            }
            var jsonResponse = await HttpContentJsonExtensions.ReadFromJsonAsync<UserInfo>(response.Content);

            if (jsonResponse != null)
            {
                homeFolderID = jsonResponse.homeFolderID;
            }
        }

        public static async Task AddNewFile(string path, string name)
        {
            string postUrl = $"/api/v1/folders/{homeFolderID}/files";
            if (!File.Exists(path) || string.IsNullOrEmpty(name))
            {
                throw new Exception("File not found");
            }

            string parentFolder = Directory.GetParent(path).Name;
            
            //using MultipartFormDataContent jsonContent = new MultipartFormDataContent();
            //jsonContent.Add(new StreamContent(File.Open(path, FileMode.OpenOrCreate)));
            var stream = new FileStream(path, FileMode.Open);
            using MultipartFormDataContent content = new MultipartFormDataContent();
            HttpContent streamContent = new StreamContent(stream);
            streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = $"{parentFolder}-{name}"
            };

            //streamContent = new StreamContent(stream);
            content.Add(streamContent, "file");
            using HttpResponseMessage response = await sharedClient.PostAsync(postUrl, content);

            //response.EnsureSuccessStatusCode()
            //    .WriteRequestToConsole();
            if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new Exception($"The file {name} already exists in your home folder. Change the name and try again");
            }

            if (!response.IsSuccessStatusCode)
            { 
                throw new Exception($"Could not upload file {response.StatusCode}");
            }
        }

       
    }
}
