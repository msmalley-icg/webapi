using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace webapi.Controllers
{
    public class PeopleHub : Hub
    {
        private readonly ILogger<PeopleHub> _logger;
        private readonly PeopleContext _context;

        public PeopleHub(ILogger<PeopleHub> logger, PeopleContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<string> SayHello(string name)
        {
            _logger.LogInformation($"Saying hello to: {name}");
            await Clients.All.SendAsync($"Hello: {name}");
            return $"Hello: {name}";
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            _logger.LogInformation("Getting people");
            return await _context.People.ToListAsync();
        }

        public async Task AddPersonViaStrings(string name, string role)
        {
            _logger.LogInformation($"Adding person via strings {name}");
            _context.People.Add(new Person { Name = name, Role = role });
            await _context.SaveChangesAsync();
        }

        public async Task AddPersonJson(Person person)
        {
            _logger.LogInformation($"Adding person via JSON {person.Name}");
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }
    }
}
