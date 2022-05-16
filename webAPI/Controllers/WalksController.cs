using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webAPI.Models.Domain;
using webAPI.Models.DTO;
using webAPI.Repository;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDiffRepo;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDiffRepo)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDiffRepo = walkDiffRepo;
        }

        [HttpGet]
        [ActionName("get-all")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllAsync()
        {
            // Fetch all walks
            IEnumerable<Walk> walks = await walkRepository.GetAllAsync();

            // Convert to DTO
            List<WalkDTO> walksDTO = mapper.Map<List<WalkDTO>>(walks);

            // Return
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            Walk? walk = await walkRepository.GetAsync(id);

            if (walk == null)
            {
                return NotFound($"Walk with id {id} not found");
            }

            WalkDTO walkDTO = mapper.Map<WalkDTO>(walk);

            return Ok(walkDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddAsync([FromBody]AddWalkRequest walk)
        {
            // Validate request
            bool valid = await ValidateAddAsync(walk);
            if (!valid)
            {
                return BadRequest(ModelState);
            }
            // Convert DTO to domain
            Walk walkDomain = new Walk()
            {
                Name = walk.Name,
                Length = walk.Length,
                WalkDifficultyId = walk.WalkDifficultyId,
                RegionId = walk.RegionId,
            };

            // Add to db
            walkDomain = await walkRepository.AddAsync(walkDomain);

            // Convert back to domain
            WalkDTO walkDTO = mapper.Map<WalkDTO>(walkDomain);

            // Return DTO
            //This does not work??
            return CreatedAtAction(nameof(GetAsync), new { id = walkDTO.Id }, walkDTO);
            //return Ok(walkDTO);

        }

        // must retrun added at action
        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id, [FromBody]UpdateWalkRequest walk)
        {
            // Validate request
            bool valid = await ValidateUpdateAsync(walk);
            if (!valid)
            {
                return BadRequest(ModelState);
            }
            // DTO to domain
            Walk? walkDomain = new Walk()
            {
                Id = id,
                Name = walk.Name,
                Length = walk.Length,
                WalkDifficultyId = walk.WalkDifficultyId,
                RegionId = walk.RegionId
            };

            // Update entity
            walkDomain = await walkRepository.UpdateAsync(walkDomain);

            if(walkDomain == null)
            {
                return NotFound($"Walk with id {id} not found");
            }

            //Domain to Dto
            WalkDTO walkDTO = mapper.Map<WalkDTO>(walkDomain);

            return Ok(walkDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
        {
            //Remove and return entity
            Walk? walk = await walkRepository.DeleteAsync(id);

            if(walk == null)
            {
                return NotFound($"Walk with id {id} was not found");
            }

            //Domain to DTO
            WalkDTO walkDTO = mapper.Map<WalkDTO>(walk);

            return Ok(walkDTO);
        }

        #region Private methods
        
        private async Task<bool> ValidateAddAsync(AddWalkRequest addWalkRequest)
        {
            // These nominal checks are done with FluentValidator
            //if(addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest), $"{nameof(addWalkRequest)} cannot be empty");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name), $"{nameof(addWalkRequest.Name)} cannot be null or whitespace");
            //}

            //if (addWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length), $"{nameof(addWalkRequest.Length)} cannot be zero or negative");
            //}

            Region? foundRegion = await regionRepository.GetAsync(addWalkRequest.RegionId);
            if(foundRegion == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId), $"{nameof(addWalkRequest.RegionId)} is not valid");
            }

            WalkDifficulty? foundWalkDiff = await walkDiffRepo.GetAsync(addWalkRequest.WalkDifficultyId);
            if (foundWalkDiff == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId), $"{nameof(addWalkRequest.WalkDifficultyId)} is not valid");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateUpdateAsync(UpdateWalkRequest updateWalkRequest)
        {
            // These nominal checks are done with FluentValidator
            //if (updateWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest), $"{nameof(updateWalkRequest)} cannot be empty");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Name), $"{nameof(updateWalkRequest.Name)} cannot be null or whitespace");
            //}

            //if (updateWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Length), $"{nameof(updateWalkRequest.Length)} cannot be zero or negative");
            //}

            Region? foundRegion = await regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (foundRegion == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId), $"{nameof(updateWalkRequest.RegionId)} is not valid");
            }

            WalkDifficulty? foundWalkDiff = await walkDiffRepo.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (foundWalkDiff == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId), $"{nameof(updateWalkRequest.WalkDifficultyId)} is not valid");
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
