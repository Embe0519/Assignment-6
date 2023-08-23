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

        // GET: api/Franchises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadFranchisesDto>>> GetFranchises()
        {
          //if (_context.Franchises == null)
          //{
          //    return NotFound();
          //}
          //  var franchisesDomain = await _context.Franchises.ToListAsync();
          //  var franchiseDto = _mapper.Map<List<ReadFranchisesDto>>(franchisesDomain);
          //  return franchiseDto;

            var franchises = await _service.GetAllAsync();
            var franchiseDto = _mapper.Map<List<ReadFranchisesDto>>(franchises);
            return Ok(franchiseDto);
        }

        // GET: api/Franchises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadFranchisesDto>> GetFranchise(int id)
        {
            //if (_context.Franchises == null)
            //{
            //    return NotFound();
            //}
            //  var franchise = await _context.Franchises.FindAsync(id);

            //  if (franchise == null)
            //  {
            //      return NotFound();
            //  }

            //  return franchise;
            var franchise = await _service.GetByIdAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            var franchiseDto = _mapper.Map<ReadCharacterDto>(franchise);
            return Ok(franchiseDto);
        }

        // PUT: api/Franchises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, UpdateFranchisesDto franchiseDto)
        {
            //if (id != franchise.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(franchise).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!FranchiseExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
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
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Franchises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReadFranchisesDto>> PostFranchise(CreateFranchisesDto franchiseDto)
        {
            //if (_context.Franchises == null)
            //{
            //    return Problem("Entity set 'MovieDbContext.Franchises'  is null.");
            //}

            //  var franchiseDomain = _mapper.Map<Franchise>(franchiseDto);
            //  _context.Franchises.Add(franchiseDomain);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetFranchise", new { id = franchiseDomain.Id }, franchiseDto);
            var franchise = _mapper.Map<Franchise>(franchiseDto);
            var franchiseId = await _service.AddAsync(franchise);
            return CreatedAtAction("GetFranchise", franchiseId, franchiseDto);
        }

        // DELETE: api/Franchises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            //if (_context.Franchises == null)
            //{
            //    return NotFound();
            //}
            //var franchise = await _context.Franchises.FindAsync(id);
            //if (franchise == null)
            //{
            //    return NotFound();
            //}

            //_context.Franchises.Remove(franchise);
            //await _context.SaveChangesAsync();

            //return NoContent();
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

        private async Task <bool> FranchiseExists(int id)
        {
            return await _service.ExistsWithIdAsync(id);
        }
    }
}
