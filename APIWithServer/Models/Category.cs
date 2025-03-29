using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace day1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public List<Product>? products { get; set; }

        
    }
}
