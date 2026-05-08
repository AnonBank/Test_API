using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExamApi.Models
{
    public class ExamResultModel
    {
        [Key]
        public int id { get; set; }
        public string user_name { get; set; }
        public int total_score { get; set; }
        public int total_correct { get; set; }
        public int total_wrong { get; set; }
        public DateTime? created_date { get; set; }

    }
  
}
