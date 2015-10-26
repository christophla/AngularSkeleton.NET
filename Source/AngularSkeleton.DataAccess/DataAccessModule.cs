//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using AngularSkeleton.DataAccess.Entities;
using AngularSkeleton.DataAccess.Repositories;
using AngularSkeleton.DataAccess.Repositories.Impl;
using AngularSkeleton.Domain;
using Autofac;

namespace AngularSkeleton.DataAccess
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Modules

            builder.RegisterModule(new DomainModule());

            // Types

            builder.RegisterType<EntityContext>().InstancePerRequest();
            builder.RegisterType<RepositoryFacade>().As<IRepositoryFacade>().InstancePerLifetimeScope();
            builder.RegisterType<ProductsRepository>().As<IProductsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>().InstancePerLifetimeScope();
        }
    }
}