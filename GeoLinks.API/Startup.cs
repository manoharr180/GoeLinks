using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GeoLinks.DataLayer;
using GeoLinks.DataLayer.DalImplementation;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Services.Implementations;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace GeoLinks.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //Data Access Layer
            services.AddTransient<IProfileDal, ProfilesDal>();
            services.AddTransient<IAuthDal, AuthDal>();
            services.AddTransient<IFriendDal, FriendDal>();
            services.AddTransient<IHobbiesDal, HobbiesDal>();
            services.AddTransient<IInterestsDal, InterestsDal>();

            //Service layer
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IFriendService, FriendService>();
            services.AddTransient<IInterestsService, InterestsService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Startup));
            //Should be in beginning
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuers = Configuration["Jwt:Issuer"].Split(','),
                        ValidAudiences = Configuration["Jwt:Issuer"].Split(','),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]))
                    };
                });


            services.AddCors(
            options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                    .WithMethods("GET,POST,PUT,DELETE,OPTIONS")
                    .AllowAnyHeader();
                    //policy.WithMethods("GET,POST,PUT,DELETE,OPTIONS");

                });
                options.AddPolicy("EnableCors",
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .WithMethods("GET, POST, PUT, DELETE, OPTIONS")
                    .AllowAnyHeader();
                });
            }
            );

            services.AddMvc(routes =>
            {
                routes.EnableEndpointRouting = true;
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


            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseAuthentication();
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod()
                            .AllowAnyOrigin());


        }
    }
}
