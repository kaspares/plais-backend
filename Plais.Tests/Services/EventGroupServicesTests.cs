using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Plais.Data.Interfaces;
using Plais.DTOs.EventGroup;
using Plais.Models;
using Plais.Services;

namespace Plais.Tests.Services
{
    public class EventGroupServicesTests
    {
        private readonly Mock<ILogger<EventGroupService>> _loggerMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IEventGroupRepository> _repoMock = new();
        private readonly EventGroupService _service;
        public EventGroupServicesTests()
        {
            _service = new EventGroupService(_loggerMock.Object, _mapperMock.Object, _repoMock.Object);
        }

        private static SaveEventGroupDto CreateSaveDto() => new SaveEventGroupDto
        {
            Title = "2022",
            PhotoFileName = "photo.png"
        };

        private static EditEventGroupDto CreateEditDto() => new EditEventGroupDto
        {
            Title = "Group Edited",
            PhotoFileName = "new.png",
            DateCreated = new DateTime(2020, 1, 1)
        };

        private static EventGroupDto CreateMappedDto(int id = 1) => new EventGroupDto
        {
            Id = id,
            Title = "Mapped",
            PhotoFileName = null,
            Position = 0,
            Events = new()
        };

        [Fact]
        public async Task CreateAsync_ValidDto_ReturnsMappedDto()
        {
            var dto = CreateSaveDto();
            var mappedEntity = new EventGroup();
            var mappedDto = CreateMappedDto();

            _mapperMock.Setup(m => m.Map<EventGroup>(dto)).Returns(mappedEntity);
            _mapperMock.Setup(m => m.Map<EventGroupDto>(mappedEntity)).Returns(mappedDto);

            var result = await _service.CreateAsync(dto);

            result.Should().BeSameAs(mappedDto);
            _mapperMock.Verify(m => m.Map<EventGroupDto>(mappedEntity), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ValidDto_SetsPositionToZeroAndDateCreated
()
        {
            var dto = CreateSaveDto();
            var mappedEntity = new EventGroup { Position = 999, DateCreated = default };

            _mapperMock.Setup(m => m.Map<EventGroup>(dto)).Returns(mappedEntity);
            _mapperMock.Setup(m => m.Map<EventGroupDto>(mappedEntity)).Returns(CreateMappedDto());

            await _service.CreateAsync(dto);

            mappedEntity.Position.Should().Be(0);
            mappedEntity.DateCreated.Should().NotBe(default);
        }

        [Fact]
        public async Task CreateAsync_ValidDto_CallsIncrementAddAndSaveChanges()
        {
            var dto = CreateSaveDto();
            var mappedEntity = new EventGroup();

            _mapperMock.Setup(m => m.Map<EventGroup>(dto)).Returns(mappedEntity);
            _mapperMock.Setup(m => m.Map<EventGroupDto>(mappedEntity)).Returns(CreateMappedDto());

            await _service.CreateAsync(dto);

            _repoMock.Verify(r => r.IncrementAllPositionsAsync(), Times.Once);
            _repoMock.Verify(r => r.AddAsync(mappedEntity), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task MoveDownAsync_GroupHasNextGroup_SwapsPositionsAndSaves()
        {
            var group = new EventGroup { Id = 1, Position = 2 };
            var toSwap = new EventGroup { Id = 2, Position = 3 };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(group);
            _repoMock.Setup(r => r.GetByPositionAsync(3)).ReturnsAsync(toSwap);

            await _service.MoveDownAsync(1);

            group.Position.Should().Be(3);
            toSwap.Position.Should().Be(2);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task MoveDownAsync_GroupAtBottom_DoesNothing()
        {
            var group = new EventGroup { Id = 1, Position = 3 };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(group);
            _repoMock.Setup(r => r.GetByPositionAsync(4))
                     .ReturnsAsync((EventGroup?)null);

            await _service.MoveDownAsync(1);

            group.Position.Should().Be(3); 
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }



        [Fact]
        public async Task MoveUpAsync_GroupHasNextGroup_SwapsPositionsAndSaves()
        {
            var group = new EventGroup { Id = 1, Position = 3 };
            var toSwap = new EventGroup { Id = 2, Position = 2 };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(group);
            _repoMock.Setup(r => r.GetByPositionAsync(2)).ReturnsAsync(toSwap);

            await _service.MoveUpAsync(1);

            group.Position.Should().Be(2);
            toSwap.Position.Should().Be(3);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task MoveUpAsync_GroupAtTop_DoesNothing()
        {
            var group = new EventGroup { Id = 1, Position = 0 };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(group);

            await _service.MoveUpAsync(1);

            _repoMock.Verify(r => r.GetByPositionAsync(It.IsAny<int>()), Times.Never);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}
