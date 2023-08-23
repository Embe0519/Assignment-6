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
using MovieAPI.DTOs.MoviesDtos;
using MovieAPI.Models;
using MovieAPI.Services.CharacterServices;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _service;
        private readonly IMapper _mapper;

        public CharactersController(ICharacterService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadCharacterDto>>> GetCharacters()
        {
          //if (_context.Characters == null)
          //{
          //    return NotFound();
          //}
          //  var charactersDomain = await _context.Characters.ToListAsync();
          //  var characterDto = _mapper.Map<List<ReadCharacterDto>>(charactersDomain);
          //  return characterDto;

            var characters = await _service.GetAllAsync();
            var charactersDto = _mapper.Map<List<ReadCharacterDto>>(characters);
            return Ok(charactersDto);
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCharacterDto>> GetCharacter(int id)
        {
          //if (_context.Characters == null)
          //{
          //    return NotFound();
          //}
          //  var character = await _context.Characters.FindAsync(id);

          //  if (character == null)
          //  {
          //      return NotFound();
          //  }

          //  return character;

            var character = await _service.GetByIdAsync(id);
            if (character == null)
            {
                return NotFound(); 
            }

            var characterDto = _mapper.Map<ReadCharacterDto>(character);
            return Ok(characterDto);


        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, UpdateCharacterDto characterDto)
        {
            //if (id != character.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(character).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CharacterExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();

            var character = _mapper.Map<Character>(characterDto);
            try
            {
                await _service.UpdateAsync(character);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CharacterExists(id))
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

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReadCharacterDto>> PostCharacter(CreateCharacterDto characterDto)
        {
            //if (_context.Characters == null)
            //{
            //    return Problem("Entity set 'MovieDbContext.Characters'  is null.");
            //}

            //  var characterDomain = _mapper.Map<Character>(characterDto);
            //  _context.Characters.Add(characterDomain);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetCharacter", new { id = characterDomain.Id }, characterDto);

            var character = _mapper.Map<Character>(characterDto);
            var characterId = await _service.AddAsync(character);
            return CreatedAtAction("GetCharacter", characterId, characterDto);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            //if (_context.Characters == null)
            //{
            //    return NotFound();
            //}
            //var character = await _context.Characters.FindAsync(id);
            //if (character == null)
            //{
            //    return NotFound();
            //}

            //_context.Characters.Remove(character);
            //await _context.SaveChangesAsync();

            //return NoContent();

            if (!await _service.ExistsWithIdAsync(id))
            {
                return NotFound();
            }

            var character = await _service.GetByIdAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(character);
            return NoContent();
        }

        private async Task<bool> CharacterExists(int id)
        {
            return await _service.ExistsWithIdAsync(id);
        }
    }
}
