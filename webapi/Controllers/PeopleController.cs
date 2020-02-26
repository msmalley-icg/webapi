using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController
    {
        private List<Person> _people = new List<Person>();

        public PeopleController()
        {
            _people.Add(new Person{Name = "matthew", Role = "PM"});
            _people.Add(new Person { Name = "mo", Role = "Dev" });
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _people;
        }
    }
}
