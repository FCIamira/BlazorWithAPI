using day1.DTO;
using day1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace day1.Reprository
{
    
    public class GenerateCategory :  IGenerateCategory
    {
        private readonly ITIContext context;

        public GenerateCategory(ITIContext context) {

            this.context = context;
        }

        public List<CategorytWithDTO> GetAll()
        {
            return context.categories
                .Include(c => c.products) 
                .Select(c => new CategorytWithDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    productNames = c.products.Select(p => p.Name).ToList() 
                })
                .ToList();
        }


        public CategorytWithDTO GetByID(int id)
        {
            var cat= context.categories     
                .Where(p => p.Id == id)
                .Include(p => p.products)
                .Select(p => new CategorytWithDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    productNames = p.products.Select(c => c.Name).ToList()
                })
                .FirstOrDefault();
            return cat;
        }

        public Category Insert(Category entity)
        {
            context.categories.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var category = context.categories.FirstOrDefault(i=>i.Id==id);
            if (category != null)
            {
                context.categories.Remove(category);
               
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

       
       
    }
}
