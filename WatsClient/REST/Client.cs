﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using System.Threading;
using WatsClient.Bindings;

namespace WatsClient.REST
{
    public class Client
    {
        public static string AuthUser { get; set; }
        public static string AuthPass { get; set; }
        public static string Proxy { get; set; }
        public static string BaseUrl { get; set; }

        public delegate void LoginResult(HttpStatusCode statusCode, User self);
        public static event LoginResult OnLoginResult;

        public delegate void GetAllUsersResult(HttpStatusCode statusCode, Users users);
        public static event GetAllUsersResult OnGetAllUsersResult;

        public delegate void GetResourceListResult(HttpStatusCode statusCode, ResourceList resourceList);
        public static event GetResourceListResult OnGetResourceListResult;

        public static void LogIn()
        {
            new Thread(delegate()
            {
                RestRequest request = new RestRequest();
                request.Method = Method.POST;
                request.Resource = "registration";

                IRestResponse<User> response = Execute<User>(request);
                if (OnLoginResult != null)
                {
                    OnLoginResult(response.StatusCode, response.Data);
                }

            }).Start();
        }

        public static void LogOut()
        {
            RestRequest request = new RestRequest();
            request.Method = Method.DELETE;
            request.Resource = "registration";
            Execute<User>(request);
        }

        public static void GetAllUsers()
        {
            new Thread(delegate()
            {
                RestRequest request = new RestRequest();
                request.Method = Method.GET;
                request.Resource = "users";

                IRestResponse<Users> response = Execute<Users>(request);
                if (OnGetAllUsersResult != null)
                {
                    OnGetAllUsersResult(response.StatusCode, response.Data);
                }

            }).Start();
        }

        public static void GetResourceList()
        {
            new Thread(delegate()
            {
                RestRequest request = new RestRequest();
                request.Method = Method.GET;
                request.Resource = "resource";

                IRestResponse<ResourceList> response = Execute<ResourceList>(request);
                if (OnGetResourceListResult != null)
                {
                    OnGetResourceListResult(response.StatusCode, response.Data);
                }

            }).Start();
        }

        static Client()
        {
            client = new RestClient();
            client.CookieContainer = new CookieContainer();
        }

        public static IRestResponse<T> Execute<T>(RestRequest request) where T : new()
        {
            client.BaseUrl = BaseUrl;
            client.Authenticator = new DigestAuthenticator(AuthUser, AuthPass);
            if (!string.IsNullOrEmpty(Proxy))
            {
                WebProxy proxy = new WebProxy(Proxy, false);
                client.Proxy = proxy;
            }
            else
            {
                client.Proxy = null;
            }

            IRestResponse<T> response = client.Execute<T>(request);
            return response;
        }

        private static RestClient client;


    }
}
