using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CustomersController(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET api/customers?products
        [HttpGet]
        public async Task<IActionResult> Get(string include)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    if (include == "products")
                    {

                        cmd.CommandText = @"SELECT c.Id, c.FirstName, c.LastName, c.CreationDate, c.LastActiveDate, p.Id AS ProductId, Title, Description, Price, Quantity
                        FROM Customer c
                         JOIN Product p ON c.Id = p.CustomerId";



                        //cmd.CommandText = @"SELECT Id, FirstName, LastName, CreationDate, LastActiveDate
                        //FROM Customer c";


                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
                        while (reader.Read())
                        {
                            int customerId = reader.GetInt32(reader.GetOrdinal("Id"));
                            if (!customers.ContainsKey(customerId))
                            {
                                Customer customer = new Customer
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                                    LastActiveDate = reader.GetDateTime(reader.GetOrdinal("LastActiveDate")),

                                };

                                customers.Add(customerId, customer);
                            }
                            Customer fromDictionary = customers[customerId];
                            if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
                            {
                                Product aProduct = new Product()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                    Description = reader.GetString(reader.GetOrdinal("Description")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    Quantity = reader.GetInt16(reader.GetOrdinal("Quantity"))
                                };
                                fromDictionary.Products.Add(aProduct);
                            }
                        }
                        reader.Close();

                        return Ok(customers.Values);

                    }else
                    {

                    }
                }
            }
        }

        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT ID, FirstName, LastName, CreationDate, LastActiveDate 
                                      FROM Customer 
                                      WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    Customer customer = null;
                    if (reader.Read())
                    {
                        customer = new Customer
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                            LastActiveDate = reader.GetDateTime(reader.GetOrdinal("LastActiveDate")),

                        };
                    }

                    reader.Close();

                    return Ok(customer);
                }
            }
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        // More string interpolation
                        cmd.CommandText = @"
                        INSERT INTO Customer (FirstName, LastName, CreationDate, LastActiveDate)
                        OUTPUT INSERTED.Id
                        VALUES (@FirstName, @LastName, @CreationDate, @LastActiveDate)
                    ";
                        cmd.Parameters.Add(new SqlParameter("@FirstName", customer.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@LastName", customer.LastName));
                        cmd.Parameters.Add(new SqlParameter("@CreationDate", customer.CreationDate));
                        cmd.Parameters.Add(new SqlParameter("@LastActiveDate", customer.LastActiveDate));

                        customer.Id = (int)await cmd.ExecuteScalarAsync();

                        return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Customer customer)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            UPDATE Customer
                            SET FirstName = @FirstName,
                            LastName = @LastName,
                            CreationDate = @CreationDate,
                            LastActiveDate = @LastActiveDate
                            WHERE Id = @id
                        ";
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@FirstName", customer.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@LastName", customer.LastName));
                        cmd.Parameters.Add(new SqlParameter("@CreationDate", customer.CreationDate));
                        cmd.Parameters.Add(new SqlParameter("@LastActiveDate", customer.LastActiveDate));

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new StatusCodeResult(StatusCodes.Status204NoContent);
                        }

                        throw new Exception("No rows affected");
                    }
                }
            }
            catch (Exception)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM Customer WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return new StatusCodeResult(StatusCodes.Status204NoContent);
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }
            catch (Exception)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CustomerExists(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id FROM Customer WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    return reader.Read();
                }
            }
        }
    }
}
