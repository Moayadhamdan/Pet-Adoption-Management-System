using Microsoft.AspNetCore.Mvc;
using PetAdoption.Server.Repository;
using PetAdoption.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetAdoption.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly PetRepository _petRepository;
        private readonly AdoptionRepository _adoptionRepository;

        public PetController(PetRepository petRepository, AdoptionRepository adoptionRepository)
        {
            _petRepository = petRepository;
            _adoptionRepository = adoptionRepository;
        }

        // GET: api/pet
        [HttpGet]
        public IActionResult GetAllPets()
        {
            try
            {
                var pets = _petRepository.Select().Execute();
                return Ok(pets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/pet
        [HttpPost]
        public IActionResult AddPet([FromBody] PetModel pet)
        {
            if (pet == null)
            {
                return BadRequest("Pet data is null.");
            }

            try
            {
                _petRepository.Insert().WithFields(p => p.Exclude(f => f.Id)).Execute(pet);
                return CreatedAtAction(nameof(GetAllPets), new { id = pet.Id }, pet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/pet/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePet(int id, [FromBody] PetModel pet)
        {
            if (pet == null)
            {
                return BadRequest("Pet data is null.");
            }

            try
            {
                var updateResult = _petRepository.Update().Where(p => p.Eq(m => m.Id, id)).Execute(pet);

                if (updateResult == null)
                {
                    return NotFound($"Pet with id {id} not found.");
                }

                return Ok(pet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/pet/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePet(int id)
        {
            try
            {
                var adoptionRequests = _adoptionRepository.Select()
                    .Where(ar => ar.Eq(m => m.PetId, id))
                    .Execute();

                foreach (var request in adoptionRequests)
                {
                    _adoptionRepository.Delete().Where(ar => ar.Eq(m => m.Id, request.Id)).Execute();
                }

                var deleteResult = _petRepository.Delete().Where(p => p.Eq(m => m.Id, id)).Execute();

                if (deleteResult == 0)
                {
                    return NotFound($"Pet with id {id} not found.");
                }

                return Ok($"Pet with id {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
