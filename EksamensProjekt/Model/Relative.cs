using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model
{
    public class Relative : Person
    {
        public string Email { get; set; }
        public List<Time> NotAvailableTimes { get; set; }

        public Relative(string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, List<Time> notAvailableTimes)
        {
            this.Name = name;
            this.CprNr = cprNr;
            this.Gender = gender;
            this.Age = age;
            this.Address = address;
            this.City = city;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.NotAvailableTimes = notAvailableTimes;
        }
        public Relative(string name, string cprNr, string phoneNumber, string address, string city)//Stefan
        {
            this.CprNr = cprNr;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.City = city;
        }
        public Relative(string name, string cprNr)
        {
            this.CprNr = cprNr;
            this.Name = name;
            
        }
    }
}
