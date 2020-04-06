using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AllLogin.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using  Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace AllLogin
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
        {//fHLYZakqBCkbIzks5_lb53H9
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAuthentication()
       .AddGoogle(options =>
       {
           IConfigurationSection googleAuthNSection =
               Configuration.GetSection("Authentication:Google");

           options.ClientId = "968405537304-vg6d2tma1i3qoj5m90s912pj3skpt4b5.apps.googleusercontent.com";//"240556908069-8ql9c1ja3ocp4eamn4pg8ejgaplt56rc.apps.googleusercontent.com";
           options.ClientSecret = "_BkNtsnKDIFp8FbDDaGVlvIr";//"fHLYZakqBCkbIzks5_lb53H9";
       });
            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = "MrZDApD0dBmOkxW68ie1HXxYI";
                twitterOptions.ConsumerSecret = "KBidP63FhwKBGNel354KhpFvQUT50BDsb0G5QRPes1TYbwdjVJ";
                twitterOptions.RetrieveUserDetails = true;
            });

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "249342389438478";
                facebookOptions.AppSecret = "07257c3f2358d55b5d9a82c6df173835";
            });
            services.AddAuthentication().AddLinkedIn(linkedInOptions =>
            {
                linkedInOptions.ClientId = "81fqirprsin3i6";
                linkedInOptions.ClientSecret = "em1BlJElTaPxky6h";
            });
            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = "dd3984b3-c5e9-4869-96c5-f33dff88a373";
                microsoftOptions.ClientSecret = "BObuc_Jtuus6Eg.swKK8_44uphMAC/hJ";
            });


            services.AddAuthentication().AddGitHub(gitOptions =>
            {
                gitOptions.ClientId = "c4eb5e2a565f7706b0b9";
                gitOptions.ClientSecret = "44bf29549fe2954dd982d04b718b2205b0a073bd";
              //  gitOptions.CallbackPath = new PathString("/signin-github");
                //   gitOptions.AuthorizationEndpoint = "";



            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
          //  app.UseOAuthAuthentication(GihubOption);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
