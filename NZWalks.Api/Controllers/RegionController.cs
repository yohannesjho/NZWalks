using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domains;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext _context;
        public RegionController(NZWalksDbContext dbContext)
        {
            _context = dbContext;
        }
        [HttpGet]
        public IActionResult GetlAll()
        {
            var RegionsDomainModel = _context.Regions.ToList();

            var RegionsDto = new List<RegionDto>();
            foreach(var Reg in RegionsDomainModel)
            {
                RegionsDto.Add(new RegionDto
                {
                    Id = Reg.Id,
                    Name = Reg.Name,
                    Code = Reg.Code,
                    RegionImgUrl = Reg.RegionImgUrl

                });
            }

            return Ok(RegionsDto);


        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var Region = _context.Regions.Find(id);

                if(Region == null)
            {
                return NotFound();
            }

            return Ok(Region);
        }
    }
}
