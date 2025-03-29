using BlazorApp3.Model;
using System.Net.Http.Json;

namespace BlazorApp3.Service
{
    public class CategoryService : IService<Category>
    {
        HttpClient client;
        public CategoryService(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri("http://localhost:5280");
        }
        public async Task<Category> GetById(int id)
        {
            Category categoryobj = await client.GetFromJsonAsync<Category>($"/api/Category/{id}");
            if (categoryobj == null)
            {
                Console.WriteLine("Not Found");
            }
            return categoryobj;
        }
        public async Task<List<Category>> GetAll()
        {
            List<Category> categoryobj = await client.GetFromJsonAsync<List<Category>>("api/Category") ;
            return categoryobj;
        }

        public async Task insert(Category entity)
        {
            await client.PostAsJsonAsync<Category>("api/Category", entity);

        }

        public async Task Delete(int id)
        {
            await client.DeleteAsync($"api/Category/{id}");
        }

        public async Task Update(int id, Category entity)
        {
            await client.PutAsJsonAsync($"api/Category/{id}", entity);
        }
    }
}
