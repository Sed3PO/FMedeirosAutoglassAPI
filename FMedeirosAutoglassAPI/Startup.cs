using FMedeirosAutoglassAPI.Application.Interface;
using FMedeirosAutoglassAPI.Application.Mapper;
using FMedeirosAutoglassAPI.Application.Service;
using FMedeirosAutoglassAPI.Controllers;
using FMedeirosAutoglassAPI.Domain.Core.Interface.Repository;
using FMedeirosAutoglassAPI.Domain.Core.Interface.Service;
using FMedeirosAutoglassAPI.Domain.Entity;
using FMedeirosAutoglassAPI.Domain.Services.Service;
using FMedeirosAutoglassAPI.Infrastructure.Data;
using FMedeirosAutoglassAPI.Infrastructure.Data.Repositorys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FMedeirosAutoglassAPI
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
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddTransient<AppSettings>();
            services.AddTransient<AuthController>();
            services.AddScoped<IApplicationAuth, ApplicationServiceAuth>();

            services.AddTransient<ProductController>();
            services.AddScoped<IApplicationProduct, ApplicationServiceProduct>();
            services.AddScoped<IServiceProduct, ServiceProduct>();
            services.AddScoped<IRepositoryProduct, RepositoryProduct>();

            services.AddTransient<SqlContext>();

            services.AddDbContext<SqlContext>(options =>
            {
                options.UseSqlServer(appSettings.ConnectionString);
            });

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(ConfigurationMapping));

            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FMedeirosAutoglassAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Favor Inserir seu token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "FMedeirosAutoglassAPI v1"));
        }
    }
}