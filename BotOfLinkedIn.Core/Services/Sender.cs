using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BotOfLinkedIn.Core.Models.JsonModels.AddUser.Request;
using BotOfLinkedIn.Core.Models.JsonModels.GetUsers.Responses;

namespace BotOfLinkedIn.Core.Services
{
    public class Sender
    {
        //TODO: переписать на dictionary
        private string _cookie;
        private string _csrfToken;

        public Sender(string cookie, string csrfToken)
        {
            _cookie = cookie;
            _csrfToken = csrfToken;
        }
        
        public async Task<List<UserInfoItem>> GetUsers(int offset = 0)
        {
            var client = new HttpClient();
            //запрос на it-recruiter - 2 ранг
            var url =
                $"https://www.linkedin.com/voyager/api/search/dash/clusters?decorationId=com.linkedin.voyager.dash.deco.search.SearchClusterCollection-103&origin=FACETED_SEARCH&q=all&query=(keywords:it%20rectuiter,flagshipSearchIntent:SEARCH_SRP,queryParameters:(network:List(S),resultType:List(PEOPLE)),includeFiltersInResponse:false)&start={offset}";
            client.DefaultRequestHeaders.Add("cookie",_cookie);
            client.DefaultRequestHeaders.Add("csrf-token",_csrfToken);
            client.DefaultRequestHeaders.Add("x-restli-protocol-version","2.0.0");
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseModel = JsonSerializer.Deserialize<UserInfoResponse>(responseString);
            var content = Convert.ToString(responseModel.Result[1]);
            var users = JsonSerializer.Deserialize<UserInfoResults>(content);
            return users?.Items.ToList();
        }

        public async Task AddToContact(string profile, string tag)
        {
            var client = new HttpClient();

            var url = "https://www.linkedin.com/voyager/api/growth/normInvitations?action=verifyQuotaAndCreate";
            client.DefaultRequestHeaders.Add("cookie",_cookie);
            client.DefaultRequestHeaders.Add("csrf-token",_csrfToken);
            client.DefaultRequestHeaders.Add("x-restli-protocol-version","2.0.0");

            var request = new AddUserRequest {Details =
            {
                TrackingId = tag
            }};
            
            request.Details.Info.Profile.ProfileId = profile;
            var requestString = JsonSerializer.Serialize(request);

            var content = new StringContent(requestString, Encoding.UTF8,
                "application/vnd.linkedin.normalized+json+2.1");
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Successed");
            }
            else
            {
                Console.WriteLine("Failed");
                throw new ArgumentException("Упс");
            }
        }
    }
}