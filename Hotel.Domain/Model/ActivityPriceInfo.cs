using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class ActivityPriceInfo
    {
        public ActivityPriceInfo()
        {
        }

        public ActivityPriceInfo(int priceAdult, int priceChild, int discount)
        {
            PriceAdult = priceAdult;
            PriceChild = priceChild;
            Discount = discount;
        }

        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
        public int Discount { get; set; }
    }
}
