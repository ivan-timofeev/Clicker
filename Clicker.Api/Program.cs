using System.Text.Json.Serialization;
using Clicker.Api.Graphql.Queries;
using Clicker.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    var jsonStringEnumConverter = new JsonStringEnumConverter();
    jsonOptions.JsonSerializerOptions.Converters.Add(jsonStringEnumConverter);
});
builder
    .Services
    .AddGraphQLServer()
    .AddQueryType<FindUsersQuery>()
    .AddFiltering()
    .AddSorting();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpLogging(o => o.CombineLogs = true);

builder.Services.AddInMemoryInfrastructure();
builder.Services.AddDatabaseSeeder();

var app = builder.Build();

app
    .UseRouting()
    .UseEndpoints(s =>
    {
        // Available at http://localhost:5066/swagger
        s.MapControllers();
        
        // Available at http://localhost:5066/graphql
        s.MapGraphQL();
    });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
}

await app.UseDatabaseSeederAsync();

app.Run();
