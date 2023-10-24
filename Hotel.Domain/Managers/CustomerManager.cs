using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IReadOnlyList<Customer> GetCustomersBy(string? filter)
        {
            try
            {
                return _customerRepository.GetCustomersBy(filter);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("GetCustomers", ex);
            }
        }

        public void UpdateCustomerInformation(int? id, string name, string email, string phone, string address)
        {
            if(id == null)
            {
                throw new ArgumentNullException("id");
            }
            else
            {
                _customerRepository.UpdateCustomerById(id, name, email, phone, address);
            }
        }
        
    }
}
