using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ShopApi.Controllers;
using ShopApi.TemporaryForTDD.Context;
using ShopApi.TemporaryForTDD.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntegrationTest
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<ProductController>>
    {
        private readonly WebApplicationFactory<Shop.Program> _factory;
        private readonly HttpClient _client;

        public ProductControllerTests(WebApplicationFactory<ProductController> webApplicationFactory)
        {
            _factory = new WebApplicationFactory<Shop.Program>()
             .WithWebHostBuilder(builder =>
              {
                  builder.ConfigureServices(services =>
                  {
                      // Remove the app's MyDbContext registration.
                      var descriptor = services.SingleOrDefault(
                      d => d.ServiceType ==
                              typeof(DbContextOptions<ShopDbContext>));

                      if (descriptor != null)
                      {
                          services.Remove(descriptor);
                      }

                      // Add MyDbContext using an in-memory database for testing.
                      services.AddDbContext<ShopDbContext>(options =>
                      {
                          options.UseInMemoryDatabase("TestDatabase");
                      });

                      // Build the service provider.
                      var sp = services.BuildServiceProvider();

                      // Create a scope to obtain a reference to the database
                      // context (MyDbContext).
                      using var scope = sp.CreateScope();
                      var scopedServices = scope.ServiceProvider;
                      var db = scopedServices.GetRequiredService<ShopDbContext>();

                      // Ensure the database is created.
                      db.Database.EnsureCreated();

                      // Seed the database with test data.

                  });
              });

            _client = _factory.CreateClient();
        }
    

        [Fact]
        public async Task CreateProduct_WithValidJwt_ReturnsCreated()
        {
            // Arrange

            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyMTIzIiwicm9sZSI6IkFkbWluIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJleHAiOjE2MjYxODgwMDB9.eEcRG0_FN9Axos6OI0yhmG3Mb3lxGH14C6Xa3AzeQa0";
                //GenerateJwtToken("Admin"); 

            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var newProduct = new ProductDto { Name = "Test Product" };
            var content = new StringContent(JsonSerializer.Serialize(newProduct), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Product", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateProduct_WithoutJwt_ReturnsUnauthorized()
        {
            // Arrange
     

            var newProduct = new ProductDto { Name = "Test Product" };
            var content = new StringContent(JsonSerializer.Serialize(newProduct), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsJsonAsync("/api/product", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // We can Add more tests for other ProductController endpoints

        private string GenerateJwtToken( string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "createTimeDontNeed",
                audience: "DonotNeed",
                claims: new[]
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role)
                },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var a = new JwtSecurityTokenHandler().WriteToken(token);
            return a;
        }
    }

}
