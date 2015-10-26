//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using AngularSkeleton.DataAccess;
using AngularSkeleton.Service.Config;
using AngularSkeleton.Service.Impl;
using Autofac;

namespace AngularSkeleton.Service
{
    /// <summary>
    ///     Module for the services library
    /// </summary>
    public class ServiceModule : Module
    {
        private readonly IServiceFacade _serviceFacade;

        public ServiceModule(IServiceFacade serviceFacade = null)
        {
            _serviceFacade = serviceFacade;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Modules
            builder.RegisterModule(new DataAccessModule());

            // Types
            builder.RegisterType<CatalogService>().As<ICatalogService>().InstancePerRequest();
            builder.RegisterType<ManagementService>().As<IManagementService>().InstancePerRequest();
            builder.RegisterType<SecurityService>().As<ISecurityService>().InstancePerRequest();

            // Service Facade
            if (null == _serviceFacade)
                builder.RegisterType<ServiceFacade>().As<IServiceFacade>().InstancePerRequest().PropertiesAutowired();
            else
                builder.RegisterInstance(_serviceFacade).As<IServiceFacade>();

            MapperConfig.Initialize();
        }
    }
}