using GraphQlDemo.Context;
using GraphQlDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace GraphQlDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _context;
        public ProductService(DbContextClass context)
        {
            _context = context;
        }

        public async Task<ProductDetails> GetProductDetailByIdAsync(Guid productId) => await _context.Products.FindAsync(productId);

        public async Task<List<ProductDetails>> ProductListAsync() => await _context.Products.ToListAsync();

        public async Task<bool> AddProductAsync(ProductDetails productDetails)
        {
            await _context.Products.AddAsync(productDetails);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProductAsync(ProductDetails productDetails)
        {
            var product = await _context.Products.FindAsync(productDetails.Id);
            if (product == null)
            {
                return false;
            }
            _context.Products.Update(product);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return true;
            }
            return false;
        }
    }
}
