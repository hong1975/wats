using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using RestSharp;

namespace WatsClient.REST
{
    public class DigestAuthenticator : IAuthenticator
    {
        private readonly string mUser;
        private readonly string mPass;

        public DigestAuthenticator(string user, string pass)
        {
            mUser = user;
            mPass = pass;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.Credentials = new NetworkCredential(mUser, mPass);
        }
    }
}
