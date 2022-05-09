using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webAPI.Models.Domain;
using webAPI.Models.DTO;
using webAPI.Repository;
/* For status code methods see: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-6.0
 */

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //List<Region> regions = this._regionRepository.GetAll();
            var regions = await this.regionRepository.GetAllAsync();

            /*It is considered bad practise to expose the domain model to the client, if the domain model is changed then we will send breaking changes to the client. 
             * The solution is to use a DTO (data transfer model) and expose that to the client instead.
             * This can be done automatically using automapper.
             */
            //List<Models.Domain.Region> regionsDTO = new List<Models.Domain.Region>();

            //regions.ToList().ForEach(domainRegion =>
            //{
            //    regionsDTO.Add(new Models.Domain.Region()
            //    {
            //        Id = domainRegion.Id,
            //        Name = domainRegion.Name,
            //        Code = domainRegion.Code,
            //        Area = domainRegion.Area,
            //        Lat = domainRegion.Lat,
            //        Long = domainRegion.Long,
            //        Population = domainRegion.Population
            //    });
            //});

            /*This is equivalent to above code except done using AutoMapper
             * Must specify the generic type of mapper.map method as the destination type, it takes the source object to be mapped as its argument. 
             */
            var regionsDTO = mapper.Map<List<Models.DTO.RegionDTO>>(regions);

            return Ok(regionsDTO);
        } 

        [HttpGet]
        // Route parameter specifies that the id must be a guid or it will not be accepted
        [Route("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var region = await this.regionRepository.GetAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            var regionsDTO = mapper.Map<Models.DTO.RegionDTO>(region);

            return Ok(regionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddRegionRequest addRegionRequest)
        {
            // Request(DTO) to domain model
            Region regionDomain = new Region()
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population
            };
            // Pass details to repository
            regionDomain = await this.regionRepository.AddAsync(regionDomain);

            // Domain to DTO 
            //RegionDTO regionDTO = new RegionDTO()
            //{
            //    Id = regionDomain.RegionId,
            //    Name = regionDomain.Name,
            //    Area = regionDomain.Area,
            //    Code = regionDomain.Code,
            //    Lat = regionDomain.Lat,
            //    Long=regionDomain.Long,
            //    Population=regionDomain.Population
            //};

            RegionDTO regionDTO = mapper.Map<RegionDTO>(regionDomain);

            return CreatedAtAction(nameof(Get), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            // Get region from database
            Region? region = await this.regionRepository.DeleteAsync(id);
            
            if(region == null)
            {
                return NotFound($"Entity with id: {id} not Found");
            }
            
            RegionDTO regionDTO = mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateRegionRequest updateRegionRequest)
        {
            // DTO to domain
            Region region = new Region()
            {
                RegionId = id,
                Name = updateRegionRequest.Name,
                Code=updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat=updateRegionRequest.Lat,
                Long=updateRegionRequest.Long,
                Population=updateRegionRequest.Population
            };

            Region updatedRegion = await regionRepository.UpdateAsync(id, region);

            if(updatedRegion == null)
            {
                return NotFound($"Entity with id = {id} not found");
            }

            RegionDTO regionDTO = mapper.Map<RegionDTO>(updatedRegion);

            return Ok(regionDTO);
        }

        //This code was auto-generated with API controller with read/write actions template
        //// GET: api/<RegionsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<RegionsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<RegionsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RegionsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RegionsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
