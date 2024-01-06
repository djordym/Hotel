using Hotel.Domain.Model;
using Hotel.Presentation.OrganizerWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.OrganizerWPF.Mapper
{
    public static class UIToDomain
    {
        public static Organizer OrganizerUIToOrganizer(OrganizerUI organizerUI)
        {
            var organizer = new Organizer
            {
                Id = organizerUI.Id,
                Name = organizerUI.Name,
                ContactInfo = new ContactInfo(organizerUI.Email, organizerUI.Phone, new Address(organizerUI.City, organizerUI.Street, organizerUI.PostalCode, organizerUI.HouseNumber)),
                Activities = new List<Activity>()
            };
            foreach (var activityUI in organizerUI.Activities)
            {
                organizer.Activities.Add(ActivityUIToActivity(activityUI));
            }
            return organizer;
        }

        public static Activity ActivityUIToActivity(ActivityUI activityUI)
        {
            return new Activity
            {
                Id = activityUI.Id,
                Fixture = activityUI.Fixture,
                NrOfPlaces = activityUI.NrOfPlaces,
                OrganizerId = activityUI.OrganizerId,
                Description = new ActivityDescription
                {
                    Id = activityUI.DescriptionId,
                    Duration = activityUI.Duration,
                    Location = activityUI.Location,
                    Discription = activityUI.Description,
                    Name = activityUI.Name
                },
                PriceInfo = new ActivityPriceInfo
                {
                    PriceAdult = activityUI.PriceAdult,
                    PriceChild = activityUI.PriceChild,
                    Discount = activityUI.Discount
                }
            };
        }
    }
}
