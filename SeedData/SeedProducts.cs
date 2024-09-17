using GraphQlDemo.Context;
using GraphQlDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlDemo.SeedData
{
    public class SeedProducts
    {
        public static void InitilizeData(IServiceProvider serviceProvider)
        {
            using (var context = new DbContextClass(serviceProvider.GetRequiredService<DbContextOptions<DbContextClass>>()))
            {
                if (context.Items.Any())
                {
                    return;
                }

                context.Items.AddRange(
                new ItemData
                {
                    Id = 1,
                    Title = "todo1",
                    Description = "it is a todo1",
                    Done = true,
                    ListId=1
                },
                new ItemData
                {
                    Id = 2,
                    Title = "todo2",
                    Description = "it is a todo2",
                    Done = true,
                    ListId=2
                });

                context.Lists.AddRange(
                new ItemList
                {
                    Id = 1,
                    Name = "first"
                },
                new ItemList
                {
                    Id = 2,
                    Name = "second"
                });

                context.SaveChanges();
            }
        }
    }
}
