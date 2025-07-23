using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domains;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _context;
        public RegionsController(NZWalksDbContext dbContext)
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

        [HttpPost]
        public IActionResult Create([FromBody] RegionCreateRequestDto dto)
        {
            var RegionDomainModel = new Region()
            {
                Name = dto.Name,
                Code = dto.Code,
                RegionImgUrl = dto.RegionImgUrl,
            };

            _context.Regions.Add(RegionDomainModel);
            _context.SaveChanges();

            var RegionDto = new RegionDto()
            {
                Id = RegionDomainModel.Id,
                Name = RegionDomainModel.Name,
                RegionImgUrl = RegionDomainModel.RegionImgUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = RegionDomainModel.Id }, RegionDto);

        }

        [HttpPut("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] RegionUpdateRequestDto dto)
        {
            var ExistingRegionDomainModel = _context.Regions.FirstOrDefault(x => x.Id == id);

            if(ExistingRegionDomainModel == null)
            {
                return NotFound();
            }

            ExistingRegionDomainModel.Name = dto.Name;
            ExistingRegionDomainModel.Code = dto.Code;
            ExistingRegionDomainModel.RegionImgUrl = dto.RegionImgUrl;

            _context.SaveChanges();

            var RegionDto = new RegionDto()
            {
                Name = ExistingRegionDomainModel.Name,
                Code = ExistingRegionDomainModel.Code,
                RegionImgUrl = ExistingRegionDomainModel.RegionImgUrl
            };

            return Ok(RegionDto);

        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var ExistingRegionDomainModel = _context.Regions.FirstOrDefault(x => x.Id == id);

            if(ExistingRegionDomainModel == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(ExistingRegionDomainModel);
            _context.SaveChanges();

            return Ok();
        }
        
    }
}
