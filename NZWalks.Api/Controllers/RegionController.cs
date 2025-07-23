using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domains;

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
            var Regions = _context.Regions.ToList();

            return Ok(Regions);


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
