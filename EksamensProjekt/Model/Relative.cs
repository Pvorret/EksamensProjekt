using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model
{
    class Relative : Person
    {
        public string Email { get; set; }
        public TimeSpan NotAvailable { get; set; }

        public Relative(string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, List<string> NotAvailable)
        {
            this.Name = name;
            this.CprNr = cprNr;

        }
    }
}
