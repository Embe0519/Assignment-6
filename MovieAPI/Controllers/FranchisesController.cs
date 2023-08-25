using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.DataAccess;
using MovieAPI.DTOs.CharactersDtos;
using MovieAPI.DTOs.FranchisesDtos;
using MovieAPI.DTOs.MoviesDtos;
using MovieAPI.Models;
using MovieAPI.Services.FranchiseServices;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly IFranschiseService _service;
        private readonly IMapper _mapper;

        public FranchisesController(IFranschiseService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all franchises
        /// </summary>
        /// <returns>An array of franchises</returns>
        // GET: api/Franchises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadFranchisesDto>>> GetFranchises()
        {
         

            var franchises = await _service.GetAllAsync();
            var franchiseDto = _mapper.Map<List<ReadFranchisesDto>>(franchises);
            return Ok(franchiseDto);
        }

        /// <summary>
        /// Gat franchise by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A franchise object</returns>
        // GET: api/Franchises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadFranchisesDto>> GetFranchise(int id)
        {
         
            var franchise = await _service.GetByIdAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            var franchiseDto = _mapper.Map<ReadCharacterDto>(franchise);
            return Ok(franchiseDto);
        }

        /// <summary>
        /// Update franchise 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="franchiseDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, UpdateFranchisesDto franchiseDto)
        {
          
            var franchise = _mapper.Map<Franchise>(franchiseDto);
            try
            {
                await _service.UpdateAsync(franchise);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FranchiseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    // Refresh the entity state from the database
                    var existingFranchise = await _service.GetByIdAsync(id);

                    // Update the entity with changes from the DTO
                    _mapper.Map(franchiseDto, existingFranchise);

                    // Save the changes again
                    await _service.AddAsync(franchise);
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Add a new franchise
        /// </summary>
        /// <param name="franchiseDto"></param>
        /// <returns>The newly added franchise object</returns>
        [HttpPost]
        public async Task<ActionResult<ReadFranchisesDto>> PostFranchise(CreateFranchisesDto franchiseDto)
        {
           
            var franchise = _mapper.Map<Franchise>(franchiseDto);
            var franchiseId = await _service.AddAsync(franchise);
            return CreatedAtAction("GetFranchise", franchiseId, franchiseDto);
        }

        /// <summary>
        /// Delete a franchise 
        /// </summary>
        /// <param name="id">The Id of the Franchise you want to delete.</param>
        /// <returns></returns>
        // DELETE: api/Franchises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
         
            if (!await _service.ExistsWithIdAsync(id))
            {
                return NotFound();
            }

            var franchise = await _service.GetByIdAsync(id);

            if (franchise == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(franchise);
            return NoContent();
        }

        /// <summary>
        /// A helper method to check if a specific Franchise exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task <bool> FranchiseExists(int id)
        {
            return await _service.ExistsWithIdAsync(id);
        }
    }
}
