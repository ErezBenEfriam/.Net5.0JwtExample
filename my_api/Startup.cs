using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using my_api.DBContext;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Text;
using my_api.Repository;

namespace my_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSingleton<IJWTAuthenticationManager, JWTAuthenticationManager>();
            services.AddScoped<IRepository, Repository.Repository>();
            services.AddDbContext<ApllicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            var tokenKey = Configuration.GetValue<string>("TokenKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, //check the website that create to token
                    ValidateAudience = false //check the user that got to token
                };
            });


            //create Policy: we can create policy from one or more ROLES: policy.RequireRole("admin","Role1","Role2"....)
            services.AddAuthorization(options =>
                  options.AddPolicy("adminPolicy", policy => policy.RequireRole("admin"))
               );
            // we can create policy from one or more CLAIMS:
            services.AddAuthorization(options =>
               options.AddPolicy("PolicyWithTwoClaims", policy => policy.RequireClaim("firstClaim","secondClaim"))
            );
            //for create role we just need to put on top of the function   [Authorize(Roles = "admin")]
            //no need to declare anything on startup.cs


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "my_api", Version = "v1" });
            });

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApllicationDbContext dbContext)
        {

            dbContext.Database.EnsureCreated();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "my_api v1"));
            }


            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true) // allow any origin
              .AllowCredentials()); // allow credentials
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
