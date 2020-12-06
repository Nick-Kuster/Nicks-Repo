using RestSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TMT5K.Domain;

namespace TMT5K.Infrastructure
{
    public class YouTubeAPI : IYoutubeAPI
    {
        private readonly IAPIInfo _apiInfo;

        public YouTubeAPI(IAPIInfo apiInfo)
        {
            _apiInfo = apiInfo;
        }

        public async Task CallAPI()
        {
            var client = new RestClient(_apiInfo.Endpoint);
            var request = new RestRequest(Method.GET);
            request.AddParameter("key", _apiInfo.APIKey);
            request.AddParameter("channelID", _apiInfo.Arguments["ChannelID"]);
            request.AddParameter("part", "snippet,id");
            request.AddParameter("order", "date");
            request.AddParameter("maxResults", 50);
            var response = await client.ExecuteAsync(request);

            var x = response;
        }
    }
}
