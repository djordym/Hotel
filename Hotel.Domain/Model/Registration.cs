using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Registration
    {
        public Registration(int id, Activity activity, Customer customer, List<Member> members)
        {
            Id = id;
            Activity = activity;
            Customer = customer;
            Members = members;
        }

        public int Id { get; set; }
        public Activity Activity { get; set; }
        public Customer Customer { get; set; }
        public List<Member> Members { get; set; }
    }
}
