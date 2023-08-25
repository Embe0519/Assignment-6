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

        public async Task<Franchise> GetByIdAsync(int id)
        {
            return await _context.Franchises.FindAsync(id);
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
        public async Task<bool> ExistsWithIdAsync(int id)
        {
            return (_context.Franchises?.Any(franchise => franchise.Id == id)).GetValueOrDefault();
        }
        public bool FranchiseExists()
        {
            return _context.Franchises == null;
        }
        public async Task<ICollection<Movie>> GetMoviesByFranchise(int Id)
        {
            var moviesByFranchise = await _context.Movies
                .Where(x => x.FranchiseId == Id)
                .ToListAsync();

            return moviesByFranchise;
        }

        public async Task<ICollection<Character>> GetCharactersByFranchise(int Id)
        {
            var charactersByFranchise = await _context.Characters
                .Where(character => character.CharacterMovies
                    .Any(cm => cm.Movie.FranchiseId == Id))
                .ToListAsync();

            return charactersByFranchise;
        }
    }
}
