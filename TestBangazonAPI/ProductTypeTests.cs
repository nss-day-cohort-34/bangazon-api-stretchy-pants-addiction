using BangazonAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestBangazonAPI
{
    public class ProductTypeTests
    {

        [Fact]
        public async Task Test_Get_All_ProductTypes()
        {

            using (var client = new APIClientProvider().Client)
            {
                /*
                    ARRANGE
                */


                /*
                    ACT
                */
                var response = await client.GetAsync("/api/ProductType");


                string responseBody = await response.Content.ReadAsStringAsync();
                var ProductTypeList = JsonConvert.DeserializeObject<List<ProductTypes>>(responseBody);

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(ProductTypeList.Count > 0);
            }
        }

        [Fact]
        public async Task Test_Get_One_ProductType()
        {

            using (var client = new APIClientProvider().Client)
            {
                /*
                    ARRANGE
                */


                /*
                    ACT
                */
                var response = await client.GetAsync("/api/producttype/1");


                string responseBody = await response.Content.ReadAsStringAsync();
                var ProductType = JsonConvert.DeserializeObject<ProductTypes>(responseBody);

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(ProductType);
            }
        }
        [Fact]
        public async Task Test_Post_ProductType()
        {
            /*
                Generate a new instance of an HttpClient that you can
                use to generate HTTP requests to your API controllers.
                The `using` keyword will automatically dispose of this
                instance of HttpClient once your code is done executing.
            */
            using (var client = new APIClientProvider().Client)
            {
                /*
                    ARRANGE
                */

                // Construct a new student object to be sent to the API
                ProductTypes producttype = new ProductTypes
                {
                    Name = "Bathroom"
                };

                // Serialize the C# object into a JSON string
                var producttypeJSON = JsonConvert.SerializeObject(producttype);


                /*
                    ACT
                */

                // Use the client to send the request and store the response
                var response = await client.PostAsync(
                    "/api/producttype",
                    new StringContent(producttypeJSON, Encoding.UTF8, "application/json")
                );

                // Store the JSON body of the response
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON into an instance of Animal
                var newproducttype = JsonConvert.DeserializeObject<ProductTypes>(responseBody);


                /*
                    ASSERT
                */

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.Equal("Bathroom", newproducttype.Name);

            }
        }


       [Fact]
        public async Task Test_Modify_ProductType()
        {
           
            string newName = "HouseCare";

            using (var client = new APIClientProvider().Client)
            {
               
                ProductTypes modifiedProductType = new ProductTypes
                {
                    Name = "HouseCare",

                };
                var modifiedProductTypeAsJSON = JsonConvert.SerializeObject(modifiedProductType);

                var response = await client.PutAsync(
                    "/api/producttype/1",
                    new StringContent(modifiedProductTypeAsJSON, Encoding.UTF8, "application/json")
                );
                string responseBody = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);


            
                var getProductType = await client.GetAsync("/api/producttype/1");
                getProductType.EnsureSuccessStatusCode();

                string getProductTypeBody = await getProductType.Content.ReadAsStringAsync();
                ProductTypes newProductType = JsonConvert.DeserializeObject<ProductTypes>(getProductTypeBody);

                Assert.Equal(HttpStatusCode.OK, getProductType.StatusCode);
                Assert.Equal(newName, newProductType.Name);
            }
        }
    }
}

