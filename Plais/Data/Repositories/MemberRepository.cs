using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;

namespace Plais.Data.Repositories
{
    public class MemberRepository(PlaisDbContext dbContext) : IMemberRepository
    {
        public async Task AddAsync(CurrentMembers member)
        {
            await dbContext.CurrentMembers.AddAsync(member);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CurrentMembers member)
        {
            dbContext.CurrentMembers.Remove(member);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<CurrentMembers>> GetAllAsync()
        {
            var members = await dbContext.CurrentMembers.ToListAsync();

            return members;
        }

        public async Task<CurrentMembers?> GetByIdAsync(int id)
        {
            var member = await dbContext.CurrentMembers.FirstOrDefaultAsync(m => m.Id == id);

            return member;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
