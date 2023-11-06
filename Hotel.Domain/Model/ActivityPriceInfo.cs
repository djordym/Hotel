using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class ActivityPriceInfo
    {
        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
        public int DiscountAdult { get; set; }
        public int DiscountChild { get; set; }
    }
}
