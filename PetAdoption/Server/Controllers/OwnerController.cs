using Microsoft.AspNetCore.Mvc;
using PetAdoption.Server.Repository;
using PetAdoption.Shared;

namespace PetAdoption.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly OwnerRepository _ownerRepository;

        public OwnerController(OwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        [HttpGet]
        public ActionResult<List<OwnerModel>> GetAllOwners()
        {
            var owners = _ownerRepository.Select().Execute();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        public ActionResult<OwnerModel> GetOwner(int id)
        {
            var owner = _ownerRepository.Select()
                                        .Where(o => o.Eq(m => m.Id, id))
                                        .Execute().FirstOrDefault();

            if (owner == null) return NotFound();
            return Ok(owner);
        }

        [HttpPost]
        public ActionResult<OwnerModel> CreateOwner([FromBody] OwnerModel owner)
        {
            _ownerRepository.Insert().WithFields(o => o.Exclude(f => f.Id)).Execute(owner);
            return CreatedAtAction(nameof(GetOwner), new { id = owner.Id }, owner);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(int id, [FromBody] OwnerModel owner)
        {
            if (id != owner.Id) return BadRequest();

            var ownerToUpdate = new OwnerModel
            {
                PetId = owner.PetId,
                OwnerName = owner.OwnerName,
                AdoptionDate = owner.AdoptionDate
            };

            _ownerRepository.Update()
                .Where(o => o.Eq(m => m.Id, id))
                .WithFields(o => o.Exclude(f => f.Id))
                .Execute(ownerToUpdate);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(int id)
        {
            _ownerRepository.Delete().Where(o => o.Eq(m => m.Id, id)).Execute();
            return NoContent();
        }
    }
}
