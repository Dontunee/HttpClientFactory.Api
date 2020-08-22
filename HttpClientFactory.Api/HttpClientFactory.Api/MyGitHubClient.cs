using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactory.Api
{
    public class MyGitHubClient
    {
        public HttpClient Client;
        public MyGitHubClient(HttpClient client)
        {
            Client = client;
        }


    }
}
