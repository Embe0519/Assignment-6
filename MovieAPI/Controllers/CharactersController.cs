﻿using System;
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
    

            var characters = await _service.GetAllAsync();
            var charactersDto = _mapper.Map<List<ReadCharacterDto>>(characters);
            return Ok(charactersDto);
        }

  

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, UpdateCharacterDto characterDto)
        {
            

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
                    // Refresh the entity state from the database
                    var existingCharacter = await _service.GetByIdAsync(id);

                    // Update the entity with changes from the DTO
                    _mapper.Map(characterDto, existingCharacter);

                    // Save the changes again
                    await _service.AddAsync(character);
                }
            }
            return NoContent();
        }

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReadCharacterDto>> PostCharacter(CreateCharacterDto characterDto)
        {
        

            var character = _mapper.Map<Character>(characterDto);
            var characterId = await _service.AddAsync(character);
            return CreatedAtAction("GetCharacter", characterId, characterDto);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
          

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
