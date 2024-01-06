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
    public class UserManager
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly ICustomerRepository _customerRepository;
        public UserManager(IRegistrationRepository registrationRepository, ICustomerRepository customerRepository)
        {
            _registrationRepository = registrationRepository;
            _customerRepository = customerRepository;
        }

        public Customer GetCustomerByEmail(string text)
        {
            try
            {
            return _customerRepository.GetCustomerByEmail(text);
            }catch(CustomerException ex)
            {
                throw;
            }
        }
    }
}
