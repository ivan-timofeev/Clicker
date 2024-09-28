using System.Text.Json.Serialization;
using Clicker.Api.Extensions;
using Clicker.Api.Middlewares;
using Clicker.Application.Features;
using Clicker.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(AuthenticateRequest).Assembly));
builder.Services.AddInMemoryInfrastructure();

builder.Services.AddOAuth(builder.Configuration);
builder.Services.AddPermissionBasedAuthorization();

builder.Services.AddGraphQl();
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddHttpLogging(o => o.CombineLogs = true);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app
    .UseRouting()
    .UseMiddleware<PermissionBasedAuthMiddleware>()
    .UseAuthentication()
    .UseEndpoints(s =>
    {
        // Swagger available at http://localhost:13450/swagger
        s.MapControllers();
        
        // GraphQL playground available at http://localhost:13450/graphql
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

app.Run();
