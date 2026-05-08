using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExamApi.Models
{
    public class QuestionResultModel
    {
        [Key]
        public int id { get; set; }
        public int question_no { get; set; }
        public string? question_text { get; set; }
        public string? answer1 { get; set; }
        public string? answer2 { get; set; }
        public string? answer3 { get; set; }
        public string? answer4 { get; set; }
        public int crrect_answer { get; set; }
        public DateTime? created_date { get; set; }

    }
    public class RequestListQuestionResult
    {

    }

    public class RequestCreateQuestionResult
    {

        [JsonPropertyName("question")]
        public string? QuestionText { get; set; }

        [JsonPropertyName("answer1")]
        public string? Answer1 { get; set; }

        [JsonPropertyName("answer2")]
        public string? Answer2 { get; set; }

        [JsonPropertyName("answer3")]
        public string? Answer3 { get; set; }

        [JsonPropertyName("answer4")]
        public string? Answer4 { get; set; }
    }

    public class RequestDeleteQuestionResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }


    public class ListQuestionResult
    {

        [JsonPropertyName("details")]
        public List<QuestionDetail>? Details { get; set; }

        public class QuestionDetail
        {

            [JsonPropertyName("id")]
            public int? Id { get; set; }

            [JsonPropertyName("question_no")]
            public int? QuestionNo { get; set; }

            [JsonPropertyName("question_text")]
            public string? QuestionText { get; set; }

            [JsonPropertyName("answer1")]
            public string? Answer1 { get; set; }

            [JsonPropertyName("answer2")]
            public string? Answer2 { get; set; }

            [JsonPropertyName("answer3")]
            public string? Answer3 { get; set; }

            [JsonPropertyName("answer4")]
            public string? Answer4 { get; set; }

            [JsonPropertyName("created_date")]
            public DateTime? CreatedDate { get; set; }
        }
    }
    public class RequestSubmit
    {
        public string UserName { get; set; } = string.Empty;

        public List<AnswerDetail> Answers { get; set; } = [];

        public class AnswerDetail
        {
            public int QuestionId { get; set; }

            public int SelectedAnswer { get; set; }
        }
    }
    public class ResponseSubmit
    {
        public string UserName { get; set; } = string.Empty;

        public int TotalScore { get; set; }

        public int TotalCorrect { get; set; }

        public int TotalWrong { get; set; }
    }
}
