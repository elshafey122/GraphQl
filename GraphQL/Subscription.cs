using GraphQlDemo.Models;

namespace GraphQlDemo.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public ItemList OnListAdded([EventMessage] ItemList itemList) 
        {
            return itemList;
        }
    }
}
