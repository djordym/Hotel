using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hotel.Domain.Model
{
    public class Organizer
    {
        
        private int _id;
        private string _name;
        private ContactInfo _contactInfo;
        public int Id { get { return _id; } set { if (value <= 0) throw new OrganizerException("invalid id"); _id = value; } }
        public string Name { get { return _name; } set { if (string.IsNullOrWhiteSpace(value)) throw new OrganizerException("name is empty"); _name = value; } }

        public ContactInfo ContactInfo { get { return _contactInfo; } set { if (value == null) throw new OrganizerException("contactinfo null"); _contactInfo = value; } }

        public List<Activity> Activities { get; set; } = new List<Activity>();

    }
}
