using Microsoft.AspNetCore.Mvc;
using PetAdoption.Server.Repository;
using PetAdoption.Shared;

namespace PetAdoption.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdoptionController : ControllerBase
    {
        private readonly AdoptionRepository _adoptionRepository;

        public AdoptionController(AdoptionRepository adoptionRepository)
        {
            _adoptionRepository = adoptionRepository;
        }

        [HttpGet]
        public ActionResult<List<AdoptionModel>> GetAllAdoptions()
        {
            var adoptions = _adoptionRepository.Select().Execute();
            return Ok(adoptions);
        }

        [HttpGet("{id}")]
        public ActionResult<AdoptionModel> GetAdoption(int id)
        {
            var adoption = _adoptionRepository.Select()
                                              .Where(a => a.Eq(m => m.Id, id))
                                              .Execute().FirstOrDefault();

            if (adoption == null) return NotFound();
            return Ok(adoption);
        }

        [HttpPost]
        public ActionResult<AdoptionModel> CreateAdoption([FromBody] AdoptionModel adoption)
        {
            _adoptionRepository.Insert().WithFields(a => a.Exclude(f => f.Id)).Execute(adoption);
            return CreatedAtAction(nameof(GetAdoption), new { id = adoption.Id }, adoption);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAdoption(int id, [FromBody] AdoptionModel adoption)
        {
            if (id != adoption.Id) return BadRequest();

            var adoptionToUpdate = new AdoptionModel
            {
                PetId = adoption.PetId,
                RequesterName = adoption.RequesterName,
                RequestDate = adoption.RequestDate,
                Status = adoption.Status
            };

            _adoptionRepository.Update()
                .Where(a => a.Eq(m => m.Id, id))
                .WithFields(a => a.Exclude(f => f.Id))
                .Execute(adoptionToUpdate);

            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteAdoption(int id)
        {
            _adoptionRepository.Delete().Where(a => a.Eq(m => m.Id, id)).Execute();
            return NoContent();
        }
    }
}
