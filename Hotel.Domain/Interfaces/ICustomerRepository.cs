using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        int GetCustomerIdByEmail(string email);
        List<Customer> GetCustomersBy(string filter);
        void UpdateCustomer(Customer c);
    }
}
