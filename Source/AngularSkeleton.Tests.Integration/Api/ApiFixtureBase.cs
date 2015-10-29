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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using AngularSkeleton.Common;
using AngularSkeleton.DataAccess.Migrations;
using AngularSkeleton.Domain.Accounts;
using AngularSkeleton.Service;
using AngularSkeleton.Web.Application.Infrastructure;
using Microsoft.Owin.Testing;
using Moq;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace AngularSkeleton.Tests.Integration.Api
{
    [Trait("Category", TestCategory.Integration)]
    public abstract class ApiFixtureBase : IDisposable
    {
        protected static string CatalogSearchUrl = string.Format("http://localhost/{0}/search", Constants.Api.V1.CatalogRoutePrefix);
        protected static string ProductsUrl = string.Format("http://localhost/{0}/products", Constants.Api.V1.ManageRoutePrefix);
        protected static string UsersUrl = string.Format("http://localhost/{0}/users", Constants.Api.V1.ManageRoutePrefix);

        private string _token;

        protected HttpClient Client;
        protected HttpRequestMessage Request;
        protected HttpResponseMessage Response;
        protected TestServer Server;
        protected Mock<IServiceFacade> ServiceFacadeMock;

        protected ApiFixtureBase()
        {
            ServiceFacadeMock = new Mock<IServiceFacade>();

            Server = TestServer.Create(app =>
            {
                var startup = new Startup();
                startup.Configuration(app, ServiceFacadeMock.Object);
            });

            Request = new HttpRequestMessage();
            Request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Client = Server.HttpClient;
            var token = RetrieveBearerToken(Server);
            Client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
        }

        public void Dispose()
        {
            if (null != Response)
                Response.Dispose();

            if (null != Client)
                Client.Dispose();

            if (null != Server)
                Server.Dispose();
        }

        protected string RetrieveBearerToken(TestServer server)
        {
            if (!string.IsNullOrWhiteSpace(_token))
                return _token;

            // account service mock

            var securityServiceMock = new Mock<ISecurityService>();
            ServiceFacadeMock.Setup(m => m.Security).Returns(securityServiceMock.Object);

            var user = new User(Configuration.DefaultAdminUsername) {IsAdmin = true};
            user.SetPassword(Configuration.DefaultPassword);

            securityServiceMock.Setup(m => m.AuthorizeAsync(Configuration.DefaultAdminUsername, Configuration.DefaultPassword)).ReturnsAsync(user);

            // retrieve token

            var tokenDetails = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", Configuration.DefaultAdminUsername),
                new KeyValuePair<string, string>("password", Configuration.DefaultPassword)
            };

            var tokenPostData = new FormUrlEncodedContent(tokenDetails);
            var tokenResult = server.HttpClient.PostAsync(string.Format("/{0}/accesstoken", Constants.Api.V1.RoutePrefix), tokenPostData).Result;
            tokenResult.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = JObject.Parse(tokenResult.Content.ReadAsStringAsync().Result);

            _token = (string) body["access_token"];
            return _token;
        }
    }
}