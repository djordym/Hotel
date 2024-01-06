using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class ActivityDescription
    {
        public ActivityDescription()
        {
        }

        public ActivityDescription(int id,int duration, string location, string discription, string name)
        {

            Id = id;
            Duration = duration;
            Location = location;
            Discription = discription;
            Name = name;
        }

        public int Id { get; set; }
        public int Duration { get; set; }
        public string Location { get; set; }
        public string Discription { get; set; }
        public string Name { get; set; }

    }
}
