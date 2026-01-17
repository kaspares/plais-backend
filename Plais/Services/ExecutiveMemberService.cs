using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.DTOs.ExecutiveMember;
using Plais.Exceptions;
using Plais.Services.Interfaces;
using PLAIS.API.Data;
using PLAIS.API.Models;

namespace Plais.Services
{
    public class ExecutiveMemberService(ILogger<ExecutiveMemberService> logger
        ,IExecutiveMemberRepository executiveMemberRepository,
        IMapper mapper,
        IImageService imageService) : IExecutiveMemberService
    {
        public async Task<ExecutiveMemberDto> CreateAsync(SaveExecutiveMemberDto dto)
        {
            logger.LogInformation("Creating a new executive member {@ExecutiveMember}", dto);
            var executiveMember = mapper.Map<ExecutiveMember>(dto);

            executiveMember.Memberships = dto.Memberships.Select(ms => new CadenceMembership
            {
                CadenceId = ms.CadenceId,
                FullName = ms.FullName,
                Department = ms.Department,
                Role = ms.Role,
                PhotoFileName = ms.PhotoFileName,
                Position = ms.Position,
                ExecutiveMember = executiveMember
            }).ToList();

            await executiveMemberRepository.AddAsync(executiveMember);

            var executiveMemberDto = mapper.Map<ExecutiveMemberDto>(executiveMember);

            return executiveMemberDto;
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Deleting executive member with id: {ExecutiveMemberId}", id);
            var executiveMember = await executiveMemberRepository.GetByIdAsync(id);

            if (executiveMember == null)
            {
                throw new NotFoundException(nameof(ExecutiveMember), id.ToString());
            }

            foreach (var membership in executiveMember.Memberships)
            {
                if (!string.IsNullOrEmpty(membership.PhotoFileName))
                {
                    imageService.DeleteExecutiveMemberPhoto(membership.PhotoFileName);
                }
            }

            await executiveMemberRepository.DeleteAsync(executiveMember);
        }

        public async Task<List<ExecutiveMemberDto>> GetAllAsync()
        {
            logger.LogInformation("Getting all executive members");
            var executiveMembers = await executiveMemberRepository.GetAllAsync();

            var executiveMembersDto = mapper.Map<List<ExecutiveMemberDto>>(executiveMembers);

            return executiveMembersDto;
        }

        public async Task<ExecutiveMemberDto> GetByIdAsync(int id)
        {
            logger.LogInformation("Getting executive member with id: {ExecutiveMemberId}", id);
            var executiveMember = await executiveMemberRepository.GetByIdAsync(id);


            if (executiveMember == null)
            {
                throw new NotFoundException(nameof(ExecutiveMember), id.ToString());
            }

            var executiveMemberDto = mapper.Map<ExecutiveMemberDto>(executiveMember);

            return executiveMemberDto;

        }

        public async Task UpdateAsync(int id, SaveExecutiveMemberDto dto)
        {
            logger.LogInformation("Updating executive member with id: {ExecutiveMemberId} with {@ExecutiveMember}", id, dto);
            var executiveMember = await executiveMemberRepository.GetByIdAsync(id);

            if (executiveMember == null)
            {
                throw new NotFoundException(nameof(ExecutiveMember), id.ToString());
            }

            foreach (var membershipToRemove in executiveMember.Memberships
                .Where(existing => !dto.Memberships.Any(incoming => incoming.CadenceId == existing.CadenceId))
                .ToList())
            {
                executiveMember.Memberships.Remove(membershipToRemove);
            }

            executiveMember.Email = dto.Email;
            executiveMember.Phone = dto.Phone;
            executiveMember.About = dto.About;

            foreach (var incoming in dto.Memberships)
            {
                var existing = executiveMember.Memberships
                    .FirstOrDefault(m => m.CadenceId == incoming.CadenceId);

                if (existing is null)
                {
                    executiveMember.Memberships.Add(new CadenceMembership
                    {
                        CadenceId = incoming.CadenceId,
                        FullName = incoming.FullName,
                        Department = incoming.Department,
                        Role = incoming.Role,
                        PhotoFileName = incoming.PhotoFileName,
                        Position = incoming.Position,
                        ExecutiveMember = executiveMember
                    });

                    continue;
                }

                if (existing.Position != incoming.Position)
                {
                    var toSwap = await executiveMemberRepository
                        .GetMembershipByCadenceAndPositionAsync(incoming.CadenceId, incoming.Position);

                    if (toSwap is not null)
                        toSwap.Position = existing.Position;

                    existing.Position = incoming.Position;
                }

                if (!string.IsNullOrEmpty(existing.PhotoFileName) &&
                    existing.PhotoFileName != incoming.PhotoFileName)
                {
                    imageService.DeleteExecutiveMemberPhoto(existing.PhotoFileName);
                }

                existing.FullName = incoming.FullName;
                existing.Department = incoming.Department;
                existing.Role = incoming.Role;
                existing.PhotoFileName = incoming.PhotoFileName;
            }

            await executiveMemberRepository.SaveChangesAsync();
        }
    }
}
