using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExamApi.Models
{
    public class ProductBarcodeModel
    {
        [Key]
        public int id { get; set; }
        public string? barcode_id { get; set; }
        public string? barcode { get; set; }
        public DateTime? created_date { get; set; }

    }
    public class RequestListProduct
    {

    }

    public class RequestCreateProduct
    {
        [JsonPropertyName("barcode_id")]
        public string? BarcodeId { get; set; } = string.Empty;
    }

    public class RequestDeleteProduct
    {
        [JsonPropertyName("barcode_id")]
        public string? BarcodeId { get; set; } = string.Empty;
    }


    public class ListProduct
    {

        [JsonPropertyName("details")]
        public List<ProductDetail>? Details { get; set; }

        public class ProductDetail
        {

            [JsonPropertyName("id")]
            public int? Id { get; set; }

            [JsonPropertyName("barcode_id")]
            public string? BarcodeId { get; set; }

            [JsonPropertyName("barcode")]
            public string? Barcode { get; set; }

            [JsonPropertyName("created_date")]
            public DateTime? CreatedDate { get; set; }
        }
    }
}
