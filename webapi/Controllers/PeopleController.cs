using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webapi.Controllers;

namespace webapi
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleContext _context;
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(PeopleContext context, ILogger<PeopleController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            _logger.LogInformation("Getting people (controller)");
            return await _context.People.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Person>> AddPerson(Person person)
        {
            _logger.LogInformation("Adding person (controller)");
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return CreatedAtAction("AddPerson", new {id = person.Id}, person);
        }
    }
}
