using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExamApi.Models
{
    public class DocumentsModel
    {
        [Key]
        public Guid Document_index { get; set; }
        public string? Document_id { get; set; }
        public int? Status { get; set; }
        public string? Remark { get; set; }
        public DateTime? Created_date { get; set; }

    }

    public class RequestListApproval
    {

    }

    public class ListApproval
    {

        [JsonPropertyName("details")]
        public List<ApprovalDetail>? Details { get; set; }

        public class ApprovalDetail
        {

            [JsonPropertyName("document_index")]
            public Guid? DocumentIndex { get; set; }

            [JsonPropertyName("document_id")]
            public string? DocumentId { get; set; }

            [JsonPropertyName("status")]
            public int? Status { get; set; }

            [JsonPropertyName("remark")]
            public string? Remark { get; set; }
        }
    }


    public class RequestSaveApproval
    {
        [JsonPropertyName("documents")]
        public List<DocumentsDetail> Documents { get; set; } = [];

        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
    }

    public class DocumentsDetail
    {
        [JsonPropertyName("document_index")]
        public Guid DocumentIndex { get; set; }
    }
}
