using IntelliSurgery.DbOperations;
using IntelliSurgery.Global;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DbOperations.WorkingBlocks;
using Microsoft.AspNetCore.Identity;
using IntelliSurgery.Logic;

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

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            //add repository
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<ISurgeryTypeRepository, SurgeryTypeRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<ISurgeonRepository, SurgeonRepository>();
            services.AddScoped<ISurgeryRepository, SurgeryRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ITheatreRepository, TheatreRepository>();
            services.AddScoped<IWorkingBlockRepository, WorkingBlockRepository>();
            services.AddScoped<ISpecialityRepository, SpecialityRepository>();

            //add logic
            services.AddScoped<ISurgeryScheduler, SurgeryScheduler>();
            services.AddScoped<IWorkingBlockLogic, WorkingBlockLogic>();
            services.AddScoped<IAppointmentLogic, AppointmentLogic>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
