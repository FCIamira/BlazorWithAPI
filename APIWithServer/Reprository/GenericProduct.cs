using day1.DTO;
using day1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace day1.Reprository
{
    public class GenericProduct : IGenerateProduct
    {
        private readonly ITIContext context;

        public GenericProduct(ITIContext context)
        {

            this.context = context;
        }

        public List<ProductWithDTO> GetAll()
        {
            return context.products
                .Include(p => p.Category) 
                .Select(p => new ProductWithDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    categoryName = p.Category != null ? p.Category.Name : null 
                })
                .ToList();
        }
        [Authorize]
        [HttpGet("{id:int}")]
        public ProductWithDTO GetByID(int id)
        {
            var prod = context.products
        .Where(p => p.Id == id)
        .Include(p => p.Category)
        .Select(p => new ProductWithDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            categoryName = p.Category.Name
        })
        .FirstOrDefault();

            return prod;
        }


        public Product Insert(Product entity)
        {
            context.products.Add(entity);
            Save();
            return entity;
        }

        public void Delete(int id)
        {
            var category = context.products.FirstOrDefault(i=>i.Id==id);
            if (category != null)
            {
                context.products.Remove(category);

            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
