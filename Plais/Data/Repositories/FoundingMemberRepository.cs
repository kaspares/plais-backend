using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class FoundingMemberRepository(PlaisDbContext dbContext) : IFoundingMemberRepository
    {
        public async Task AddAsync(FoundingMembers member)
        {
            await dbContext.FoundingMembers.AddAsync(member);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(FoundingMembers member)
        {
            dbContext.FoundingMembers.Remove(member);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<FoundingMembers>> GetAllAsync()
        {
            var members = await dbContext.FoundingMembers.ToListAsync();

            return members;
        }

        public async Task<FoundingMembers?> GetByIdAsync(int id)
        {
            var member = await dbContext.FoundingMembers.FirstOrDefaultAsync(m => m.Id == id);

            return member;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}

