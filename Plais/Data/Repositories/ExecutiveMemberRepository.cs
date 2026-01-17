using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.DTOs;
using PLAIS.API.Data;
using PLAIS.API.Models;

namespace Plais.Data.Repositories
{
    public class ExecutiveMemberRepository(PlaisDbContext dbContext) : IExecutiveMemberRepository
    {
        public async Task AddAsync(ExecutiveMember executiveMember)
        {
            await dbContext.ExecutiveMembers.AddAsync(executiveMember);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ExecutiveMember executiveMember)
        {
            dbContext.ExecutiveMembers.Remove(executiveMember);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<ExecutiveMember>> GetAllAsync()
        {
            var executiveMembers = await dbContext.ExecutiveMembers
                .Include(em => em.Memberships).ToListAsync();
            return executiveMembers;
        }

        public async Task<ExecutiveMember?> GetByIdAsync(int id)
        {
            var executiveMember = await dbContext.ExecutiveMembers
               .Include(em => em.Memberships).FirstOrDefaultAsync(em => em.Id == id);
            return executiveMember;
        }

        public async Task<CadenceMembership?> GetMembershipByCadenceAndPositionAsync(int cadenceId, int position)
        {
            var result = await dbContext.CadenceMemberships.Include(cm => cm.ExecutiveMember)
                .FirstOrDefaultAsync(em => em.CadenceId == cadenceId && em.Position == position);

            return result;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}
