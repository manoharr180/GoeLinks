using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeoLinks.DataLayer;
using GeoLinks.DataLayer.DalImplementation;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Services.Implementations;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GeoLinks.Web
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
            services.AddControllers();
            services.AddTransient<IProfileDal, ProfilesDal>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(
            //options =>
            //{
            //    options.AddDefaultPolicy(policy =>
            //    {
            //        policy.WithOrigins("http://localhost:3000")
            //        .WithMethods("GET,POST,PUT,DELETE,OPTIONS")
            //        .AllowAnyHeader();
            //        //policy.WithMethods("GET,POST,PUT,DELETE,OPTIONS");

            //    });
            //    //options.AddPolicy("EnbleCors",
            //    //builder =>
            //    //{
            //    //    builder.WithOrigins("http://localhost:3000");
            //    //    builder.WithMethods("GET, POST, PUT, DELETE, OPTIONS");
            //    //});
            //}
            );

            services.AddMvc(routes =>
            {
                routes.EnableEndpointRouting = false;
            });

            services.AddDbContext<GeoLensContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("GeoLensContext")
                    , builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseMvc();
        }
    }
}
