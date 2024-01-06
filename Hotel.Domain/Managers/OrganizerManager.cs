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
    public class OrganizerManager
    {
        private readonly IOrganizerRepository _organizerRepository;
        public OrganizerManager(IOrganizerRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

        public void AddActivity(Activity domainActivity)
        {
            try
            {
                _organizerRepository.AddActivity(domainActivity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Organizer GetOrganizerByEmail(string email)
        {
            try
            {
                return _organizerRepository.GetOrganizerByEmail(email);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RemoveActivityById(int id)
        {
            try
            {
                _organizerRepository.RemoveActivityById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
