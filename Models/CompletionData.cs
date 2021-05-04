using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankIdTestApp.Models
{
    public class CompletionData
    {
        public UserResponse user { get; set; }
        public Device device { get; set; }
        public Cert cert { get; set; }
        public string signature { get; set; }
        public string ocspResponse { get; set; }
    }
}
