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

        public void AddCustomer(Customer c)
        {
            try
            {
                _customerRepository.AddCustomer(c);
            }catch (Exception ex)
            {
                throw new CustomerManagerException("addcustomer", ex);
            }
        }

        public int GetCustomerIdByEmail(string email)
        {
            try
            {
                return _customerRepository.GetCustomerIdByEmail(email);
            }catch(Exception ex)
            {
                throw new CustomerManagerException($"getcustomeridbyemail", ex);
            }
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

        public void RemoveCustomerById(int? id)
        {
            try
            {
                _customerRepository.RemoveCustomerById(id);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("removecustomer", ex);
            }
        }

        public void RemoveMember(int? customerId, string memberName, DateOnly memberBirthday)
        {
            try
            {
                _customerRepository.RemoveMember(customerId, memberName, memberBirthday);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("removemember", ex);
            }
        }

        public void UpdateCustomer(Customer c)
        {
            try
            {
                _customerRepository.UpdateCustomer(c);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("addcustomer", ex);
            }
        }

        
        
    }
}
