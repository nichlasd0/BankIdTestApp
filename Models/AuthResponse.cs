using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankIdTestApp.Models
{
    public class AuthResponse
    {
        public string orderRef { get; set; }
        public string autoStartToken { get; set; }
        public string qrStartToken { get; set; }
        public string qrStartSecret { get; set; }
    }
}
