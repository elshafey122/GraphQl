﻿using GraphQlDemo.Models;

namespace GraphQlDemo.Services
{
    public interface IProductService
    {
        public Task<List<ProductDetails>> ProductListAsync();
        public Task<ProductDetails> GetProductDetailByIdAsync(Guid productId);
        public Task<bool> AddProductAsync(ProductDetails productDetails);
        public Task<bool> UpdateProductAsync(ProductDetails productDetails);
        public Task<bool> DeleteProductAsync(Guid productId);
    }
}
