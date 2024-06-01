using System.Text.Json.Serialization;

namespace Abstra.Mappers.Requests
{
    public class ClientPatchRequestChangePasswordDto
    {
        [JsonPropertyName("oldPassword")]
        public required string OldPassword { get; set; }

        [JsonPropertyName("newPassword")]
        public required string NewPassword { get; set; }
    }
}
