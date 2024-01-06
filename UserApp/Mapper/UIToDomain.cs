using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Model;

namespace UserApp.Mapper
{
    public static class UIToDomain
    {
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
