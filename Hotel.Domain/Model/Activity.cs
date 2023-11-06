using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime Fixture {  get; set; }

        public int NrOfPlaces { get; set; }
        public List<ActivityDescription> Description { get; set; }
        public List<ActivityPriceInfo> PriceInfo { get; set;}
    }
}
