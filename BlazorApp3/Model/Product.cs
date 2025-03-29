using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorApp3.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }

        public string categoryName { get; set; }
        public override string ToString()
        {
            return $"name : {Name} \t {Description} \t price: {Price} \t ";
        }
    }
}
