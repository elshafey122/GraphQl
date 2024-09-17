using GraphQlDemo.Context;
using GraphQlDemo.Models;

namespace GraphQlDemo.GraphQL
{
    public class Query
    {
        //[UseDbContext(Type(DbContextClass)]
        [UseProjection] // it allow to get navigate properity data that inside model  
        [UseSorting]
        [UseFiltering]
        public IQueryable<ItemData> GetItem([Service] DbContextClass context)
        {
            return context.Items;
        }

        [UseProjection]
        [UseSorting]
        [UseFiltering]
        public IQueryable<ItemList> GetList([Service] DbContextClass context)
        {
            return context.Lists;
        }
    }
}
