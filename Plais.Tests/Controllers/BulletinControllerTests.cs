using FluentAssertions;
using Plais.Tests;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Plais.Common;
using Plais.DTOs.Bulletin;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plais.Services;



namespace Plais.Tests.Controllers
{
    public class BulletinControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factoryAuthorized;
        private readonly WebApplicationFactory<Program> _factoryUnauthorized;
        private readonly Mock<IBulletinServices> _bulletinServicesMock = new();

        public BulletinControllerTests(WebApplicationFactory<Program> factory)
        {
            _factoryUnauthorized = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(IBulletinServices));
                    services.AddScoped(_ => _bulletinServicesMock.Object);
                });
            });

            _factoryAuthorized = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    services.RemoveAll(typeof(IBulletinServices));
                    services.AddScoped(_ => _bulletinServicesMock.Object);
                });
            });
        }

        [Fact]
        public async Task GetAll_ValidQuery_Returns200Ok()
        {
            var query = new ResourceQuery { PageNumber = 1, PageSize = 10, SearchPhrase = null };

            _bulletinServicesMock
                .Setup(s => s.GetAllAsync(It.IsAny<ResourceQuery>()))
                .ReturnsAsync(new PagedResult<Bulletin>(
                    items: new List<Bulletin>(),
                    totalCount: 0,
                    pageSize: query.PageSize,
                    pageNumber: query.PageNumber));

            var client = _factoryUnauthorized.CreateClient();

            var response = await client.GetAsync("/api/bulletin?pageNumber=1&pageSize=10");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_InvalidQuery_Returns400BadRequest()
        {
            var client = _factoryUnauthorized.CreateClient();

            var response = await client.GetAsync("/api/bulletin?pageNumber=abc&pageSize=10");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetById_ExistingId_Returns200Ok()
        {
            var id = 1;
            var bulletin = new Bulletin
            {
                Id = id,
                Title = "Test",
                Content = "Test content"
            };

            _bulletinServicesMock
                .Setup(s => s.GetByIdAsync(id))
                .ReturnsAsync(bulletin);

            var client = _factoryUnauthorized.CreateClient();

            var response = await client.GetAsync($"/api/bulletin/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await response.Content.ReadFromJsonAsync<Bulletin>();
            body.Should().NotBeNull();
            body!.Id.Should().Be(id);
            body.Title.Should().Be("Test");
            body.Content.Should().Be("Test content");
        }

        [Fact]
        public async Task GetById_NonExistingId_Returns404NotFound()
        {
            var id = 999;

            _bulletinServicesMock
                .Setup(s => s.GetByIdAsync(id))
                .ThrowsAsync(new NotFoundException(nameof(Bulletin), id.ToString()));

            var client = _factoryUnauthorized.CreateClient();

            var response = await client.GetAsync($"/api/bulletin/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetFourLatest_ValidRequest_Returns200Ok()
        {
            _bulletinServicesMock
                .Setup(s => s.GetFourLatestAsync())
                .ReturnsAsync(new List<BulletinSummaryDto> { new(), new() });

            var client = _factoryUnauthorized.CreateClient();

            var response = await client.GetAsync("/api/bulletin/latestFour");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_Authenticated_Returns201Created()
        {
            var dto = new SaveBulletinDto { Title = "New", Content = "Content" };
            var created = new Bulletin { Id = 123, Title = "New", Content = "Content" };

            _bulletinServicesMock
                .Setup(s => s.CreateAsync(It.IsAny<SaveBulletinDto>()))
                .ReturnsAsync(created);

            var client = _factoryAuthorized.CreateClient();

            var response = await client.PostAsJsonAsync("/api/bulletin", dto);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_Unauthenticated_Returns401Unauthorized()
        {
            var client = _factoryUnauthorized.CreateClient();
            var dto = new SaveBulletinDto { Title = "New", Content = "Content" };

            var response = await client.PostAsJsonAsync("/api/bulletin", dto);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Update_Authenticated_Returns204NoContent()
        {
            var id = 10;
            var dto = new EditBulletinDto { Title = "Edited", Content = "Edited content", PhotoFileNames = new List<string>() };

            _bulletinServicesMock
                .Setup(s => s.UpdateAsync(id, It.IsAny<EditBulletinDto>()))
                .Returns(Task.CompletedTask);

            var client = _factoryAuthorized.CreateClient();

            var response = await client.PutAsJsonAsync($"/api/bulletin/{id}", dto);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_Unauthenticated_Returns401Unauthorized()
        {
            var client = _factoryUnauthorized.CreateClient();
            var dto = new EditBulletinDto { Title = "Edited", Content = "Edited content", PhotoFileNames = new List<string>() };

            var response = await client.PutAsJsonAsync("/api/bulletin/1", dto);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Delete_Authenticated_Returns204NoContent()
        {
            var id = 10;

            _bulletinServicesMock
                .Setup(s => s.DeleteAsync(id))
                .Returns(Task.CompletedTask);

            var client = _factoryAuthorized.CreateClient();

            var response = await client.DeleteAsync($"/api/bulletin/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Unauthenticated_Returns401Unauthorized()
        {
            var client = _factoryUnauthorized.CreateClient();

            // act
            var response = await client.DeleteAsync("/api/bulletin/1");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

    }
}
