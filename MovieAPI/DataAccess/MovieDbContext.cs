using Film_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieAPI.DataAccess
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions options) : base(options){}
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterMovie> CharacterMovies { get; set; }

        //Seed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Franchise
            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 1, Name = "Marvel", Description = "The Marvel Cinematic Universe (MCU) films are a series of American superhero films produced by Marvel Studios based on characters that appear in publications by Marvel Comics." });
            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 2, Name = "DC", Description = "The DC Universe (DCU) is an upcoming American media franchise and shared universe based on characters from DC Comics publications." });
            // Movie
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 1, Title = "Avengers: Endgame", Genre = "Action", Year = 2019, Director = "Anthony Russo", Picture = "x", Trailer = "https://www.youtube.com/watch?v=hA6hldpSTF8", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 2, Title = "Suicide Squad", Genre = "Fantasy", Year = 2016, Director = "David Ayer", Picture = "x", Trailer = "https://www.youtube.com/watch?v=CmRih_VtVAs", FranchiseId = 2 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 3, Title = "Guardians of the Galaxy", Genre = "Action", Year = 2014, Director = "James Gunn", Picture = "x", Trailer = "https://www.youtube.com/watch?v=0t2bmsXeFYE", FranchiseId = 1 });
            ////Character
            modelBuilder.Entity<Character>().HasData(new Character { Id = 1, Name = "Robert Downey Jr", Alias = "Iron Man", Gender = "Male", Picture = "https://www.imdb.com/name/nm0000375/?ref_=tt_cl_t_1" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 2, Name = "Scarlett Johansson", Alias = "Black Widow", Gender = "Female", Picture = "https://www.imdb.com/name/nm0424060/?ref_=tt_cl_t_5" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 3, Name = "Jared Leto", Alias = "The Joker", Gender = "Male", Picture = "https://www.imdb.com/name/nm0001467/?ref_=tt_cl_t_2" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 4, Name = "Margot Robbie", Alias = "Harley Quinn", Gender = "Female", Picture = "https://www.imdb.com/name/nm3053338/?ref_=tt_cl_t_3" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 5, Name = "Chris Pratt", Alias = "Star Lord", Gender = "Male", Picture = "https://www.imdb.com/name/nm0695435/?ref_=tt_cl_i_1" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 6, Name = "Pom Klementieff", Alias = "Mantis", Gender = "Female", Picture = "https://www.imdb.com/name/nm2962353/?ref_=tt_cl_t_4" });
            //Setting up many to many relationship
            modelBuilder.Entity<CharacterMovie>().HasKey(mc => new { mc.MovieId, mc.CharacterId });
            modelBuilder.Entity<CharacterMovie>().HasOne(mc => mc.Movie).WithMany(m => m.CharacterMovies).HasForeignKey(mc => mc.MovieId);
            modelBuilder.Entity<CharacterMovie>().HasOne(mc => mc.Character).WithMany(c => c.CharacterMovies).HasForeignKey(mc => mc.CharacterId);
            //MovieCharacter
            modelBuilder.Entity<CharacterMovie>().HasData(new CharacterMovie { MovieId = 1, CharacterId = 1 });
            modelBuilder.Entity<CharacterMovie>().HasData(new CharacterMovie { MovieId = 1, CharacterId = 2 });
            modelBuilder.Entity<CharacterMovie>().HasData(new CharacterMovie { MovieId = 2, CharacterId = 3 });
            modelBuilder.Entity<CharacterMovie>().HasData(new CharacterMovie { MovieId = 2, CharacterId = 4 });
            modelBuilder.Entity<CharacterMovie>().HasData(new CharacterMovie { MovieId = 3, CharacterId = 5 });
            modelBuilder.Entity<CharacterMovie>().HasData(new CharacterMovie { MovieId = 3, CharacterId = 6 });
        }

    }

}
