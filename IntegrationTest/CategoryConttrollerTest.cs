using Application.Categories.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Persistance.Context;
using ShopApi.Controllers;

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
    public class CategoryConttrollerTest : IClassFixture<WebApplicationFactory<CategoryController>>
    {
        private readonly WebApplicationFactory<Shop.Program> _factory;
        private readonly HttpClient _client;

        public CategoryConttrollerTest(WebApplicationFactory<CategoryController> webApplicationFactory)
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
        public async Task CreateCategory_WithValidJwt_ReturnsCreated()
        {
            // Arrange

            var category = new CategoryDto { Name = "category1" };
            var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Category", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

    }

}
