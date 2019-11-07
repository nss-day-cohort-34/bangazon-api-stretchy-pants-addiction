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
    public class OrdersController : ControllerBase
    {
        private readonly IConfiguration _config;

        public OrdersController(IConfiguration config)
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

        // GET api/orders
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using SqlConnection conn = Connection;
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {

                cmd.CommandText = @"SELECT ID, CustomerId, PaymentTypeId
                                    FROM [Order]";

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                List<Order> orders = new List<Order>();
                while (reader.Read())
                {

                    Order order = new Order
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        PaymentTypeId = null
                    };

                    var IsPaymentNull = reader.IsDBNull(reader.GetOrdinal("PaymentTypeId"));
                    if (!IsPaymentNull)
                    {
                        order.PaymentTypeId = reader.GetInt32(reader.GetOrdinal("PaymentTypeId"));
                    }

                    orders.Add(order);
                }

                reader.Close();

                return Ok(orders);
            }
        }

        // GET api/Orders/5
        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT ID, CustomerId, PaymentTypeId
                                    FROM [Order]
                                    WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    Order order = null;
                    if (reader.Read())
                    {
                        order = new Order
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            PaymentTypeId = null
                        };

                        var IsPaymentNull = reader.IsDBNull(reader.GetOrdinal("PaymentTypeId"));
                        if (!IsPaymentNull)
                        {
                            order.PaymentTypeId = reader.GetInt32(reader.GetOrdinal("PaymentTypeId"));
                        }
                    }

                    reader.Close();

                    return Ok(order);
                }
            }
        }

        // POST api/orders
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // More string interpolation
                    cmd.CommandText = @"
                        INSERT INTO [Order] (CustomerId, PaymentTypeId)
                        OUTPUT INSERTED.Id
                        VALUES ( @CustomerId, @PaymentTypeId)
                    ";
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", order.CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", order.PaymentTypeId));

                    order.Id = (int)await cmd.ExecuteScalarAsync();

                    return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
                }
            }
        }

        // PUT api/order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] Order order)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            UPDATE [Order] SET CustomerId = @CustomerId, PaymentTypeId = @PaymentTypeId
                            WHERE Id = @id
                        ";
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@CustomerId", order.CustomerId));
                        cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", order.PaymentTypeId));

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
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE api/order/5
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
                        cmd.CommandText = @"DELETE FROM [Order] WHERE Id = @id";
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
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool OrderExists(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id FROM Order WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    return reader.Read();
                }
            }
        }
    }
}
