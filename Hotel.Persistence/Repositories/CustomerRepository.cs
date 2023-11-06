using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Customer> GetCustomersBy(string filter)
        {
            try
            {
                Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
                string sql;
                if (string.IsNullOrEmpty(filter))
                {
                    sql = "select t1.id,t1.email,t1.name customername,t1.address,t1.phone,t2.name membername,t2.birthday\r\nfrom customer t1 \r\nleft join (select * from member where status=1) t2 on t1.id=t2.customerId\r\nwhere t1.status=1";
                }
                else
                {
                    sql = "select t1.id,t1.email,t1.name customername,t1.address,t1.phone,t2.name membername,t2.birthday\r\nfrom customer t1 \r\nleft join (select * from member where status=1) t2 on t1.id=t2.customerId\r\nwhere t1.status=1 and (t1.id like @filter or t1.name like @filter or t1.email like @filter)";
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            if (!customers.ContainsKey(id)) //member toevoegen
                            {
                                customers.Add(id, new Customer((string)reader["customername"], (int)reader["id"], new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"]))));
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                customers[id].AddMember(new Member((string)reader["membername"], DateOnly.FromDateTime((DateTime)reader["birthday"])));
                            }
                        }
                    return customers.Values.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("GetCustomer", ex);
            }
        }
        public void AddCustomer(Customer customer)
        {
            try
            {
                string SQL = "INSERT INTO Customer(name,email,phone,address,status) output INSERTED.ID VALUES(@name,@email,@phone,@address,@status) ";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        //write customer table
                        cmd.CommandText = SQL;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@name", customer.Name);
                        cmd.Parameters.AddWithValue("@email", customer.ContactInfo.Email);
                        cmd.Parameters.AddWithValue("@phone", customer.ContactInfo.Phone);
                        cmd.Parameters.AddWithValue("@address", customer.ContactInfo.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@status", 1);
                        int id = (int)cmd.ExecuteScalar();
                        //write members table
                        SQL = "INSERT INTO member(name,birthday,customerid,status) VALUES(@name,@birthday,@customerid,@status) ";
                        cmd.CommandText = SQL;

                        foreach (Member member in customer.GetMembers())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@name", member.Name);
                            cmd.Parameters.AddWithValue("@birthday", member.BirthDay.ToDateTime(TimeOnly.MinValue));
                            cmd.Parameters.AddWithValue("@customerid", id);
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("AddCustomer", ex);
            }
        }


        public void UpdateCustomer(Customer c)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        // update customer table
                        string SQL = "UPDATE Customer SET name = @name, email = @email, phone = @phone, address = @address WHERE id = @id";
                        cmd.CommandText = SQL;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@id", c.Id);
                        cmd.Parameters.AddWithValue("@name", c.Name);
                        cmd.Parameters.AddWithValue("@email", c.ContactInfo.Email);
                        cmd.Parameters.AddWithValue("@phone", c.ContactInfo.Phone);
                        cmd.Parameters.AddWithValue("@address", c.ContactInfo.Address.ToAddressLine());
                        cmd.ExecuteNonQuery();

                        // update members table
                        SQL = "SELECT COUNT(*) FROM member WHERE customerid = @customerid AND name = @name AND birthday = @birthday";
                        cmd.CommandText = SQL;

                        foreach (Member member in c.GetMembers())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@name", member.Name);
                            cmd.Parameters.AddWithValue("@birthday", member.BirthDay.ToDateTime(TimeOnly.MinValue));
                            cmd.Parameters.AddWithValue("@customerid", c.Id);
                            int count = (int)cmd.ExecuteScalar();

                            if (count == 0)
                            {
                                string insertSQL = "INSERT INTO member(name,birthday,customerid,status) VALUES(@name,@birthday,@customerid,@status) ";
                                cmd.CommandText = insertSQL;
                                cmd.Parameters.AddWithValue("@status", 1);
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = SQL; // reset back to SELECT command text
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new CustomerRepositoryException("UpdateCustomerById", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("UpdateCustomerById", ex);
            }
        }

        public int GetCustomerIdByEmail(string email)
        {
            try
            {
                string sql = "Select id from customer where email = @email";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    } else
                    {
                        throw new CustomerRepositoryException("Customer not found");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("GetCustomerby email", ex);
            }
        }

        public void RemoveCustomerById(int? id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        // update customer status to 0 (inactive)
                        string SQL = "UPDATE Customer SET Status = 0 WHERE id = @id";
                        cmd.CommandText = SQL;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new CustomerRepositoryException("RemoveCustomerById", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("RemoveCustomerById", ex);
            }
        }

        public void RemoveMember(int? customerId, string memberName, DateOnly memberBirthday)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        //update member status to 0 (inactive)
                        string SQL = "UPDATE Member SET status = 0 WHERE customerId = @customerId AND name = @name AND birthday = @birthday;";
                        cmd.CommandText = SQL;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@name", memberName);
                        cmd.Parameters.AddWithValue("@birthday", memberBirthday.ToDateTime(TimeOnly.MinValue));

                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("removemember", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("removemember", ex);
            }
        }
    }
}
