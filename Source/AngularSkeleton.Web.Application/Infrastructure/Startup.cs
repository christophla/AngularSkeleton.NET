//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Web.Http;
using AngularSkeleton.NET.WebApplication.Infrastructure.Config;
using AngularSkeleton.Service;
using AngularSkeleton.Web.Application.Infrastructure;
using AngularSkeleton.Web.Application.Infrastructure.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace AngularSkeleton.Web.Application.Infrastructure
{
    /// <summary>
    ///     Application (OWIN) Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Configures the application
        /// </summary>
        /// <param name="app">The app builder</param>
        /// <param name="serviceFacade">An instance of the service facade ofr mocking unit tests</param>
        public void Configuration(IAppBuilder app, IServiceFacade serviceFacade)
        {
            var config = new HttpConfiguration();
            ApiRouteConfig.Register(config);
            FormattersConfig.Register(config);
            MapperConfig.Initialize();
            DocumentationConfig.Register(config);

            // authorize all requests
            config.Filters.Add(new AuthorizeAttribute());

            // ioc container
            ContainerConfig.Register(app, config, serviceFacade);

            // cors
            app.UseCors(CorsOptions.AllowAll);
        }

        /// <summary>
        ///     Configures the application
        /// </summary>
        /// <param name="app">The app builder</param>
        public void Configuration(IAppBuilder app)
        {
            Configuration(app, null);
        }
    }
}