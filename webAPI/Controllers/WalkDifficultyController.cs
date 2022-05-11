using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webAPI.Models.Domain;
using webAPI.Models.DTO;
using webAPI.Repository;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDiffRepo;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDiffRepo, IMapper mapper)
        {
            this.walkDiffRepo = walkDiffRepo;
            this.mapper = mapper;
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AddWalkDifficultyRequest walkDiff)
        {
            //Validate request
            //if (!ValidateAddAsync(walkDiff))
            //{
            //    return BadRequest(ModelState);
            //}

            WalkDifficulty walkDiffDomain = await walkDiffRepo.AddAsync(new WalkDifficulty() { Code = walkDiff.Code });
            WalkDifficultyDTO walkDiffDTO = mapper.Map<WalkDifficultyDTO>(walkDiffDomain);
            return Ok(walkDiffDTO);
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<WalkDifficulty> walkDiffDomain = await walkDiffRepo.GetAllAsync();
            List<WalkDifficultyDTO> walkDiffDTO = mapper.Map<List<WalkDifficultyDTO>>(walkDiffDomain);
            return Ok(walkDiffDTO);
        }

        [HttpGet]
        [ActionName("Get")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAsync([FromRoute]Guid id)
        {
            WalkDifficulty? walkdiff = await walkDiffRepo.GetAsync(id);
            if (walkdiff == null)
            {
                return NotFound($"Walk Difficulty with id: {id} not found");
            }
            WalkDifficultyDTO walkDiffDTO = mapper.Map<WalkDifficultyDTO>(walkdiff);
            return Ok(walkDiffDTO);
        }

        [HttpPut]
        [ActionName("Update")]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id,[FromBody] UpdateWalkDifficultyRequest walkDiff)
        {
            //Validate request
            //if (!ValidateUpdateAsync(walkDiff))
            //{
            //    return BadRequest(ModelState);
            //}

            WalkDifficulty? walkDiffDomain = new WalkDifficulty()
            {
                WalkDifficultyId = id,
                Code = walkDiff.Code
            };
            walkDiffDomain = await walkDiffRepo.UpdateAsync(walkDiffDomain);
            if (walkDiffDomain == null)
            {
                return NotFound($"Walk difficulty with id {id} not found");
            }
            WalkDifficultyDTO walkDiffDTO = mapper.Map<WalkDifficultyDTO>(walkDiffDomain);
            return Ok(walkDiffDTO);
        }

        [HttpDelete]
        [ActionName("Delete")]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            WalkDifficulty? walkDiff = await walkDiffRepo.DeleteAsync(id);
            if (walkDiff == null)
            {
                return NotFound($"Walk difficulty with id {id} not found");
            }
            WalkDifficultyDTO walkDiffDTO = mapper.Map<WalkDifficultyDTO>(walkDiff);
            return Ok(walkDiffDTO);
        }

        #region Private methods
        private bool ValidateAddAsync(AddWalkDifficultyRequest walkDiff)
        {
            if(walkDiff == null)
            {
                ModelState.AddModelError(nameof(walkDiff), $"{nameof(walkDiff)} cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(walkDiff.Code))
            {
                ModelState.AddModelError(nameof(walkDiff.Code), $"{nameof(walkDiff.Code)} cannot be null or white space");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateAsync(UpdateWalkDifficultyRequest walkDiff)
        {
            if (walkDiff == null)
            {
                ModelState.AddModelError(nameof(walkDiff), $"{nameof(walkDiff)} cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(walkDiff.Code))
            {
                ModelState.AddModelError(nameof(walkDiff.Code), $"{nameof(walkDiff.Code)} cannot be null or white space");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }


        #endregion
    }
}
