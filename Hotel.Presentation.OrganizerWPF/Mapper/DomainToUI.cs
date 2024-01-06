using Hotel.Domain.Model;
using Hotel.Presentation.OrganizerWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.OrganizerWPF.Mapper
{
    public static class DomainToUI
    {
        public static OrganizerUI OrganizerToOrganizerUI(Organizer organizer)
        {
            var organizerUI = new OrganizerUI
            {
                Id = organizer.Id,

                Name = organizer.Name,
                Email = organizer.ContactInfo.Email,
                Phone = organizer.ContactInfo.Phone,
                City = organizer.ContactInfo.Address.City,
                PostalCode = organizer.ContactInfo.Address.PostalCode,
                Street = organizer.ContactInfo.Address.Street,
                HouseNumber = organizer.ContactInfo.Address.HouseNumber,
                Activities = new ObservableCollection<ActivityUI>()
            };
            foreach (var activity in organizer.Activities)
            {
                organizerUI.Activities.Add(ActivityToActivityUI(activity));
            }
            return organizerUI;
        }

        public static ActivityUI ActivityToActivityUI(Activity activity)
        {
            return new ActivityUI
            {
                Id = activity.Id,
                OrganizerId = activity.OrganizerId,
                Fixture = activity.Fixture,
                NrOfPlaces = activity.NrOfPlaces,
                DescriptionId = activity.Description.Id,
                Duration = activity.Description.Duration,
                Location = activity.Description.Location,
                Description = activity.Description.Discription,
                PriceAdult = activity.PriceInfo.PriceAdult,
                PriceChild = activity.PriceInfo.PriceChild,
                Discount = activity.PriceInfo.Discount
            };
        }
    }
}
