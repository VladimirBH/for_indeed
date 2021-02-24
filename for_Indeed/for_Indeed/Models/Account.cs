using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace for_Indeed.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int Id_Client { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
