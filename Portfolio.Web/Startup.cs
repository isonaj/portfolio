using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Data;
using Portfolio.Data.Repositories;
using Portfolio.Application.Data;
using MediatR;
using System.Reflection;
using Portfolio.Application.Portfolio;

namespace Portfolio.Web
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
            services.AddMvc();
            /*
            services.AddDbContext<PortfolioDbContext>(options => {
                options.UseSqlServer(Configuration["ConnectionString"], sqlServerOptions => 
                    sqlServerOptions.MigrationsAssembly(typeof(PortfolioDbContext).Assembly.Location));
                });
            */
            services.AddDbContext<PortfolioDbContext>(options => {
                options.UseSqlite(Configuration["SqliteDbConnection"], sqliteOptions =>
                    sqliteOptions.MigrationsAssembly(typeof(PortfolioDbContext).Assembly.Location));
            });

            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMediatR(typeof(GetPortfolios).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(routes =>
            {
                routes.MapDefaultControllerRoute();
            });
        }
    }
}
