using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShopApi;
using Application;
using Persistance;
using Persistance.Context;
namespace Shop;
public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddApplicationLayer();
        builder.Services.AddPersistenceLayer();

        // Configure DbContext with SQL Server
        builder.Services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Bind JWT settings from appsettings.json
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        builder.Services.AddSingleton(jwtSettings);

        // Configure JWT authentication
        var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(options =>
        //{
        //    options.RequireHttpsMetadata = false;
        //    options.SaveToken = true;
        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = false,
        //      //  IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateIssuer = false,
        //        ValidateAudience = false
        //    };
        //});
      
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
       // app.UseAuthentication();
        app.UseAuthorization();

        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}
