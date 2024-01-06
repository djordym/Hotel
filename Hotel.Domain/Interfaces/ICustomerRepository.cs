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
        void RemoveCustomerById(int? id);
        void RemoveMember(int? customerId, string memberName, DateOnly memberBirthday);
        void UpdateCustomer(Customer c);
        Customer GetCustomerByEmail(string text);
    }
}
