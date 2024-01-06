using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Model;

namespace UserApp.Mapper
{
    public static class DomainToUI
    {
        public static CustomerUI MapCustomerToCustomerUI(Customer customer)
        {
            var customerUI = new CustomerUI
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.ContactInfo.Email,
                Phone = customer.ContactInfo.Phone,
                Address = customer.ContactInfo.Address.ToString(),
                Members = customer.Members.Select(MapMemberToMemberUI).ToList()
            };
            return customerUI;
        }

        public static MemberUI MapMemberToMemberUI(Member member)
        {
            var memberUI = new MemberUI
            {
                Id = member.Id,
                Name = member.Name,
                BirthDay = DateOnly.FromDateTime(member.BirthDay.ToDateTime(TimeOnly.MinValue))
            };
            return memberUI;
        }

        public static RegistrationUI RegistrationToRegistrationUI(Registration registration)
        {
            var registrationUI = new RegistrationUI
            {
                Id = registration.Id,
                Activity = ActivityToActivityUI(registration.Activity),
                Members = registration.Members.Select(MapMemberToMemberUI).ToList()
            };
            return registrationUI;
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
