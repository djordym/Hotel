using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hotel.Domain.Model
{

    public class ContactInfo
    {
        public ContactInfo(string email, string phone, Address address)
        {
            Email = email;
            Phone = phone;
            Address = address;
        }
        private string _email;
        private string _phone;
        private Address _address;
        public string Email { get { return _email; } set { if (string.IsNullOrEmpty(value) || !value.Contains('@')) throw new ContactInfoException("email is empty"); _email = value; } }
        public string Phone { get { return _phone; } set { if (string.IsNullOrWhiteSpace(value)) throw new ContactInfoException("phone is empty"); _phone = value; } }
        public Address Address { get { return _address; } set { if (value == null) throw new ContactInfoException("address is null"); _address = value; } }
    }

}
