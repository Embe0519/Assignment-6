using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.DataAccess;
using MovieAPI.DTOs.CharactersDtos;
using MovieAPI.DTOs.MoviesDtos;
using MovieAPI.Models;
using MovieAPI.Services.MovieServices;

namespace MovieAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Movies
        /// <summary>
        /// Get all Movies.
        /// </summary>
        /// <returns>An array of Movies.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadMoviesDto>>> GetAlbum()
        {
            var movies = await _service.GetAllAsync();
            var moviesDto = _mapper.Map<List<ReadMoviesDto>>(movies);

            return Ok(moviesDto);
        }

        // GET: api/movies/5
        /// <summary>
        /// Get an Movie by Id.
        /// </summary>
        /// <param name="id">The Id of the Movie you want to get.</param>
        /// <returns>A Movie object.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadMoviesDto>> GetMovie(int id)
        {
            // Use service to get movie by id
            var movie = await _service.GetByIdAsync(id);

            // Check if found item is null
            if (movie == null)
            {
                return NotFound();
            }

            // Map domain to dto
            var movieDto = _mapper.Map<ReadMoviesDto>(movie);

            return Ok(movieDto);
        }

        // GET: api/movies/1/characters
        /// <summary>
        /// Get all of the characters associated with a specific Movie.
        /// </summary>
        /// <param name="id">The Id of the Movie whose characters you want to get.</param>
        /// <returns>An array of Characters.</returns>
        // GET: api/Characters/5
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<ReadCharacterDto>>> GetMoviesByCharacterId(int id)
        {
      
            if (!await MovieExistsAsync(id))
            {
                return NotFound();
            }

            // Get characters by movie using service
            var character = await _service.GetCharactersByMovie(id);

            if (character == null)
            {
                return NotFound();
            }

            // Map domain to dtos
            var characterDto = _mapper.Map<List<ReadCharacterDto>>(character);

            return Ok(characterDto);


        }

        // PUT: api/movies/5
        /// <summary>
        /// Update an Movie.
        /// </summary>
        /// <param name="id">The Id of the Movie you want to update.</param>
        /// <param name="movieDto">The updated Movie object.</param>
        /// <returns>An Http response code based on the outcome of the transaction.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, UpdateMoviesDto movieDto)
        {
            // Map dto to domain object
            var movie = _mapper.Map<Movie>(movieDto);

            try
            {
                await _service.UpdateAsync(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MovieExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // PUT: api/Movie/1/Characters
        /// <summary>
        /// Update the Characters associated with a specific Movie.
        /// </summary>
        /// <param name="id">The Id of the Movie whose Artists you want to update.</param>
        /// <param name="characterIds">A list of Characters Ids who are associated with the Movie.</param>
        /// <returns>An Http response code based on the outcome of the transaction.</returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateMovieCharacter(int id, IEnumerable<int> characterIds)
        {
            if (!await MovieExistsAsync(id))
            {
                return NotFound();
            }

            try
            {
                // Fetch the movie to be updated
                var movieToUpdate = await _service.GetMovieIncludingCharacters(id);

                if (movieToUpdate == null)
                {
                    return NotFound();
                }

                // Execute the update using the service
                await _service.UpdateMovieCharacters(movieToUpdate, characterIds);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }


        // POST: api/movies
        /// <summary>
        /// Add a new Movie.
        /// </summary>
        /// <param name="moviesDto">The new Movie object.</param>
        /// <returns>The newly added Movie object.</returns>
        [HttpPost]
        public async Task<ActionResult<ReadMoviesDto>> PostMovie(CreateMoviesDto moviesDto)
        {
            // Map dto to domain object
            var movie = _mapper.Map<Movie>(moviesDto);
            var movieId = await _service.AddAsync(movie);

            return CreatedAtAction("Get movie", movieId, moviesDto);
        }

        // DELETE: api/movies/5
        /// <summary>
        /// Delete a Movie.
        /// </summary>
        /// <param name="id">The Id of the Movie you want to delete.</param>
        /// <returns>An Http response code based on the outcome of the transaction.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!await _service.ExistsWithIdAsync(id))
            {
                return NotFound();
            }

            var movie = await _service.GetByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(movie);

            return NoContent();
        }

        /// <summary>
        /// A helper method to check if a specific Movie exists.
        /// </summary>
        /// <param name="id">The Id of the Movie you want to check exists.</param>
        /// <returns>True if the Movie exists.</returns>
        private async Task<bool> MovieExistsAsync(int id)
        {
            return await _service.ExistsWithIdAsync(id);
        }
    }
}

