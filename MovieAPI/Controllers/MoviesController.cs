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
using MovieAPI.DTOs.MoviesDtos;
using MovieAPI.Models;
using MovieAPI.Services.MovieServices;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<ReadAlbumDto>> GetAlbum(int id)
        {
            // Use service to get album by id
            var album = await _service.GetByIdAsync(id);

            // Check if found item is null
            if (album == null)
            {
                return NotFound();
            }

            // Map domain to dto
            var albumDto = _mapper.Map<ReadAlbumDto>(album);

            return Ok(albumDto);
        }

        // GET: api/albums/1/artists
        /// <summary>
        /// Get all of the Artists associated with a specific Album.
        /// </summary>
        /// <param name="id">The Id of the Album whose Artists you want to get.</param>
        /// <returns>An array of Artists.</returns>
        [HttpGet("{id}/artists")]
        public async Task<ActionResult<IEnumerable<ReadArtistDto>>> GetArtistsByAlbumId(int id)
        {
            if (!await AlbumExistsAsync(id))
            {
                return NotFound();
            }

            // Get artists by album id from the service
            var artists = await _service.GetArtistsByAlbum(id);

            if (artists == null)
            {
                return NotFound();
            }

            // Map from domain to dtos
            var artistDtos = _mapper.Map<List<ReadArtistDto>>(artists);

            return Ok(artistDtos);
        }

        // PUT: api/albums/5
        /// <summary>
        /// Update an Album.
        /// </summary>
        /// <param name="id">The Id of the Album you want to update.</param>
        /// <param name="albumDto">The updated Album object.</param>
        /// <returns>An Http response code based on the outcome of the transaction.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, UpdateAlbumDto albumDto)
        {
            // Map dto to domain object
            var album = _mapper.Map<Album>(albumDto);

            try
            {
                await _service.UpdateAsync(album);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AlbumExistsAsync(id))
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

        // PUT: api/Albums/1/artists
        /// <summary>
        /// Update the Artists associated with a specific Album.
        /// </summary>
        /// <param name="id">The Id of the Album whose Artists you want to update.</param>
        /// <param name="artistIds">A list of Artist Ids who are associated with the Album.</param>
        /// <returns>An Http response code based on the outcome of the transaction.</returns>
        [HttpPut("{id}/artists")]
        public async Task<IActionResult> UpdateAlbumArtists(int id, IEnumerable<int> artistIds)
        {
            if (!await AlbumExistsAsync(id))
            {
                return NotFound();
            }

            try
            {
                // Fetch the album to be updated
                // NB: Important to retrieve the base entity including related data
                var albumToUpdate = await _service.GetAlbumIncludingArtists(id);

                if (albumToUpdate == null)
                {
                    return NotFound();
                }

                // Execute the update using the service
                await _service.UpdateAlbumArtists(albumToUpdate, artistIds);
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
        /// <param name="albumDto">The new Album object.</param>
        /// <returns>The newly added Album object.</returns>
        [HttpPost]
        public async Task<ActionResult<ReadAlbumDto>> PostAlbum(CreateAlbumDto albumDto)
        {
            // Map dto to domain object
            var album = _mapper.Map<Album>(albumDto);
            var albumId = await _service.AddAsync(album);

            return CreatedAtAction("GetAlbum", albumId, albumDto);
        }

        // DELETE: api/albums/5
        /// <summary>
        /// Delete an Album.
        /// </summary>
        /// <param name="id">The Id of the Album you want to delete.</param>
        /// <returns>An Http response code based on the outcome of the transaction.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
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
        private async Task<bool> AlbumExistsAsync(int id)
        {
            return await _service.ExistsWithIdAsync(id);
        }
    }
}

