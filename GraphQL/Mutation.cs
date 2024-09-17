using GraphQlDemo.Context;
using GraphQlDemo.Dto;
using GraphQlDemo.Models;
using HotChocolate.Execution.Processing;
using HotChocolate.Subscriptions;

namespace GraphQlDemo.GraphQL
{
    public class Mutation
    {
        public async Task<AddListPayload> AddListAsync(AddListItem addListItem , [Service] DbContextClass context,
            [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            var list = new ItemList
            {
                Name = addListItem.name
            };
            await context.Lists.AddAsync(list);
            await context.SaveChangesAsync();

            await eventSender.SendAsync(nameof(Subscription.OnListAdded), list, cancellationToken);
            return new AddListPayload(list);
        }

        public async Task<AddItemPayload> AddItemAsync(AddItemData addItemData, [Service] DbContextClass context)
        {
            var item = new ItemData
            {
                Title = addItemData.title,
                Description = addItemData.description,
                Done = addItemData.done,
                ListId = addItemData.listId,
            };
            await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
            return new AddItemPayload(item);
        }
    }
}
