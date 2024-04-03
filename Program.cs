
using API_FinalTask.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_FinalTask.Repositories;

namespace API_FinalTask
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

            builder.Services.AddDbContext<StudentContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<StudentContext>();

            builder.Services.AddScoped<IStudentRepo, StudentServices>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentServices>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true; //Refresh Token
                opt.RequireHttpsMetadata = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidateAssure"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidateAudiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

            builder.Services.AddCors(options =>
              options.AddPolicy("HTMLPolicy", corsPolicy =>
              {
                  corsPolicy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
              }

            ));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication(); //check JWT Token
            app.UseAuthorization();
            app.UseCors("HTMLPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}
