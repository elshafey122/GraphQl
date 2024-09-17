using GraphQL.Server.Ui.Voyager;
using GraphQlDemo.Context;
using GraphQlDemo.GraphQL;
using GraphQlDemo.GraphQL.DataTypes;
using GraphQlDemo.SeedData;
using GraphQlDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace GraphQlDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddDbContext<DbContextClass>(options => options.UseInMemoryDatabase("GraphQLDemo"));  // inmemory database that not need use any extrenal db
            //note:  we use AddPooledDbContextFactory ratherthan adddbcontext to allow ef to run more than query on parallel that each query has a single thread and then all return to the main pool thread used



            builder.Services.AddGraphQLServer()                                 // service that execute graphql services like query and mutitons
                            .AddQueryType<Query>()
                            .AddMutationType<Mutation>()
                            //.AddQueryType<ProductQuery>()
                            //.AddMutationType<ProductMutations>()
                            .AddProjections()                                   //to add refernce data in model that has  like itemlist has collection of itemdata i need to get item data inside so it help me in this
                            //.AddQueryType<ListType>()                         // to make custom modification in model of graphql
                            .AddFiltering()                                     // to make filtering in data
                            .AddSorting()                                       // to make sorting in data
                            .AddSubscriptionType<Subscription>()
                            .AddInMemorySubscriptions();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // allow cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();



            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DbContextClass>();
                SeedProducts.InitilizeData(services);
            }

            app.UseWebSockets();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();       // allow request to handle graphql 
            });

            app.UseGraphQLVoyager();          // ui to handle all graphql endpoints

            app.MapControllers();

            app.Run();
        }
    }
}
