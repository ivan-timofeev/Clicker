using System.Text.Json.Serialization;
using Clicker.Api.SwaggerExamples;
using Clicker.Api.Graphql.Queries;
using Clicker.Api.Middlewares;
using Clicker.Api.Services;
using Clicker.Application.Features;
using Clicker.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clicker API", Version = "v1" });
    c.ExampleFilters();
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer <JWT-TOKEN>'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                } 
            }, 
            new string[] {} 
        }
    });
});
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
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<PermissionBasedAuthMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpLogging(o => o.CombineLogs = true);

builder.Services.AddInMemoryInfrastructure();
builder.Services.AddDatabaseSeeder();
builder.Services.AddTransient<JwtService>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateJwtRequestExample>();

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

await app.UseDatabaseSeederAsync();

app.Run();
