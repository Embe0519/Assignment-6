using MovieAPI.Models;

namespace MovieAPI.Services.MovieServices
{
    /// <summary>
    /// Contains the functionality that only MovieService should implement
    /// </summary>
    public interface IMovieService : IRepository<Movie>
    {
        /// <summary>
        /// Get a list of Characters associated with a specific Movie.
        /// </summary>
        /// <param name="id">The Id of the Movie whose Characters you want to get.</param>
        /// <returns>An array of character objects.</returns>
        Task<ICollection<Character>> GetCharactersByMovie(int id);

        /// <summary>
        /// Get a movie by Id including its related characters.
        /// </summary>
        /// <param name="id">The Id of the movie you want to get.</param>
        /// <returns>The movie object including its related characters.</returns>
        Task<Movie> GetMovieIncludingCharacters(int id);

        /// <summary>
        /// Update the characters associated with a specific Movie.
        /// </summary>
        /// <param name="movie">The movie whose charcters you want to update.</param>
        /// <param name="characterIds">A list of Characters Ids you want to update the movie with.</param>
        Task UpdateMovieCharacters(Movie movie, IEnumerable<int> characterIds);
    }


}
