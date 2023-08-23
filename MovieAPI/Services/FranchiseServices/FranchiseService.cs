using Microsoft.EntityFrameworkCore;
using MovieAPI.DataAccess;
using MovieAPI.Models;

namespace MovieAPI.Services.FranchiseServices
{
    public class FranchiseService :IFranschiseService
    {
        private readonly MovieDbContext _context;

        public FranchiseService(MovieDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(Franchise entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        Task<Franchise> IRepository<Franchise>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<ICollection<Franchise>> IRepository<Franchise>.GetAllAsync()
        {
            return await _context.Franchises.ToListAsync();
        }

        public async Task<int> AddAsync(Franchise entity)
        {
            _context.Franchises.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(Franchise entity)
        {
            _context.Franchises.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public Task<bool> ExistsWithIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
