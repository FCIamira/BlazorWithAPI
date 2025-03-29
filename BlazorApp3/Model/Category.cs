namespace BlazorApp3.Model
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string description { get; set; }
        public List<string>? productNames { get; set; }

    }
}
