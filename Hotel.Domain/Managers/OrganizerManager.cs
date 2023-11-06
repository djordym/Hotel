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
        private IOrganizerRepository _organizerRepository;
        public OrganizerManager(IOrganizerRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

        public Organizer GetOrganizerByEmail(string email)
        {
            try
            {
                _organizerRepository.GetOrganizerByEmail(email);
            }catch (Exception ex)
            {
                OrganizerException("getorganbyemail", ex);
            }
        }
    }
}
