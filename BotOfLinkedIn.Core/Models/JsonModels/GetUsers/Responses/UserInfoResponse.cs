using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BotOfLinkedIn.Core.Models.JsonModels.GetUsers.Responses
{
    public class UserInfoResponse
    {
        [JsonPropertyName("elements")]
        public object[] Result { get; set; }
    }
    public class UserInfoResults
    {
        [JsonPropertyName("results")]
        public UserInfoItem[] Items { get; set; }
    }

    public class UserInfoItem
    {
        [JsonPropertyName("entityUrn")]
        //  "entityUrn": "urn:li:fsd_entityResultViewModel:(urn:li:fsd_profile:ACoAACUzGj0Bdy_1YYjRsjSGC306jQBrLpFjpKs,SEARCH_SRP)",
        public string EntityUrl
        {
            get;
            set;
        }

        public string GetEntityUrl
        {
            get
            {
                var regular = @"urn:li:fsd_profile:(.*?),";
                var match = Regex.Match(EntityUrl, regular);
                return match.Groups[1].Value;
            }
        }

        [JsonPropertyName("trackingId")]
        public string TrackingId { get; set; }
        [JsonPropertyName("entityCustomTrackingInfo")]
        public EntityCustomTrackingInfo Info { get; set; }
    }

    public class EntityCustomTrackingInfo
    {
        [JsonPropertyName("memberDistance")]
        public string Distance { get; set; }

        public bool IsValid => Distance is "DISTANCE_2" or "DISTANCE_3";
    }
}