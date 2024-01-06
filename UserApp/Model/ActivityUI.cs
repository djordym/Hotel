using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Model
{
    public class ActivityUI
    {
        public int Id { get; set; }
        public int OrganizerId { get; set; }
        public string Name { get; set; }
        public DateTime Fixture { get; set; }
        public int DescriptionId { get; set; }
        public int NrOfPlaces { get; set; }
        public int Duration { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
        public int Discount { get; set; }
    }
}
