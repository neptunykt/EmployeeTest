using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Core.AutoMapper;
using Core.DAL;
using Core.Model;
using Core.Services;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApp.Infrastructure;

namespace WebApp
{
    public class Startup
    {
        public Startup(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _hostingEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var databaseOption = Configuration.GetSection("DatabaseEngine").Get<DatabaseEngineOption>();
            services.AddControllers();
            services.AddDbContext<DataContext>(options =>
            {
                switch (databaseOption.Option)
                {
                    case DatabaseEngine.Sql:
                        options.UseSqlServer(Configuration.GetConnectionString("AppDbContext"));
                        break;
                    case DatabaseEngine.InMemory:
                        options.UseInMemoryDatabase("Employee");
                        break;
                }

            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "An ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense {Name = "Proprietary"}
                });
                options.EnableAnnotations();
            });
            services.AddAutoMapper(typeof(EmployeeProfile));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ILoadService, UploadCsvService>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddFluentValidation(AppDomain.CurrentDomain.GetAssemblies().Where(p=>!p.IsDynamic));
            services.AddScoped<ApiExceptionFilterAttribute>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new List<CultureInfo> { new CultureInfo("en-US")};
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseSwagger();
            app.UseSwaggerUI(
                c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Version 1"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}