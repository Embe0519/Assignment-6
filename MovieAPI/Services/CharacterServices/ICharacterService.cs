using MovieAPI.Models;

namespace MovieAPI.Services.CharacterServices
{
    public interface ICharacterService : IRepository<Character>
    {
        /// <summary>
        /// Get a list of Characters associated with a specific Movie.
        /// </summary>
        /// <param name="id">The Id of the Movie whose Characcters you want to get.</param>
        /// <returns>An array of character objects.</returns>
         Task<ICollection<Character>> GetCharactersByMovie(int id);
        
        


    }
}
