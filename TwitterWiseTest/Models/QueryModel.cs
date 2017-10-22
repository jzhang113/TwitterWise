using System.ComponentModel.DataAnnotations;

namespace TwitterWise.Models
{
    public class QueryModel
    {
        [Required]
        public string Query { get; set; }
    }
}
