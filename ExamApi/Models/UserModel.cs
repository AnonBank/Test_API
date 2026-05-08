using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExamApi.Models
{
    public class UserModel
    {
        [Key]
        public Guid user_index { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public int? age { get; set; }
        public string? address { get; set; }
        public DateTime? birthdate { get; set; }
        public DateTime? created_date { get; set; }

    }


    public class RequestCreateUser
    {
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("age")]
        public int? Age { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("birthdate")]
        public DateTime? BirthDate { get; set; }
    }

    public class RequestListUser
    {

    }

    public class ListUser
    {

        [JsonPropertyName("details")]
        public List<UserDetail>? Details { get; set; }

        public class UserDetail
        {

            [JsonPropertyName("user_index")]
            public Guid? UserIndex { get; set; }

            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [JsonPropertyName("last_name")]
            public string? LastName { get; set; }

            [JsonPropertyName("age")]
            public int? Age { get; set; }

            [JsonPropertyName("address")]
            public string? Address { get; set; }

            [JsonPropertyName("birthdate")]
            public string? BirthDate { get; set; }

            [JsonPropertyName("created_date")]
            public DateTime? CreatedDate { get; set; }
        }
    }
}
