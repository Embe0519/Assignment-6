using MovieAPI.Models;

namespace MovieAPI.Services.FranchiseServices
{
    public interface IFranschiseService :IRepository<Franchise>
    {
        public bool FranchiseExists();
        public Task<ICollection<Movie>> GetMoviesByFranchise(int Id);
        public Task<ICollection<Character>> GetCharactersByFranchise(int Id);
        public  Task UpdateAsync(Franchise entity);
        public  Task<Franchise> GetByIdAsync(int id);
        
        public  Task<int> AddAsync(Franchise entity);
        public  Task DeleteAsync(Franchise entity);
        public Task<bool> ExistsWithIdAsync(int id);



    }
}
