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
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularSkeleton.Common;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Products;
using AngularSkeleton.Web.Application.Infrastructure.Attributes;

namespace AngularSkeleton.Web.Application.Controllers.Manage
{
    /// <summary>
    ///     Controller for accessing <see cref="ProductModel" /> instances
    /// </summary>
    [RoutePrefix(Constants.Api.Version.RestV1ManageRoutePrefix)]
    public class ProductsController : ControllerBase
    {
        private const string RetrieveProductRoute = "GetProductById";

        /// <summary>
        ///     Creates a new products controller instance
        /// </summary>
        /// <param name="services"></param>
        public ProductsController(IServiceFacade services) : base(services)
        {
        }

        /// <summary>
        ///     Create a product
        /// </summary>
        /// <remarks>Creates a new product</remarks>
        /// <param name="model">The product data</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products")]
        [AcceptVerbs("POST")]
        [CheckModelForNull]
        [ValidateModel]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> CreateAsync(ProductAddModel model)
        {
            var product = await Services.Management.CreateProductAsync(model);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            var uri = Url.Link(RetrieveProductRoute, new {id = product.Id});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        ///     Delete a product
        /// </summary>
        /// <remarks>Deletes a single product, specified by the id parameter.</remarks>
        /// <param name="id">The ID of the desired product</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A product was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products/{id:long}")]
        [AcceptVerbs("DELETE")]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> DeleteAsync(long id)
        {
            await Services.Management.DeleteProductAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        ///     Get users
        /// </summary>
        /// <remarks>Returns a collection of all users.</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products")]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (IList<ProductModel>))]
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            var products = await Services.Management.GetAllProductsAsync();
            var models = products as IList<ProductModel> ?? products.ToList();
            var response = Request.CreateResponse(models);
            response.Headers.Add(Constants.ResponseHeaders.TotalCount, models.Count().ToString());
            return response;
        }

        /// <summary>
        ///     Get a single product
        /// </summary>
        /// <remarks>Returns a single client, specified by the id parameter.</remarks>
        /// <param name="id">The id of the desired client</param>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A client was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products/{id:long}", Name = RetrieveProductRoute)]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (ProductModel))]
        public async Task<HttpResponseMessage> GetSingleAsync(long id)
        {
            var product = await Services.Management.GetProductAsync(id);
            return Request.CreateResponse(product);
        }

        /// <summary>
        ///     Toggle product
        /// </summary>
        /// <remarks>
        ///     This will archive an active product or activate an archived product.
        /// </remarks>
        /// <param name="id">The id of the desired product</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A user was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products/{id:long}/toggle")]
        [AcceptVerbs("POST")]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> Toggle(long id)
        {
            await Services.Management.ToggleProductAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        ///     Update product
        /// </summary>
        /// <remarks>Updates a single user, specified by the id parameter.</remarks>
        /// <param name="model">The user data</param>
        /// <param name="id">The id</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A user was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products/{id:long}")]
        [AcceptVerbs("PUT")]
        [CheckModelForNull]
        [ValidateModel]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> UpdateAsync(ProductUpdateModel model, long id)
        {
            await Services.Management.UpdateProductAsync(model, id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}