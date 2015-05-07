using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model
{
    public class Relative : Person
    {
        public string CitizenCprNr { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> NotAvailable { get; set; }

        public Relative(string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, Dictionary<string, string> NotAvailable)
        {
            this.Name = name;
            this.CprNr = cprNr;
            this.Gender = gender;
            this.Age = age;
            this.Address = address;
            this.City = city;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.NotAvailable = NotAvailable;
            
        }

        public Relative(string citizenCprNr, string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, Dictionary<string, string> NotAvailable) {

            this.CitizenCprNr = citizenCprNr;
            this.Name = name;
            this.CprNr = cprNr;
            this.Gender = gender;
            this.Age = age;
            this.Address = address;
            this.City = city;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
        }
        public Relative(string cprNr, string name, string address, string city)//Stefan
        {
            this.CprNr = cprNr;
            this.Name = name;
            this.Address = address;
            this.City = city;
        }
    }
}
