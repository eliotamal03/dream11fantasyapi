using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.IRepository;
using CoreLibrary.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLibrary;
using ServicesLibrary;

namespace D11Analytics
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
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromHours(10);
                options.Cookie.HttpOnly = true;
            });
            services.AddMvc();

           
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<IMatchRepository, MatchRepository>();

            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();

            services.AddTransient<IContestService,ContestService>();
            services.AddTransient<IContestRepository, ContestRepository>();

            services.AddTransient<IFantasyService, FantasyService>();
            services.AddTransient<IFantasyRepository, FantasyRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Dashboard}/{id?}");
            });
        }
    }
}
