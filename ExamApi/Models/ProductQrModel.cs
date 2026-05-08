using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExamApi.Models
{
    public class ProductQrModel
    {
        [Key]
        public int id { get; set; }
        public string? barcode_id { get; set; }
        public string? barcode { get; set; }
        public DateTime? created_date { get; set; }

    }
    public class RequestListProductQr
    {

    }

    public class RequestCreateProductQr
    {
        [JsonPropertyName("barcode_id")]
        public string? BarcodeId { get; set; } = string.Empty;
    }

    public class RequestDeleteProductQr
    {
        [JsonPropertyName("barcode_id")]
        public string? BarcodeId { get; set; } = string.Empty;
    }


    public class ListProductQr
    {

        [JsonPropertyName("details")]
        public List<ProductQrDetail>? Details { get; set; }

        public class ProductQrDetail
        {

            [JsonPropertyName("id")]
            public int? Id { get; set; }

            [JsonPropertyName("barcode_id")]
            public string? BarcodeId { get; set; }

            [JsonPropertyName("created_date")]
            public DateTime? CreatedDate { get; set; }
        }
    }
}
