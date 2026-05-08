using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExamApi.Models
{
    public class AuthModel
    {
        [Key]
        public Guid user_index { get; set; }
        public string? username { get; set; }
        public string? password_hash { get; set; }
        public DateTime? created_date { get; set; }

    }

    public class RequestRegister
    {
        [JsonPropertyName("user_name")]
        public string? Username { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }

    public class RequestLogin
    {
        [JsonPropertyName("user_name")]
        public string? Username { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
