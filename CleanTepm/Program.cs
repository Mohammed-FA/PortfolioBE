using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using project.Services;
using Project.api.Hubs;
using Project.api.Providers;
using Project.Application.Interfaces;
using Project.Application.Mappings;
using Project.Domain.Entities;
using Project.Infrastructure.Data;
using Project.Infrastructure.Repositories;
using Project.Infrastructure.Services;

namespace CleanTepm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            RegisterApplicationServices(builder.Services);

            builder.Services.AddIdentity<UserModel, IdentityRole<long>>
                (options =>
                        {
                            options.SignIn.RequireConfirmedEmail = true;


                            options.Password.RequireDigit = false;
                            options.Password.RequireLowercase = true;
                            options.Password.RequireNonAlphanumeric = false;
                            options.Password.RequireUppercase = true;
                            options.Password.RequiredLength = 6;
                            options.Password.RequiredUniqueChars = 2;

                            // Default User settings
                            options.User.AllowedUserNameCharacters =
                                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                            options.User.RequireUniqueEmail = true;

                            // Lockout options
                            options.Lockout.AllowedForNewUsers = true;
                            options.Lockout.MaxFailedAccessAttempts = 5;
                            options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0);
                        }).AddDefaultTokenProviders()
                           .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddAuthentication(options =>

            {
                options.DefaultAuthenticateScheme =
                options.DefaultScheme =
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:iss"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:aud"],

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWT:skey"]!)),
                    RoleClaimType = ClaimTypes.Role

                };
            });

            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("publicConnectionString")));


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {

                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication With JWT Bearer",
                    Type = SecuritySchemeType.Http


                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                               Reference=new OpenApiReference
                               {
                                   Id="Bearer",
                                   Type=ReferenceType.SecurityScheme
                               }
                        },
                        new List<string>()
                    }
                });

            });
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            using (var scope = app.Services.CreateScope())
            {
                await SeedData.Initialize(scope.ServiceProvider);
            }

            app.MapHub<ChatHub>("/chatHub");

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();


            app.UseCors("AllowFrontend");



            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            void RegisterApplicationServices(IServiceCollection services)
            {
                services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(Program));

                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                services.AddScoped<CreateWebsitServer>();


                services.AddScoped<EmailSenderService>();



            }

        }


    }
}
