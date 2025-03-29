using System.Text.Json.Serialization;

namespace day1.DTO
{
    public class CategorytWithDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string?  productName{ get; set; }
        public List<string>? productNames { get; set; }
    }
}
