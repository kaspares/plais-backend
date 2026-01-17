using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Plais.Common;
using Plais.Data.Interfaces;
using Plais.DTOs.Bulletin;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services;
using Plais.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Plais.Services.Tests
{
    public class BulletinServicesTests
    {
        private readonly Mock<IBulletinRepository> _repoMock;
        private readonly BulletinServices _service;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<BulletinServices>> _loggerMock;

        public BulletinServicesTests()
        {
            _repoMock = new Mock<IBulletinRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<BulletinServices>>();
            _service = new BulletinServices(_loggerMock.Object, _repoMock.Object, _mapperMock.Object);
        }

        private static Bulletin CreateEntity(int id = 1) => new Bulletin
        {
            Id = id,
            Title = "Hello",
            Content = "World"
        };

        private static SaveBulletinDto CreateSaveDto() => new SaveBulletinDto
        {
            Title = "New title",
            Content = "New content"
        };

        private static EditBulletinDto CreateEditDto() => new EditBulletinDto
        {
            Title = "Edited title",
            Content = "Edited content",
            PhotoFileNames = new List<string> { "a.jpg", "b.jpg" }
        };

        [Fact]
        public async Task GetByIdAsync_WhenExists_ShouldReturnEntity()
        {
            var entity = CreateEntity(1);
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

            
            var result = await _service.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Title.Should().Be("Hello");
        }

        [Fact]
        public async Task GetByIdAsync_WhenNotExists_ShouldThrowNotFoundException()
        {
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Bulletin?)null);

            var act = async () => await _service.GetByIdAsync(99);

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_WhenNotExists_ShouldThrowNotFoundException()
        {
            _repoMock.Setup(r => r.GetByIdAsync(123)).ReturnsAsync((Bulletin?)null);

            var act = async () => await _service.DeleteAsync(123);

            await act.Should().ThrowAsync<NotFoundException>();
            _repoMock.Verify(r => r.DeleteAsync(It.IsAny<Bulletin>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_WhenExists_ShouldDelete()
        {
            var entity = CreateEntity(7);
            _repoMock.Setup(r => r.GetByIdAsync(7)).ReturnsAsync(entity);

            await _service.DeleteAsync(7);

            _repoMock.Verify(r => r.DeleteAsync(entity), Times.Once);
        }

        [Fact]
        public async Task GetFourLatestAsync_WhenCalled_ShouldReturnSummaryDtos()
        {
            var entities = new List<Bulletin>
            {
                CreateEntity(1), CreateEntity(2), CreateEntity(3), CreateEntity(4)
            };

            var mappedDtos = new List<BulletinSummaryDto>
            {
                new BulletinSummaryDto(), new BulletinSummaryDto(), new BulletinSummaryDto(), new BulletinSummaryDto()
            };

            _repoMock.Setup(r => r.GetFourLatestAsync()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<List<BulletinSummaryDto>>(entities)).Returns(mappedDtos);

            var result = await _service.GetFourLatestAsync();

            result.Should().BeSameAs(mappedDtos);
            _repoMock.Verify(r => r.GetFourLatestAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<List<BulletinSummaryDto>>(entities), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotExists_ShouldThrowNotFoundException()
        {
            _repoMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync((Bulletin?)null);

            var act = async () => await _service.UpdateAsync(5, CreateEditDto());

            await act.Should().ThrowAsync<NotFoundException>();
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Bulletin>(), It.IsAny<List<string>>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenExists_ShouldMapAndCallUpdateWithPhotoFileNames()
        {
            var entity = CreateEntity(5);
            var dto = CreateEditDto();

            _repoMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(entity);

            await _service.UpdateAsync(5, dto);

            _mapperMock.Verify(m => m.Map(dto, entity), Times.Once);
            _repoMock.Verify(r => r.UpdateAsync(entity, dto.PhotoFileNames), Times.Once);
        }

    }
}