using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using $safeprojectname$.Infrastructure;
using $ext_safeprojectname$.DAL.Context;
using $ext_safeprojectname$.ServiceLayer.Repositories;
using $ext_safeprojectname$.ServiceLayer.Services;
using $ext_safeprojectname$.DAL.GenericEntity;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using Serilog;

namespace $safeprojectname$
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var secretKey = Configuration.GetSection("AppSettings:SecretKey").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "$projectname$",
                ValidateAudience = true,
                ValidAudience = "$projectname$Aud",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(c =>
            {
                c.TokenValidationParameters = tokenValidationParameters;
                // TODO: make it true in prod
                c.RequireHttpsMetadata = false;
            });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("PolicyName", p =>
                {
                    p.RequireRole("Roles");
                    p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    p.RequireAuthenticatedUser();
                });
            });

            // Registering AppSettings from appsettings.json
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDbContext<EntityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // DbContext
            services.AddScoped<IEntityDbContext, EntityDbContext>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            // Repositories
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
			
			// Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "$ext_safeprojectname$", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder =>
            {
                builder.AllowCredentials().AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });

            var loggerConfig = new LoggerConfiguration()
                .WriteTo.File(Path.Combine(env.WebRootPath, "Logs", "log-.json"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            loggerFactory.AddSerilog(loggerConfig);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			
			// Using authentication
            app.UseAuthentication();
			
			// Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "$ext_safeprojectname$");
            });

            app.UseMvc();
        }
    }
}
