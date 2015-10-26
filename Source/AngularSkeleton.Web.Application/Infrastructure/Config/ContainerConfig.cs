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
using System.Web.Http;
using AngularSkeleton.Common;
using AngularSkeleton.Service;
using AngularSkeleton.Web.Application.Controllers;
using AngularSkeleton.Web.Application.Infrastructure.Security;
using Autofac;
using Autofac.Integration.WebApi;
using CuttingEdge.Conditions;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace AngularSkeleton.Web.Application.Infrastructure.Config
{
    /// <summary>
    ///     Configures the IOC container (Autofac)
    /// </summary>
    public class ContainerConfig
    {
        /// <summary>
        ///     Registers the configuration
        /// </summary>
        public static IContainer Register(IAppBuilder app, HttpConfiguration configuration, IServiceFacade serviceFacade = null)
        {
            Condition.Requires(configuration, "configuration").IsNotNull();

            var builder = new ContainerBuilder();

            builder.RegisterModule(new ServiceModule(serviceFacade));

            // controllers
            builder.RegisterApiControllers(typeof (ControllerBase).Assembly);

            // request
            builder.RegisterHttpRequestMessage(configuration);

            // authorization
            builder.RegisterType<AuthorizationServerProvider>().As<IOAuthAuthorizationServerProvider>().InstancePerLifetimeScope();

            // set resolver
            var container = builder.Build();

            // set the dependency resolver for Web API
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(configuration);

            ConfigureOAuth(app, container);

            app.UseWebApi(configuration);

            return container;
        }

        /// <summary>
        ///     Registers the autofac configuration.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void RegisterAutofacConfig(IAppBuilder app, HttpConfiguration configuration)
        {
            Register(app, configuration);
        }

        private static void ConfigureOAuth(IAppBuilder app, IContainer container)
        {
            var serverOptions = new OAuthAuthorizationServerOptions
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                Provider = container.Resolve<IOAuthAuthorizationServerProvider>(),
                TokenEndpointPath = new PathString(string.Format("/{0}/accesstoken", Constants.Api.Version.RestV1RoutePrefix)) // absolute for oauth
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(serverOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}