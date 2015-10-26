//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Products;
using Moq;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xbehave;

namespace AngularSkeleton.Tests.Integration.Api
{
    /// <summary>
    ///     Fixture for testing products endpoint
    /// </summary>
    public class ProductsFixture : ApiFixtureBase
    {
        protected Mock<IManagementService> ManagementServiceMock;

        public ProductsFixture()
        {
            ManagementServiceMock = new Mock<IManagementService>();
            ServiceFacadeMock.Setup(m => m.Management).Returns(ManagementServiceMock.Object);
        }

        /// <summary>
        ///     PUT /management/products
        /// </summary>
        [Scenario]
        public void CreatingAProduct(dynamic newProduct)
        {
            "Given a new product".
                f(() =>
                {
                    newProduct = JObject.FromObject(new
                    {
                        name = "TEST_PRODUCT",
                        description = "TEST_DESCRIPTION",
                        quantityAvailable = 250
                    });
                });

            "When a POST request is made".
                f(async () =>
                {
                    ManagementServiceMock.Setup(m => m.CreateProductAsync(It.IsAny<ProductAddModel>())).ReturnsAsync(new ProductModel {Id = 1});
                    Request.Method = HttpMethod.Post;
                    Request.RequestUri = new Uri(ProductsUrl);
                    Request.Content = new ObjectContent<dynamic>(newProduct, new JsonMediaTypeFormatter());
                    Response = await Client.SendAsync(Request);
                });

            "Then a '201 Created' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.Created));

            "Then the product should be saved".
                f(() => ManagementServiceMock.Verify(m => m.CreateProductAsync(It.IsAny<ProductAddModel>()), Times.Once()));

            "And the reponse header location should be set".
                f(() => Response.Headers.Location.ShouldNotBe(null));
        }

        /// <summary>
        ///     DELETE /management/products/{id}
        /// </summary>
        [Scenario]
        [Example(1)]
        public void DeletingAProduct(long productId)
        {
            "Given an existing product".
                f(() => ManagementServiceMock.Setup(m => m.DeleteProductAsync(productId)).ReturnsAsync(1));

            "When a DELETE request is made".
                f(() =>
                {
                    Request.Method = HttpMethod.Delete;
                    Request.RequestUri = new Uri(string.Format("{0}/{1}", ProductsUrl, productId));
                    Request.Content = new ObjectContent<dynamic>(new JObject(), new JsonMediaTypeFormatter());
                    Response = Client.SendAsync(Request).Result;
                });

            "Then a 200 OK status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "Then the contact should be removed".
                f(() => ManagementServiceMock.Verify(m => m.DeleteProductAsync(productId), Times.Once()));
        }

        /// <summary>
        ///     GET /management/products
        /// </summary>
        [Scenario]
        public void RetrievingAllProducts(IList<ProductModel> products, IList<ProductModel> productsResult)
        {
            "Given existing products".
                f(() =>
                {
                    products = new List<ProductModel>
                    {
                        new ProductModel {Id = 1},
                        new ProductModel {Id = 2}
                    };
                });

            "When they are retrieved".
                f(async () =>
                {
                    ManagementServiceMock.Setup(m => m.GetAllProductsAsync()).ReturnsAsync(products);
                    Request.RequestUri = new Uri(string.Format("{0}", ProductsUrl));
                    Response = await Client.SendAsync(Request);
                    productsResult = await Response.Content.ReadAsAsync<IList<ProductModel>>();
                });

            "Then a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "And the products are returned".
                f(() => productsResult.Count().ShouldBe(products.Count()));
        }

        /// <summary>
        ///     GET /management/products/{id}
        /// </summary>
        [Scenario]
        public void RetrievingAProduct(ProductModel product, long productId)
        {
            "Given an existing product".
                f(() => ManagementServiceMock.Setup(m => m.GetProductAsync(productId)).ReturnsAsync(new ProductModel {Id = productId}));

            "When it is retrieved".
                f(() =>
                {
                    Request.RequestUri = new Uri(string.Format("{0}/{1}", ProductsUrl, productId));
                    Response = Client.SendAsync(Request).Result;
                    product = Response.Content.ReadAsAsync<ProductModel>().Result;
                });

            "Then the product should be retrieved".
                f(() => ManagementServiceMock.Verify(m => m.GetProductAsync(productId), Times.Once()));

            "And a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "Then it is returned".
                f(() => product.ShouldNotBe(null));

            "And it should have an id".
                f(() => product.Id.ShouldBe(productId));
        }

        /// <summary>
        ///     GET /management/products/{productId}/toggle
        /// </summary>
        [Scenario]
        [Example(1)]
        public void TogglingAProduct(long productId)
        {
            "Given an existing task".
                f(() => ManagementServiceMock.Setup(m => m.ToggleProductAsync(productId)).ReturnsAsync(1));

            "When a POST request is made".
                f(() =>
                {
                    Request.Method = HttpMethod.Post;
                    Request.RequestUri = new Uri(string.Format("{0}/{1}/toggle", ProductsUrl, productId));
                    Request.Content = new ObjectContent<dynamic>(new JObject(), new JsonMediaTypeFormatter());
                    Response = Client.SendAsync(Request).Result;
                });

            "Then the task should be deactivated".
                f(() => ManagementServiceMock.Verify(m => m.ToggleProductAsync(productId), Times.Once()));

            "And a 200 OK status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));
        }

        /// <summary>
        ///     PUT /management/products/{id}
        /// </summary>
        [Scenario]
        public void UpdatingAProduct(ProductModel user, long productId)
        {
            "Given an existing user".
                f(() => ManagementServiceMock.Setup(m => m.UpdateProductAsync(It.IsAny<ProductUpdateModel>(), productId)).ReturnsAsync(1));

            "When a PUT request is made".
                f(() =>
                {
                    dynamic dto = JObject.FromObject(new
                    {
                        description = "TEST_DESCRIPTION",
                        name = "TEST_NAME",
                        quantityAvailable = 99,
                    });

                    Request.Method = HttpMethod.Put;
                    Request.RequestUri = new Uri(string.Format("{0}/{1}", ProductsUrl, productId));
                    Request.Content = new ObjectContent<dynamic>(dto, new JsonMediaTypeFormatter());
                    Response = Client.SendAsync(Request).Result;
                });

            "Then a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "And the user should be updated".
                f(() => ManagementServiceMock.Verify(m => m.UpdateProductAsync(It.IsAny<ProductUpdateModel>(), productId), Times.Once()));
        }
    }
}