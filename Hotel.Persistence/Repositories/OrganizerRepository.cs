using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private string connectionString;

        public OrganizerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Organizer GetOrganizerByEmail(string email)
        {

            try
            {
                
                string sql = @"SELECT o.*, a.*, ad.*, api.*
                        FROM Organizer o 
                        LEFT JOIN Activity a ON o.id = a.organizerId 
                        LEFT JOIN ActivityDescription ad ON a.descriptionId = ad.id 
                        LEFT JOIN ActivityPriceInfo api ON a.priceInfoId = api.id 
                        WHERE o.email = @Email";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Organizer organizer = new Organizer();
                        organizer.Id = (int)reader["id"];
                        organizer.Name = (string)reader["name"];
                        ContactInfo contactInfo = new ContactInfo(
                            email,
                            (string)reader["phone"],
                            new Address((string)reader["address"])
                        );
                        organizer.ContactInfo = contactInfo;

                    }
                }

            }
    }
    }

}
