using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Bnd.RestaurantReviews.Api.Infrastructure;
using Bnd.RestaurantReviews.Data;
using System;

namespace Bnd.RestaurantReviews.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bnd Restaurant Reviews API",
                    Description =
                        "Use the Bnd Restaurant Reviews REST API to create, read, update and delete reviews.",
                    TermsOfService = new Uri("http://www.example.com/terms-of-service/"),
                    Contact = new OpenApiContact
                    {
                        Name = "William Napier",
                        Email = "wnapier@example.com",
                        Url = new Uri("https://github.com/williamdnapier"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("http://www.example.com/license/"),
                    }
                });
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<ReviewsDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ReviewsDbConnection")));

            ServicesTypeRegistry.UpdateServiceCollection(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
                });

                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthorization();
                app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            }
        }
    }
}