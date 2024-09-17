using GraphQlDemo.Context;
using GraphQlDemo.Models;

namespace GraphQlDemo.GraphQL.DataTypes
{
    public class ListType : ObjectType<ItemList>
    {
        protected override void Configure(IObjectTypeDescriptor<ItemList> descriptor)
        {
            descriptor.Description("the object is used as item for itemlist");

            descriptor.Field(x => x.ItemDatas)
                .ResolveWith<Resolvers>(x => x.GetItmes(default!, default!))
                .Description("this is the list that the items has");
        }

        private class Resolvers
        {
            public IQueryable <ItemData> GetItmes(ItemList list , [Service] DbContextClass context)
            {
                return context.Items.Where(x => x.ListId == list.Id);
            }
        }
    }
}
