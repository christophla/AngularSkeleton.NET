using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularSkeleton.Common;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Products;
using AngularSkeleton.Service.Model.Users;

namespace AngularSkeleton.Web.Application.Controllers.Catalog
{
    /// <summary>
    ///     Controller for accessing the catalog
    /// </summary>
    [RoutePrefix(Constants.Api.Version.RestV1CatalogRoutePrefix)]
    public class CatalogController : ControllerBase
    {
        private const string RetrieveCatalogEntryRoute = "GetEntryById";

        /// <summary>
        ///     Creates a new catalog controller instance
        /// </summary>
        /// <param name="services">The service facade</param>
        public CatalogController(IServiceFacade services) : base(services)
        {
        }

        /// <summary>
        ///     Search catalog
        /// </summary>
        /// <remarks>Returns a collection of products</remarks>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A client was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("search")]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (ICollection<ProductModel>))]
        public async Task<HttpResponseMessage> SearchAsync(string criteria = null, int skip = 0, int take = 10)
        {
            var options = new QueryOptions {Skip = skip, Take = take};
            var items = await Services.Catalog.SearchAsync(options, criteria);
            return Request.CreateResponse(items);
        }

        /// <summary>
        ///     Retrieve a catalog entry
        /// </summary>
        /// <remarks>Returns a single entry, specified by the productId parameter.</remarks>
        /// <param name="productId">The product id of the entry</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">An entry was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("entries/{productId:long}", Name = RetrieveCatalogEntryRoute)]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (UserModel))]
        public async Task<HttpResponseMessage> GetEntryAsync(long productId)
        {
            var entry = await Services.Catalog.GetEntryByProductIdAsync(productId);
            return Request.CreateResponse(HttpStatusCode.OK, entry);
        }
    }
}