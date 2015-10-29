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
using AngularSkeleton.Service.Model.Users;
using AngularSkeleton.Web.Application.Infrastructure.Attributes;

namespace AngularSkeleton.Web.Application.Controllers.Manage
{
    /// <summary>
    ///     Controller for accessing users
    /// </summary>
    [RoutePrefix(Constants.Api.V1.ManageRoutePrefix)]
    public class UsersController : ControllerBase
    {
        private const string RetrieveUserRoute = "GetUserById";

        /// <summary>
        ///     Creates a new users controller
        /// </summary>
        /// <param name="services"></param>
        public UsersController(IServiceFacade services) : base(services)
        {
        }

        /// <summary>
        ///     Create user
        /// </summary>
        /// <remarks> Creates a new user.</remarks>
        /// <param name="model">The user data</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("users")]
        [AcceptVerbs("POST")]
        [CheckModelForNull]
        [ValidateModel]
        [ResponseType(typeof (UserModel))]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> CreateAsync(UserAddModel model)
        {
            var user = await Services.Management.CreateUserAsync(model);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            var uri = Url.Link(RetrieveUserRoute, new {id = user.Id});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        ///     Delete user
        /// </summary>
        /// <remarks>Deletes a single user, specified by the id parameter.</remarks>
        /// <param name="id">The id</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A user was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("users/{id:long}")]
        [AcceptVerbs("DELETE")]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> DeleteAsync(long id)
        {
            await Services.Management.DeleteUserAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        ///     Retrieve all users
        /// </summary>
        /// <remarks>Returns a collection of all users.</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("users")]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (IEnumerable<UserModel>))]
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            var users = await Services.Management.GetAllUsersAsync();
            var models = users as IList<UserModel> ?? users.ToList();
            var response = Request.CreateResponse(models);
            response.Headers.Add(Constants.ResponseHeaders.TotalCount, models.Count().ToString());
            return response;
        }

        /// <summary>
        ///     Retrieve a single user
        /// </summary>
        /// <remarks>Returns a single user, specified by the id parameter.</remarks>
        /// <param name="id">The id of the desired user</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A user was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("users/{id:long}", Name = RetrieveUserRoute)]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (UserModel))]
        public async Task<HttpResponseMessage> GetSingleAsync(long id)
        {
            var user = await Services.Management.GetUserAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        /// <summary>
        ///     Me
        /// </summary>
        /// <remarks>Returns the currently logged in user.</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("users/me")]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (UserModel))]
        public async Task<HttpResponseMessage> MeAsync()
        {
            var user = await Services.Management.GetCurrentUserAsync();
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        /// <summary>
        ///     Reset password
        /// </summary>
        /// <remarks>Sends an email with instructions to reset the password to the user.</remarks>
        /// <param name="id">The id</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A user was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("users/{id:long}/reset-password")]
        [AcceptVerbs("DELETE")]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> ResetPasswordAsync(long id)
        {
            await Services.Security.ResetPasswordAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        ///     Toggle user
        /// </summary>
        /// <remarks>
        ///     This will archive an active user or activate an archived user.
        /// </remarks>
        /// <param name="id">The id of the desired user</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A user was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("users/{id:long}/toggle")]
        [AcceptVerbs("POST")]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> ToggleAsync(long id)
        {
            await Services.Management.ToggleUserAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        ///     Update user
        /// </summary>
        /// <remarks>Updates a single user, specified by the id parameter.</remarks>
        /// <param name="model">The user data</param>
        /// <param name="id">The id</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A user was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("users/{id:long}")]
        [AcceptVerbs("PUT")]
        [CheckModelForNull]
        [ValidateModel]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> UpdateAsync(UserUpdateModel model, long id)
        {
            await Services.Management.UpdateUserAsync(model, id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}