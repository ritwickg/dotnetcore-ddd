using InventoryManagement.Domain.Contracts;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Entities.Enumerations;
using InventoryManagement.Infrastructure.Data;
using InventoryManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace InventoryManagement.API
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
            services.AddDbContext<InventoryDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConfigSettings:ConnectionStrings:InventoryDB"], config =>
                {
                    config.MigrationsAssembly("InventoryManagement.Infrastructure");
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", policies =>
                {
                    policies.AllowAnyOrigin();
                    policies.AllowAnyMethod();
                    policies.AllowAnyHeader();
                });
            });

            services.Configure<Configsettings>(options => Configuration.GetSection("ConfigSettings").Bind(options));

            services.AddIdentity<User, UserRole>()
                    .AddEntityFrameworkStores<InventoryDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(24, 0, 0);
                options.User.RequireUniqueEmail = true;
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.Audience = Configuration["ConfigSettings:Jwt:JwtAudience"];
                config.ClaimsIssuer = Configuration["ConfigSettings:Jwt:JwtIssuer"];
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["ConfigSettings:Jwt:JwtIssuer"],
                    ValidAudience = Configuration["ConfigSettings:Jwt:JwtAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ConfigSettings:Jwt:JwtKey"])),
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    RequireSignedTokens = true
                };
            });
            services.AddTransient(typeof(IRepository<>), typeof(RepositoryService<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAccountManager, AccountService>();
            services.AddScoped<ITokenProvider, TokenService>();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            SeedData(provider);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SeedData(IServiceProvider provider)
        {
            RoleManager<UserRole> _roleManager = provider.GetService<RoleManager<UserRole>>();
            foreach (var item in Enum.GetNames(typeof(Role)))
            {
                if (!_roleManager.RoleExistsAsync(item).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new UserRole { Name = item}).GetAwaiter().GetResult();
                }
            }
            InventoryDbContext dbContext = provider.GetService<InventoryDbContext>();
            int counter = 0;
            foreach (var item in Enum.GetNames(typeof(MembershipType)))
            {
                if (!dbContext.Memberships.AnyAsync(member => member.MembershipName == item).GetAwaiter().GetResult())
                {
                    dbContext.Memberships.Add(new Membership() { MembershipName = item, ConversionRate = 0.25 * counter++});
                    dbContext.SaveChanges();
                }   
            }

        }
    }
}
