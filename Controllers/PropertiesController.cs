using Microsoft.AspNetCore.Mvc;
using OakDevelopments.Data;
using OakDevelopments.Models;
using System.Linq;

namespace OakDevelopments.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PropertiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/properties
        [HttpGet]
        public IActionResult GetProperties()
        {
            var properties = _context.Properties.ToList();
            return Ok(properties);
        }

        // GET: api/properties/5
        [HttpGet("{id}")]
        public IActionResult GetProperty(int id)
        {
            var property = _context.Properties.Find(id);
            if (property == null)
                return NotFound();

            return Ok(property);
        }

        // POST: api/properties
        [HttpPost]
        public IActionResult AddProperty(Property property)
        {
            _context.Properties.Add(property);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, property);
        }
    }
}