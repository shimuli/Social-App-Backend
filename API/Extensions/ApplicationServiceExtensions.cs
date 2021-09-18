using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo
               {
                   Title = "Socail App Api",
                   Version = "v1",
                   Description = "Social App API",
                   Contact = new OpenApiContact()
                   {
                       Email = "shimulicedric@gmail.com",
                       Name = "Cedric Shimuli",
                       Url = new Uri("https://github.com/shimuli")
                   },
                   License = new Microsoft.OpenApi.Models.OpenApiLicense()
                   {
                       Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                   }


               });

               c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
               {
                   Name = "Authorization",
                   Type = SecuritySchemeType.ApiKey,
                   Scheme = "Bearer",
                   BearerFormat = "JWT",
                   In = ParameterLocation.Header,
                   Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjZWRyaWMiLCJuYmYiOjE2MzE5NTY5NzcsImV4cCI6MTYzMzE2NjU3NywiaWF0IjoxNjMxOTU2OTc3fQ.xRdk8hDA1JjbglnnIkDiC0zSovl3-Y2RF0opSEIUxPdh-qtNJFaUr88qmIhBNrbOsGKQ3LZEpMVOW1oS3NEoFQ\"",
               });
               c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }

               });


           });

            return services;
        }
    }
}