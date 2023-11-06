using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.OrganizerWPF.Model
{
    class ActivityUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Fixture {  get; set; }
        public int NrOfPlaces { get; set; }
        public int Duration { get; set; }
        public string Location { get; set; }
        //public DiscriptionUI Discription { get; set; }
        //public PriceUI Pricing { get; set; }
        
    }
}
