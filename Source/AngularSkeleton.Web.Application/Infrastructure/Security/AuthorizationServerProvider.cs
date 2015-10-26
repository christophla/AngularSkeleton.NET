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
using System.Threading.Tasks;
using AngularSkeleton.Domain.Security;
using AngularSkeleton.Service;
using Autofac;
using Autofac.Integration.Owin;
using Microsoft.Owin.Security.OAuth;
using NLog;

namespace AngularSkeleton.Web.Application.Infrastructure.Security
{
    internal class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public const string InvalidGrantError = "invalid_grant";

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var scope = context.OwinContext.GetAutofacLifetimeScope();
                var services = scope.Resolve<IServiceFacade>();

                var user = await services.Security.AuthorizeAsync(context.UserName, context.Password);
                if (null == user)
                    throw new Exception("The user name or password is incorrect.");

                var identity = Principal.CreateIdentity(context.Options.AuthenticationType, user.Username, user.Id, user.IsAdmin);

                context.Validated(identity);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "An error occurred while attempting to grant credentials");
                context.SetError(InvalidGrantError, ex.Message);
            }
        }

#pragma warning disable 1998
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
#pragma warning restore 1998
        {
            context.Validated(); // using resource credentials
        }
    }
}