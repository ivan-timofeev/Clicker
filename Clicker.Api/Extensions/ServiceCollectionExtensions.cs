using Clicker.Api.Graphql.Queries;
using Clicker.Api.Middlewares;
using Clicker.Api.Services;
using Clicker.Api.SwaggerExamples;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Clicker.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Clicker API", Version = "v1" });
            swagger.ExampleFilters();
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer <JWT-TOKEN>'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });

        serviceCollection.AddSwaggerExamplesFromAssemblyOf<CreateJwtRequestExample>();

        return serviceCollection;
    }

    public static IServiceCollection AddOAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = configuration["GoogleAuth:ClientId"]
                    ?? throw new InvalidOperationException("Secrets must contain GoogleAuth:ClientId.");
                googleOptions.ClientSecret = configuration["GoogleAuth:ClientSecret"]
                    ?? throw new InvalidOperationException("Secrets must contain GoogleAuth:ClientSecret.");
                googleOptions.CallbackPath = "/signin-google";
                googleOptions.Scope.Add("email");
                googleOptions.Scope.Add("profile");
            });

        return serviceCollection;
    }

    public static IServiceCollection AddPermissionBasedAuthorization(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<PermissionBasedAuthMiddleware>();
        serviceCollection.AddTransient<JwtService>();

        return serviceCollection;
    }

    public static IServiceCollection AddGraphQl(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddGraphQLServer()
            .AddQueryType<FindUsersQuery>()
            .AddFiltering()
            .AddSorting();

        return serviceCollection;
    }
}
