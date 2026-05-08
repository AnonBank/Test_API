using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExamApi.Models
{
    public class QueueModel
    {
        [Key]
        public int id { get; set; }
        public string? current_char { get; set; }
        public int current_number { get; set; }
        public DateTime? updated_at { get; set; }

    }

}
