using Microsoft.EntityFrameworkCore;
using MovieAPI.DataAccess;
using MovieAPI.Models;
using System.Collections.ObjectModel;

namespace MovieAPI.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
        private readonly MovieDbContext _context;
        public CharacterService(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Character>> GetCharactersByMovie(int id)
        {
          var characterByMovie = await _context.Characters
                .Where(c => c.CharacterMovies.Any(cm => cm.MovieId == id))
                .ToListAsync();
            return characterByMovie;
        }


        public async Task UpdateCharacterMovies(Character character, IEnumerable<int> movieIds)
        {
            var newMovie = new List<Movie>();

            foreach (var movieId in newMovie)
            {
                var movie = await _context.Movies.FindAsync(movieIds);
                if (movie == null)
                {
                    throw new KeyNotFoundException($"Movie with the ID {movieId} not found!");
                }

                newMovie.Add(movie);
            }

            character.CharacterMovies = (ICollection<CharacterMovie>)newMovie;
            await UpdateAsync(character);

        }

        public async Task UpdateAsync(Character entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            return await _context.Characters.FindAsync(id);
        }

        async Task<ICollection<Character>> IRepository<Character>.GetAllAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<int> AddAsync(Character entity)
        {
            _context.Characters.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(Character entity)
        {
            _context.Characters.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsWithIdAsync(int id)
        {
            return (_context.Characters?.Any(character => character.Id == id)).GetValueOrDefault();
        }
    }
}
