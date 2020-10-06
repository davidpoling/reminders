using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RemindersAPI.Services;
using RemindersDomain;
using Z.EntityFramework.Extensions;
using AutoMapper;
using RemindersAPI.SignalR;

namespace RemindersAPI
{
    public class Startup
    {
        private readonly string _serviceName = "Reminders";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ReminderContext>(options =>
            {
                var optionsBuilder = options.UseMySql(Configuration);
                EntityFrameworkManager.ContextFactory = context =>
                    new ReminderContext(optionsBuilder.Options);
            });

            services.AddCors();
            services.AddOptions();
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IShoppingListService, ShoppingListService>();

            services.AddSignalR();
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = _serviceName,
                    Version = "v1",
                    Description = "A simple reminders application."
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "api-doc.xml");

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ReminderContext reminderContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (reminderContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                reminderContext.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", _serviceName);
            });

            if (Configuration["DevelopmentEnvironment"] != "local")
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseCors(c => 
                c.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); endpoints.MapHub<ApplicationHub>("/application-hub"); });
        }
    }
}
