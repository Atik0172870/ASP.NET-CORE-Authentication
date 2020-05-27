using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeConnection")));
            services.AddMvc(option => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                option.Filters.Add(new AuthorizeFilter(policy));

            }).AddXmlSerializerFormatters();

            //change default accessdenied
            services.ConfigureApplicationCookie(option =>
            {
                option.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("DeleteRolePolicy",
                policy => policy.RequireClaim("Delete Role"));

                // user alternative policy
                option.AddPolicy("EditRolePolicy", policy =>

                 policy.RequireAssertion(context =>
                 context.User.IsInRole("Admin") &&
                 context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                 context.User.IsInRole("Supper Admin")
                 
                 ));

              //  option.AddPolicy("EditRolePolicy",
              //policy => policy.RequireClaim("Edit Role","true"));

            });

            services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<AppDbContext>();
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            //DefaultFilesOptions defaultFileOptions = new DefaultFilesOptions();
            //defaultFileOptions.DefaultFileNames.Clear();
            //defaultFileOptions.DefaultFileNames.Add("foo.html");
            //app.UseDefaultFiles(defaultFileOptions);
            app.UseStaticFiles();
            app.UseAuthentication(); 
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{Controller=Home}/{Action=Index}/{id?}");
            });


            //app.Run(async (context) =>
            //{

            //    await context.Response.WriteAsync("Error !"+"MW2: here msg from MW2");
            //});
        }
    }
}
