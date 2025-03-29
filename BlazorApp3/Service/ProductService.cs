using BlazorApp3.Model;
using Microsoft.Extensions.FileProviders;
using System.Net.Http.Json;

namespace BlazorApp3.Service
{
    public class ProductService : IService<Product>
    {
        
       HttpClient client;
        public ProductService(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri("http://localhost:5280");
        }
        public async Task<Product> GetById(int id)
        {
            Product productobj = await client.GetFromJsonAsync<Product>($"/api/Prod/{id}") ;
            Console.WriteLine($"{productobj.Id} == {id}  : {productobj.Name}");
            return productobj ;
        }
        public async Task<List<Product>> GetAll()
        {
            List<Product> Products= await client.GetFromJsonAsync<List<Product>>("/api/Prod") ;

            foreach ( var productobj in Products)
            {
                Console.WriteLine($"{productobj.Id}  : {productobj.Name}");
            }
            if (Products == null)
            {
                Console.WriteLine("Not Found");
            }
            return Products;          
        }
        public async Task insert(Product entity)
        {
             var x=await client.PostAsJsonAsync<Product>("/api/Prod", entity);            
        }

        public async  Task Delete(int id)
        {
             await client.DeleteAsync($"/api/Prod/{id}");
        }

        public async Task Update(int id, Product entity)
        {
            var response = await client.PutAsJsonAsync($"/api/Prod/{id}", entity);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
            }

            response.EnsureSuccessStatusCode();
        }

    }
}
