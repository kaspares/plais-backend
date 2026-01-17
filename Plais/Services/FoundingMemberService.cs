using AutoMapper;
using Plais.Data.Interfaces;
using Plais.DTOs.FoundingMembers;
using Plais.DTOs.CurrentMember;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;

namespace Plais.Services
{
    public class FoundingMemberService(ILogger<FoundingMemberService> logger,
        IMapper mapper,
        IFoundingMemberRepository memberRepository) : IFoundingMemberService
    {
        public async Task<FoundingMembersDto> CreateAsync(SaveFoundingMembersDto dto)
        {
            logger.LogInformation("Creating a new member {@Member}", dto);
            var member = mapper.Map<FoundingMembers>(dto);

            await memberRepository.AddAsync(member);

            var memberResultDto = mapper.Map<FoundingMembersDto>(member);
            return memberResultDto;
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Deleting a member with id: {MemberId}", id);

            var member = await memberRepository.GetByIdAsync(id);

            if (member == null)
            {
                throw new NotFoundException(nameof(FoundingMembers), id.ToString());

            }

            await memberRepository.DeleteAsync(member);
        }

        public async Task<List<FoundingMembersDto>> GetAllAsync()
        {
            logger.LogInformation("Getting all members");

            var members = await memberRepository.GetAllAsync();

            var membersDto = mapper.Map<List<FoundingMembersDto>>(members);

            return membersDto;
        }

        public async Task<FoundingMembersDto> GetByIdAsync(int id)
        {
            logger.LogInformation("Getting a member with id: {Id}", id);

            var member = await memberRepository.GetByIdAsync(id);

            if (member == null)
            {
                throw new NotFoundException(nameof(FoundingMembers), id.ToString());

            }

            var memberDto = mapper.Map<FoundingMembersDto>(member);

            return memberDto;
        }

        public async Task UpdateAsync(int id, SaveFoundingMembersDto dto)
        {
            logger.LogInformation("Updating a member with id: {Id} with {@SaveMemberDto}", id, dto);

            var member = await memberRepository.GetByIdAsync(id);

            if (member == null)
            {
                throw new NotFoundException(nameof(FoundingMembers), id.ToString());

            }

            mapper.Map(dto, member);
            await memberRepository.SaveChangesAsync();
        }
    }
}
