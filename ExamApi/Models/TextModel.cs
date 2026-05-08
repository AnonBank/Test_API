using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExamApi.Models
{
    public class TextModel
    {
        [Key]
        public Guid text_index { get; set; }
        public string? text_id { get; set; }
        public string? text_name { get; set; }
        public DateTime? created_date { get; set; }

    }


    public class RequestText
    {
        [JsonPropertyName("text_id")]
        public string? TextId { get; set; }
    }

    public class ListText
    {

        [JsonPropertyName("details")]
        public List<TextDetail>? Details { get; set; }

        public class TextDetail
        {

            [JsonPropertyName("text_index")]
            public Guid? TextIndex { get; set; }

            [JsonPropertyName("text_id")]
            public string? TextId { get; set; }

            [JsonPropertyName("text_name")]
            public string? TextName { get; set; }
        }
    }

}
