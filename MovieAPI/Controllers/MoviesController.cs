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

        // GET: api/albums/5
        /// <summary>
        /// Get an Album by Id.
        /// </summary>
        /// <param name="id">The Id of the Album you want to get.</param>
        /// <returns>An Album object.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadMoviesDto>> GetAlbum(int id)
        {
            // Use service to get album by id
            var album = await _service.GetByIdAsync(id);

            // Check if found item is null
            if (album == null)
            {
                return NotFound();
            }

            // Map domain to dto
            var albumDto = _mapper.Map<ReadMoviesDto>(album);

            return Ok(albumDto);
        }

        // GET: api/albums/1/artists
        /// <summary>
        /// Get all of the Artists associated with a specific Album.
        /// </summary>
        /// <param name="id">The Id of the Album whose Artists you want to get.</param>
        /// <returns>An array of Artists.</returns>
        [HttpGet("{id}/artists")]
        public async Task<ActionResult<IEnumerable<ReadCharacterDto>>> GetArtistsByAlbumId(int id)
        {
            if (!await MovieExistsAsync(id))
            {
                return NotFound();
            }

            // Get artists by album id from the service
            var character = await _service.GetCharactersByMovie(id);

            if (character == null)
            {
                return NotFound();
            }

            // Map from domain to dtos
            var characterDtos = _mapper.Map<List<ReadCharacterDto>>(character);

            return Ok(characterDtos);
        }

        // PUT: api/albums/5
        /// <summary>
        /// Update an Album.
        /// </summary>
        /// <param name="id">The Id of the Album you want to update.</param>
        /// <param name="movieDto">The updated Album object.</param>
        /// <returns>An Http response code based on the outcome of the transaction.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, UpdateMoviesDto movieDto)
        {
            // Map dto to domain object
            var movie = _mapper.Map<Movie>(movieDto);

            try
            {
                await _service.UpdateAsync(movie);
            }
            catch (DbUpdateConcurrencyException)
            {

               
            }

            return NoContent();
        }

        // PUT: api/Albums/1/artists
        /// <summary>
        /// Update the Artists associated with a specific Album.
        /// </summary>
        /// <param name="id">The Id of the Album whose Artists you want to update.</param>
        /// <param name="characterIds">A list of Artist Ids who are associated with the Album.</param>
        /// <returns>An Http response code based on the outcome of the transaction.</returns>
        [HttpPut("{id}/artists")]
        public async Task<IActionResult> UpdateMovieCharacter(int id, IEnumerable<int> characterIds)
        {
            if (!await MovieExistsAsync(id))
            {
                return NotFound();
            }

            try
            {
                // Fetch the album to be updated
                // NB: Important to retrieve the base entity including related data
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


        // POST: api/albums
        /// <summary>
        /// Add a new Album.
        /// </summary>
        /// <param name="moviesDto">The new Album object.</param>
        /// <returns>The newly added Album object.</returns>
        [HttpPost]
        public async Task<ActionResult<ReadMoviesDto>> PostAlbum(CreateMoviesDto moviesDto)
        {
            // Map dto to domain object
            var movie = _mapper.Map<Movie>(moviesDto);
            var movieId = await _service.AddAsync(movie);

            return CreatedAtAction("Get movie", movieId, moviesDto);
        }

        // DELETE: api/albums/5
        /// <summary>
        /// Delete an Album.
        /// </summary>
        /// <param name="id">The Id of the Album you want to delete.</param>
        /// <returns>An Http response code based on the outcome of the transaction.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!await _service.ExistsWithIdAsync(id))
            {
                return NotFound();
            }

            var album = await _service.GetByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(album);

            return NoContent();
        }

        /// <summary>
        /// A helper method to check if a specific Album exists.
        /// </summary>
        /// <param name="id">The Id of the Album you want to check exists.</param>
        /// <returns>True if the Album exists.</returns>
        private async Task<bool> MovieExistsAsync(int id)
        {
            return await _service.ExistsWithIdAsync(id);
        }
    }
}

