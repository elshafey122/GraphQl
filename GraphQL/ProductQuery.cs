using GraphQlDemo.Models;
using GraphQlDemo.Services;
using HotChocolate;

namespace GraphQlDemo.GraphQL
{
    public class ProductQuery
    {
        public async Task<List<ProductDetails>> GetProductDetails([Service] IProductService productService)
        {
            return await productService.ProductListAsync();
        }

        public async Task<ProductDetails> GetProductDetailsByIdAsync([Service] IProductService productService, Guid guid)
        {
            return await productService.GetProductDetailByIdAsync(guid);
        }
    }
}
