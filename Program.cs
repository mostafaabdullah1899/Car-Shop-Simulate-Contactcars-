using Grade_Project_.Models;
using Grade_Project_.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Grade_Project_
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            


            builder.Services.AddCors(corsOptions => {
                corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
                {
                    corsPolicyBuilder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });
            //connection string
            string Connection = builder.Configuration.GetConnectionString("cars_entity");
            builder.Services.AddDbContext<Cars_Entity>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(Connection);
            });
            
            builder.Services.AddIdentity<Users, Roles>(
                options => options.Password.RequireDigit = true
                ).
                AddEntityFrameworkStores<Cars_Entity>();
           
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                    (builder.Configuration["JWT:Secret"]))
                };
            }
            );

            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            //Registeration
            builder.Services.AddScoped<ICar, Car_Repository>();
            builder.Services.AddScoped<IUsers,UsersRepository>();
            builder.Services.AddScoped<ICar_Brand, Car_BrandRepository>();
            builder.Services.AddScoped<ICar_Model, Car_ModelRepository>();
            builder.Services.AddScoped<ICar_Images, Car_ImagesRepositpory>();
            builder.Services.AddScoped<IReports, Report_Repository>();

            builder.Services.AddSwaggerGen(swagger =>
            {
            //ThisistogeneratetheDefaultUIofSwaggerDocumentation
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                            Version = "v1",
                    Title = "ASP.NET5WebAPI",
                    Description = " ITI Projrcy"
            });
                        //ToEnableauthorizationusingSwagger(JWT)
                        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Enter'Bearer'[space]andthenyourvalidtokeninthetextinputbelow.\r\n\r\nExample:\"BearereyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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
                            new string[]{ }
                }
            });
        });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.MapControllers();

            app.Run();
        }
    }
}