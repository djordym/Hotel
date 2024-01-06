using Hotel.Domain.Exceptions;
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


        public void AddActivity(Activity domainActivity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert ActivityDescription
                        var descriptionCommand = new SqlCommand("INSERT INTO ActivityDescription (duration, location, description, name) VALUES (@duration, @location, @description, @name); SELECT SCOPE_IDENTITY();", connection, transaction);
                        descriptionCommand.Parameters.AddWithValue("@duration", domainActivity.Description.Duration);
                        descriptionCommand.Parameters.AddWithValue("@location", domainActivity.Description.Location);
                        descriptionCommand.Parameters.AddWithValue("@description", domainActivity.Description.Discription);
                        descriptionCommand.Parameters.AddWithValue("@name", domainActivity.Description.Name);
                        int descriptionId = Convert.ToInt32(descriptionCommand.ExecuteScalar());

                        // Insert ActivityPriceInfo
                        var priceInfoCommand = new SqlCommand("INSERT INTO ActivityPriceInfo (priceAdult, priceChild, discount) VALUES (@priceAdult, @priceChild, @discount); SELECT SCOPE_IDENTITY();", connection, transaction);
                        priceInfoCommand.Parameters.AddWithValue("@priceAdult", domainActivity.PriceInfo.PriceAdult);
                        priceInfoCommand.Parameters.AddWithValue("@priceChild", domainActivity.PriceInfo.PriceChild);
                        priceInfoCommand.Parameters.AddWithValue("@discount", domainActivity.PriceInfo.Discount);
                        int priceInfoId = Convert.ToInt32(priceInfoCommand.ExecuteScalar());

                        // Insert Activity
                        var activityCommand = new SqlCommand("INSERT INTO Activity (fixture, nrOfPlaces, descriptionId, organizerId, priceInfoId) VALUES (@fixture, @nrOfPlaces, @descriptionId, @organizerId, @priceInfoId);", connection, transaction);
                        activityCommand.Parameters.AddWithValue("@fixture", domainActivity.Fixture);
                        activityCommand.Parameters.AddWithValue("@nrOfPlaces", domainActivity.NrOfPlaces);
                        activityCommand.Parameters.AddWithValue("@descriptionId", descriptionId);
                        activityCommand.Parameters.AddWithValue("@organizerId", domainActivity.OrganizerId);
                        activityCommand.Parameters.AddWithValue("@priceInfoId", priceInfoId);
                        activityCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public Organizer GetOrganizerByEmail(string email)
        {
            try
            {

                string sql = @"SELECT o.id as OrganizerId, o.name as OrganizerName, o.email as OrganizerEmail, o.phone as OrganizerPhone, o.address as OrganizerAddress, 
                   a.id as ActivityId, a.fixture, a.nrOfPlaces, a.descriptionId, a.organizerId, a.priceInfoId,
                   ad.duration, ad.location as ActivityLocation, ad.description as ActivityDescription, ad.name as ActivityName,
                   api.priceAdult, api.priceChild, api.discount
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
                    Organizer organizer = null;
                    while (reader.Read())
                    {
                        // organizer object only needs to be initialized once
                        if (organizer == null)
                        {
                            organizer = new Organizer
                            {
                                Id = (int)reader["OrganizerId"],
                                Name = (string)reader["OrganizerName"],
                                ContactInfo = new ContactInfo(
                                    email,
                                    (string)reader["OrganizerPhone"],
                                    new Address((string)reader["OrganizerAddress"])
                                )
                            };
                        }

                        // Check if the current row contains an activity
                        if (reader["ActivityId"] != DBNull.Value)
                        {
                            ActivityPriceInfo priceinfo = new ActivityPriceInfo(
                                (int)reader["priceAdult"],
                                (int)reader["priceChild"],
                                (int)reader["discount"]
                            );

                            ActivityDescription description = new ActivityDescription(
                                (int)reader["descriptionId"],
                                (int)reader["duration"],
                                (string)reader["ActivityLocation"],
                                (string)reader["ActivityDescription"],
                                (string)reader["ActivityName"]
                            );


                            organizer.Activities.Add(new Activity(
                                (int)reader["ActivityId"],
                                (DateTime)reader["fixture"],
                                (int)reader["nrOfPlaces"],
                                description,
                                priceinfo,
                                (int)reader["organizerId"]
                            ));
                        }
                    }
                    if(organizer == null)
                    {
                        throw new OrganizerException("Organizer not found");
                    }
                    return organizer;
                }


            }
            catch(OrganizerException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new OrganizerException("Error while fetching organizer", e);
            }
        }

        public void RemoveActivityById(int id)
        {
            try
            {
                string sql = @"DELETE FROM Activity WHERE id = @Id";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new OrganizerException("Error while removing activity", e);
            }
        }
    }

}
