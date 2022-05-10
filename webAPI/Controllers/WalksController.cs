using AutoMapper;
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

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        [ActionName("get-all")]
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
        public async Task<IActionResult> AddAsync([FromBody]AddWalkRequest walk)
        {
            // Check if region id is valid
            Region? region = await regionRepository.GetAsync(walk.RegionId);

            if (region == null)
            {
                return NotFound($"Region id {walk.RegionId} is not Valid");
            }

            // Must still check if walkdifficulty is valid

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
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id, [FromBody]UpdateWalkRequest walk)
        {
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
    }
}
