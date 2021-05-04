using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankIdTestApp.Models
{
    public class UserResponse
    {
        public string personalNumber { get; set; }
        public string name { get; set; }
        public string givenName { get; set; }
        public string surname { get; set; }
    }
}
