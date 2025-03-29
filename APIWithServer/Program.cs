
using day1.Models;
using day1.Reprository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace day1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ITIContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            builder.Services.AddScoped<IGenerateCategory, GenerateCategory>();
            builder.Services.AddScoped<IGenerateProduct, GenericProduct>();
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ITIContext>();
            

            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata=false;
                options.TokenValidationParameters = new()
                {

                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Iss"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Aud"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };

            });
            builder.Services.AddEndpointsApiExplorer();
            #region Swagget Setting
            //   builder.Services.AddSwaggerGen();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = "ITI Project"
                });

                // Enable JWT Authentication in Swagger
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token below.\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
            });
            #endregion



            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowAllOrigins", policy => {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAllOrigins");
            app.UseStaticFiles();//wwwroot
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
