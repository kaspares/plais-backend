using AutoMapper;
using Plais.Data.Interfaces;
using Plais.DTOs.CurrentMember;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;

namespace Plais.Services
{
    public class MemberServices(ILogger<MemberServices> logger,
        IMapper mapper,
        IMemberRepository memberRepository) : IMemberServices
    {
        public async Task<CurrentMemberDto> CreateAsync(SaveCurrentMemberDto dto)
        {
            logger.LogInformation("Creating a new member {@Member}", dto);
            var member = mapper.Map<CurrentMembers>(dto);

            await memberRepository.AddAsync(member);

            var memberResultDto = mapper.Map<CurrentMemberDto>(member);
            return memberResultDto;
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Deleting a member with id: {MemberId}", id);

            var member = await memberRepository.GetByIdAsync(id);

            if (member == null)
            {
                throw new NotFoundException(nameof(CurrentMembers), id.ToString());

            }

            await memberRepository.DeleteAsync(member);
        }

        public async Task<List<CurrentMemberDto>> GetAllAsync()
        {
            logger.LogInformation("Getting all members");

            var members = await memberRepository.GetAllAsync();

            var membersDto = mapper.Map<List<CurrentMemberDto>>(members);

            return membersDto;
        }

        public async Task<CurrentMemberDto> GetByIdAsync(int id)
        {
            logger.LogInformation("Getting a member with id: {Id}", id);

            var member = await memberRepository.GetByIdAsync(id);

            if (member == null)
            {
                throw new NotFoundException(nameof(CurrentMembers), id.ToString());

            }

            var memberDto = mapper.Map<CurrentMemberDto>(member);

            return memberDto;
        }

        public async Task UpdateAsync(int id, SaveCurrentMemberDto dto)
        {
            logger.LogInformation("Updating a member with id: {Id} with {@SaveMemberDto}", id, dto);

            var member = await memberRepository.GetByIdAsync(id);

            if (member == null)
            {
                throw new NotFoundException(nameof(CurrentMembers), id.ToString());

            }

            mapper.Map(dto, member);
            await memberRepository.SaveChangesAsync();
        }
    }
}
