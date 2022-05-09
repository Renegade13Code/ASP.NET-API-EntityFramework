using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webAPI.Models.Domain;
using webAPI.Repository;

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
        public IActionResult Get()
        {
            //List<Region> regions = this._regionRepository.GetAll();
            var regions = this.regionRepository.GetAll();

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
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
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
