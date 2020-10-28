using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserCommon;
using Ziroh.Misc.Common;

namespace ClientApplication
{
    class SampleClient : IUserService
    {
        string baseUri = default(string);
        GenericRestClient client;
        public SampleClient()
        {
            baseUri = "http://127.0.0.1:8080/users";
            client = new GenericRestClient(baseUri);
        }

        public async Task<Data> GetUserData(string userid)
        {
            if (String.IsNullOrEmpty(userid))
            {
                Console.WriteLine("Missing Parameter.");
                return new Data(string.Empty,string.Empty,string.Empty);
            }

            string relativeUrl = string.Format("/{0}", userid);
            Data userdetails = null;
            Action<Data> onSuccess = new Action<Data>((credential =>
            {
                userdetails = credential;
            
                Console.WriteLine("Id: " + userdetails.id + " " + "Name: " + userdetails.Name + " " + "Age: " + userdetails.Age);
            }));
            Action<HttpFailure> onFailure = new Action<HttpFailure>((failure) =>
            {
                Console.WriteLine("http failure "+failure.Message);
            });
            await client.GetAsync(onSuccess, onFailure, relativeUrl);
            
            return userdetails;
        }

        public async Task<Data[]> GetAllUsers()
        { 
            Data[] users = null;
            Action<Data[]> onSuccess = new Action<Data[]>((result) => {
                users = result;
                foreach(Data data in users)
                {
                    Console.WriteLine("Id: " + data.id + " " + "Name: " + data.Name + " " + "Age: " + data.Age);
                }
            });
            Action<HttpFailure> onFailure = new Action<HttpFailure>((failure) => {
                users = null;
                Console.WriteLine("http failure " + failure.Message);
            });
            string relativeUrl = "";
            await client.GetAsync(onSuccess, onFailure, relativeUrl);
            return users;
        }

        public async Task<UserCommon.Result> CreateUser(Data details)
        {
            if(details == null || String.IsNullOrEmpty(details.id) || String.IsNullOrEmpty(details.Name) || String.IsNullOrEmpty(details.Age))
            {
                Console.WriteLine("Missing Parameter");
                return new UserCommon.Result()
                {
                    error_code = 1,
                    error_message = "Missing parameters"
                };
            }
            UserCommon.Result userAddResult = default(UserCommon.Result);
            Action<UserCommon.Result> OnSuccess = new Action<UserCommon.Result>((result) =>
            {
                userAddResult = result;
                Console.WriteLine("User Successfully Added.");
            });
            Action<HttpFailure> OnFailure = new Action<HttpFailure>((failure) => {

                userAddResult = new UserCommon.Result()
                {
                    error_code = 1,
                    error_message = failure.Message
                };
                Console.WriteLine("http failure : " + failure.Message);
            });
            string relativeUrl = "";
            client.SetBaseInput(typeof(Data));
            client.SetBaseOutput(typeof(UserCommon.Result));
            await client.PostAsync(details, OnSuccess, OnFailure, relativeUrl);
            return userAddResult;
        }

        public async Task<UserCommon.Result> UpdateUser(Data details)
        {
            if (details == null || String.IsNullOrEmpty(details.id) || String.IsNullOrEmpty(details.Name) || String.IsNullOrEmpty(details.Age))
            {
                Console.WriteLine("Missing Parameter");
                return new UserCommon.Result()
                {
                    error_code = 1,
                    error_message = "Missing parameters"
                };
            }
            UserCommon.Result userUpdateResult = default(UserCommon.Result);
            string relativeUrl = "";
            Action<UserCommon.Result> OnSuccess = new Action<UserCommon.Result>((result) =>
            {
                userUpdateResult = result;
                Console.WriteLine("User Successfully Updated.");
            });
            Action<HttpFailure> OnFailure = new Action<HttpFailure>((failure) => {

                userUpdateResult = new UserCommon.Result()
                {
                    error_code = 1,
                    error_message = failure.Message
                };
                Console.WriteLine("http failure : " + failure.Message);
            });
            client.SetBaseInput(typeof(Data));
            client.SetBaseOutput(typeof(UserCommon.Result));
            await client.PutAsync(details, OnSuccess, OnFailure, relativeUrl);
            return userUpdateResult;
        }

        public async Task<UserCommon.Result> DeleteUser(string userid)
        {
            if (String.IsNullOrEmpty(userid))
            {
                return new UserCommon.Result()
                {
                    error_code = 1,
                    error_message = "Missing parameters"
                };
            }
            string relativeUrl = string.Format("/{0}", userid);
            UserCommon.Result res = default(UserCommon.Result);
            Action<UserCommon.Result> OnSuccess = new Action<UserCommon.Result>((result) =>
            {
                res = result;
                Console.WriteLine("User Removed Successfully.");
            });
            Action<HttpFailure> OnFailure = new Action<HttpFailure>((failure) => {

                res = new UserCommon.Result()
                {
                    error_code = 1,
                    error_message = failure.Message
                };
                Console.WriteLine("http failure : " + failure.Message);
            });
            await this.client.DeleteAsync<UserCommon.Result>(OnSuccess, OnFailure, relativeUrl);
            return res;
        }
    }
}