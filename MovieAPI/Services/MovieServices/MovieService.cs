using Microsoft.EntityFrameworkCore;
using MovieAPI.DataAccess;
using MovieAPI.Models;

namespace MovieAPI.Services.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _context;

        public MovieService(MovieDbContext context)
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

        public async Task<Movie> GetMovieIncludingCharacters(int id)
        {
            return await _context.Movies
                .Include(movie => movie.CharacterMovies)
                .Where(movie => movie.Id == id)
                .FirstAsync();


        }

        public async Task UpdateMovieCharacters(Movie movie, IEnumerable<int> characterIds)
        {
            var newCharacters = new List<Character>();

            foreach (var characterId in newCharacters)
            {
                var character = await _context.Characters.FindAsync(characterIds);
                if (character == null)
                {
                    throw new KeyNotFoundException($"Character with the ID {characterId} not found!");
                }

                newCharacters.Add(character);
            }

            movie.CharacterMovies = (ICollection<CharacterMovie>)newCharacters;
            await UpdateAsync(movie);

        }

        Task<Movie> IRepository<Movie>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<ICollection<Movie>> IRepository<Movie>.GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<int> AddAsync(Movie entity)
        {
            _context.Movies.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;

        }

        public async Task UpdateAsync(Movie entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Movie entity)
        {
            _context.Movies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsWithIdAsync(int id)
        {
            return (_context.Movies?.Any(movie => movie.Id == id)).GetValueOrDefault();
        }
    }

}
