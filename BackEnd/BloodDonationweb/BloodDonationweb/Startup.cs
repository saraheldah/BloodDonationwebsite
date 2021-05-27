using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using BloodDonation.Business.Mapping;
using BloodDonation.DataAccess;
using BloodDonation.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BloodDonationweb
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
            services.AddAutoMapper(c => c.AddProfile<MappingProfile>(), typeof(Startup));
            var connectionString = Configuration.GetConnectionString("DefaultConnection");//retrieving the connection string and saving the default connection in a variable
            services.AddTransient<IUnitOfWork>(x => new UnitOfWork(connectionString));//we are passing the database we want to use (I want to make sure that this connection string is used when ever we use the IUniOfWork)
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IBloodTypeManager, BloodTypeManager>();
            services.AddTransient<ICityManager, CityManager>();
            services.AddTransient<IBloodRequestManager, BloodRequestManager>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                //this lambda determines weather user cosent for non-essential cookie is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}