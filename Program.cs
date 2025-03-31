using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using ScalarAPI.Contract;
using ScalarAPI.Helper;
using ScalarAPI.Model;

namespace ScalarAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration setup
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                 .AddEnvironmentVariables();

            IConfiguration configuration = builder.Configuration;

            // IOptionsSnapshot Implementation
            builder.Services.AddOptions()
                            .Configure<AppConfiguration>(configuration)
                            .AddTransient<IConfiguration>(item => configuration);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Basic BasicAuthentication
            builder.Services.AddAuthentication("BasicAuthentication")
                        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            // API versioning and Swagger setup
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
            });

            builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
            builder.Services.AddMemoryCache();

            builder.Services.AddSwaggerGen(opt =>
            {
                // Add Basic Authentication definition
                opt.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Description = "Basic Authentication header using the Basic scheme. Example: \"Authorization: Basic {base64(username:password)}\""
                });

                // Add Security Requirement
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Basic"
                            }
                        },
                        new string[] {}
                    }
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger(opt =>
                {
                    opt.RouteTemplate = "openapi/{documentName}.json";
                });
                app.MapScalarApiReference(opt =>
                {
  
                    opt.Title = "Scalar .NET CORE API EXAMPLE";
                    opt.Theme = ScalarTheme.Purple;
                    opt.ShowSidebar = true;
                    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
                });
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
