using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Activity
    {
        public Activity()
        {
        }

        public Activity(int id, DateTime fixture, int nrOfPlaces, ActivityDescription description, ActivityPriceInfo priceInfo, int organizerId)
        {
            Id = id;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            Description = description;
            PriceInfo = priceInfo;
            OrganizerId = organizerId;

        }


        public int Id { get; set; }
        public int OrganizerId { get; set; }
        //should be in future
        private DateTime _fixture;
        public DateTime Fixture
        {
            get => _fixture;
            set => _fixture = value > DateTime.Now ? value : throw new ArgumentException("Fixture must be in future");
        }
        //should be positive    
        private int _nrOfPlaces;
        public int NrOfPlaces {
            get => _nrOfPlaces;
            set => _nrOfPlaces = value > 0 ? value : throw new ArgumentException("NrOfPlaces must be positive");
        }

        public ActivityDescription Description { get; set; }
        public ActivityPriceInfo PriceInfo { get; set; }
    }
}
