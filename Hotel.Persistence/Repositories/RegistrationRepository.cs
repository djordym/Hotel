using Hotel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private string connectionString;
        public RegistrationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

    }
}
