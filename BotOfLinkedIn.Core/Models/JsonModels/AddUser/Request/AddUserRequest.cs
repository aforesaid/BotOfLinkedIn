using System.Text.Json.Serialization;

namespace BotOfLinkedIn.Core.Models.JsonModels.AddUser.Request
{
    //https://www.linkedin.com/voyager/api/growth/normInvitations?action=verifyQuotaAndCreate
    public class AddUserRequest
    {
        [JsonPropertyName("invitation")] 
        public Invitation Details { get; set; } = new Invitation();
    }

    public class Invitation
    {
        [JsonPropertyName("emberEntityName")]
        public string EmberEntityName { get; set; } = "growth/invitation/norm-invitation";

        [JsonPropertyName("invitee")] 
        public Invitee Info { get; set; } = new Invitee();
        
        [JsonPropertyName("trackingId")]
        public string TrackingId { get; set; }
    }

    public class Invitee
    {
        [JsonPropertyName("com.linkedin.voyager.growth.invitation.InviteeProfile")]
        public Profile Profile { get; set; } = new Profile();
    }

    public class Profile
    {
        [JsonPropertyName("profileId")]
        public string ProfileId { get; set; }
    }
}