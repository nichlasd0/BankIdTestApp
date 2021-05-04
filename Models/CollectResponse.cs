using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankIdTestApp.Models
{
    public class CollectResponse
    {
        public string orderRef { get; set; }
        public string status { get; set; }
        public string hintCode { get; set; }
        public CompletionData completionData { get; set; }
    }
}
