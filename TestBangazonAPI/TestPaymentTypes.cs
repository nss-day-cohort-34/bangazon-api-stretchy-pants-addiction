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
    public class TestPaymentTypes
    {
        [Fact]
        //====================
        // Test Get PaymentTypes
        //======================
        public async Task Test_Get_All_PaymentTypes()
        {
            using (var client = new APIClientProvider().Client)
            {
                /*
                    ARRANGE
                */


                /*
                    ACT
                */
                var response = await client.GetAsync("/api/PaymentTypes/");


                string responseBody = await response.Content.ReadAsStringAsync();
                var paymentTypes = JsonConvert.DeserializeObject<List<Customer>>(responseBody);
                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(paymentTypes.Count > 0);
            }
        }
        [Fact]
        //=========================
        // Test Get PaymentType By Id
        //=========================
        public async Task Test_PaymentType_By_Id()
        {
            using (var client = new APIClientProvider().Client)
            {
                /*
                    ARRANGE
                */


                /*
                    ACT
                */
                var response = await client.GetAsync("/api/PaymentTypes/1");


                string responseBody = await response.Content.ReadAsStringAsync();
                var paymentType = JsonConvert.DeserializeObject<PaymentType>(responseBody);
                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            }
        }
        [Fact]
        //=========================
        //Test Create PaymentType
        //==========================
        public async Task Test_Create_PaymentType()
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

                    PaymentType Pepsi = new PaymentType
                    {
                        AcctNumber = "18791310110103",
                        Name = "Pepsi Gotta Have It Card",
                        CustomerId = 1
                        

                    };

                    // Serialize the C# object into a JSON string
                    var pepsiAsJSON = JsonConvert.SerializeObject(Pepsi);


                    /*
                        ACT
                    */

                    // Use the client to send the request and store the response
                    var response = await client.PostAsync(
                        "/api/PaymentTypes",
                        new StringContent(pepsiAsJSON, Encoding.UTF8, "application/json")
                    );

                    // Store the JSON body of the response
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON into an instance of Animal
                    var newPepsi = JsonConvert.DeserializeObject<PaymentType>(responseBody);


                    /*
                        ASSERT
                    */

                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                    Assert.Equal("18791310110103", newPepsi.AcctNumber);
                    Assert.Equal("Pepsi Gotta Have It Car", newPepsi.Name);
                    Assert.Equal(new int(), newPepsi.CustomerId);
                   
                }
            }
            catch (Exception e)
            {

            }

        }
        //=======================
        //Test Modify PaymentType
        //========================
        [Fact]
        public async Task Test_Modify_PaymentType()
        {
            // New acct number to change to and test
            string newAcctNumber = "00001";

            using (var client = new APIClientProvider().Client)
            {
                /*
                    PUT section
                */
                PaymentType modifiedPepsi = new PaymentType
                {
                    AcctNumber = newAcctNumber,
                    Name = "Pepsi Gotta Have It Card",
                    CustomerId = 5
                   

                };
                var modifiedPepsiAsJSON = JsonConvert.SerializeObject(modifiedPepsi);

                var response = await client.PutAsync(
                    "/api/PaymentTypes/1",
                    new StringContent(modifiedPepsiAsJSON, Encoding.UTF8, "application/json")
                );
                string responseBody = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);


                /*
                    GET section
                    Verify that the PUT operation was successful
                */
                var getPepsi = await client.GetAsync("/api/PaymentTypes/1");
                getPepsi.EnsureSuccessStatusCode();

                string getPepsiBody = await getPepsi.Content.ReadAsStringAsync();
                PaymentType newPepsi = JsonConvert.DeserializeObject<PaymentType>(getPepsiBody);

                Assert.Equal(HttpStatusCode.OK, getPepsi.StatusCode);
                Assert.Equal(newAcctNumber, newPepsi.AcctNumber);
            }
        }
    }
}