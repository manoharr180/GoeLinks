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
    public  class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
            // Register HttpClient
            services.AddHttpClient<IExternalApiService, ExternalApiService>();

            // Data Access Layer
            services.AddTransient<IProfileDal, ProfilesDal>();
            services.AddTransient<IAuthDal, AuthDal>();

            // Service layer
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IStoreService, StoreService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            // Authentication
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

            

            // CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            services.AddMvc(routes =>
            {
                routes.EnableEndpointRouting = true;
            });

            // Database context
            services.AddDbContext<GeoLensContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("GeoLensContext"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
