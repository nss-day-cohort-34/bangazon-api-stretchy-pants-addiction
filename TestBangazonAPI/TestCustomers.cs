using System.Net;
using Newtonsoft.Json;
using Xunit;
using BangazonAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Text;

namespace TestBangazonAPI
{
    public class TestCustomers
    {
        [Fact]
        //====================
        // Test Get Customers
        //======================
        public async Task Test_Get_All_Customers()
        {
            using (var client = new APIClientProvider().Client)
            {
                /*
                    ARRANGE
                */


                /*
                    ACT
                */
                var response = await client.GetAsync("/api/customers");


                string responseBody = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<Customer>>(responseBody);
                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(customers.Count > 0);
            }
        }
        [Fact]
        //=========================
        //Test Create Customer
        //==========================
        public async Task Test_Create_Customer()
        {
            /*
                Generate a new instance of an HttpClient that you can
                use to generate HTTP requests to your API controllers.
                The `using` keyword will automatically dispose of this
                instance of HttpClient once your code is done executing.
            */
            try
            {
                using (var client = new APIClientProvider().Client)
                {
                    /*
                        ARRANGE
                    */

                    // Construct a new student object to be sent to the API

                    Customer Jack = new Customer
                    {
                        FirstName = "Jack",
                        LastName = "Taylor",
                        CreationDate = new DateTime(),
                        LastActiveDate = new DateTime()

                    };

                    // Serialize the C# object into a JSON string
                    var jackAsJSON = JsonConvert.SerializeObject(Jack);


                    /*
                        ACT
                    */

                    // Use the client to send the request and store the response
                    var response = await client.PostAsync(
                        "/api/customers",
                        new StringContent(jackAsJSON, Encoding.UTF8, "application/json")
                    );

                    // Store the JSON body of the response
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON into an instance of Animal
                    var newJack = JsonConvert.DeserializeObject<Customer>(responseBody);


                    /*
                        ASSERT
                    */

                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                    Assert.Equal("Jack", newJack.FirstName);
                    Assert.Equal("Taylor", newJack.LastName);
                    Assert.Equal(new DateTime(2010, 8, 18), newJack.CreationDate);
                    Assert.Equal(new DateTime(2012, 8, 18), newJack.LastActiveDate);
                }
            }
            catch (Exception e)
            {

            }

        }
        //=======================
        //Test Modify Customer
        //========================
        [Fact]
        public async Task Test_Modify_Customer()
        {
            // New last name to change to and test
            string newFirstName = "Jack";

            using (var client = new APIClientProvider().Client)
            {
                /*
                    PUT section
                */
                Customer modifiedJack = new Customer
                {
                    FirstName = newFirstName,
                    LastName = "Styles",
                    CreationDate = (new DateTime(2010, 8, 18)),
                    LastActiveDate = (new DateTime(2019, 8, 18))

                };
                var modifiedJackAsJSON = JsonConvert.SerializeObject(modifiedJack);

                var response = await client.PutAsync(
                    "/api/customers/1",
                    new StringContent(modifiedJackAsJSON, Encoding.UTF8, "application/json")
                );
                string responseBody = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);


                /*
                    GET section
                    Verify that the PUT operation was successful
                */
                var getJack = await client.GetAsync("/api/customers/1");
                getJack.EnsureSuccessStatusCode();

                string getJackBody = await getJack.Content.ReadAsStringAsync();
                Customer newJack = JsonConvert.DeserializeObject<Customer>(getJackBody);

                Assert.Equal(HttpStatusCode.OK, getJack.StatusCode);
                Assert.Equal(newFirstName, newJack.FirstName);
            }
        }
    }
}
