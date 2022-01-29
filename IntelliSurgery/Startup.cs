using IntelliSurgery.DbOperations;
using IntelliSurgery.Global;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using React.AspNet;
using JavaScriptEngineSwitcher.V8;
using System;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DbOperations.WorkingBlocks;

namespace IntelliSurgery
{
    public class Startup
    {
        private IConfiguration _config;
        private String connectionString;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
            connectionString = _config.GetConnectionString("DBConnection");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddControllersWithViews();

            services.AddDbContextPool<AppDbContext>(
               options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            //add repository
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<ISurgeryTypeRepository, SurgeryTypeRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<ISurgeonRepository, SurgeonRepository>();
            services.AddScoped<ISurgeryRepository, SurgeryRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ITheatreRepository, TheatreRepository>();
            services.AddScoped<IWorkingBlockRepository, WorkingBLockRepository>();
            services.AddScoped<ISpecialityRepository, SpecialityRepository>();


            services.AddScoped<ISurgeryScheduler, SurgeryScheduler>();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Appointment}/{action=Index}/{id?}");
            });
        }
    }
}
