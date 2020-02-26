using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace webapi
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

    }
}
