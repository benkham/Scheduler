using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.AspNetCore;
using VicTrains.Application.Schedules.Commands;
using VicTrains.Application.Schedules.Models;
using VicTrains.Application.Schedules.Queries;
using VicTrains.Domain.Model.ScheduleAggregate;
using VicTrains.Persistence;
using VicTrains.Persistence.Repositories;
using VicTrains.WebUI.Filters;

namespace VicTrains.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MediatR and register the handler and notifier assemblies 
            services.AddMediatR(typeof(GetScheduleDetailQueryHandler).Assembly,
                typeof(CreateScheduleCommandHandler).Assembly,
                typeof(DeleteScheduleCommandHandler).Assembly);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<VicTrainsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("VicTrains")));

            services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ScheduleDetailModel>());

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // add dependent types to the services collection
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // configure swagger to use swaggerui
            app.UseSwaggerUi3(settings =>
            {
                settings.SwaggerRoute = "/swagger/swagger.json";
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");

                    // run the angular dev server separate to compile only the backend changes
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
