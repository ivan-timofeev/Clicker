using System.Text.Json.Serialization;
using Clicker.Api.Graphql.Queries;
using Clicker.Api.Middlewares;
using Clicker.Application.Features;
using Clicker.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(AuthenticateRequest).Assembly));

builder
    .Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["GoogleAuth:ClientId"]
            ?? throw new InvalidOperationException("Secrets must contain GoogleAuth:ClientId.");
        googleOptions.ClientSecret = builder.Configuration["GoogleAuth:ClientSecret"]
            ?? throw new InvalidOperationException("Secrets must contain GoogleAuth:ClientSecret.");
        googleOptions.CallbackPath = "/signin-google";
        googleOptions.Scope.Add("email");
        googleOptions.Scope.Add("profile");
    });

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
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

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
