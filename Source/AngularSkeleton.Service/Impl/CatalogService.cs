//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using AngularSkeleton.Common;
using AngularSkeleton.Common.Exceptions;
using AngularSkeleton.DataAccess.Repositories;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Service.Model.Catalog;
using CuttingEdge.Conditions;

namespace AngularSkeleton.Service.Impl
{
    public class CatalogService : ICatalogService
    {
        private readonly IRepositoryFacade _repositories;

        public CatalogService(IRepositoryFacade repositories)
        {
            Condition.Requires(repositories, "repositories").IsNotNull();
            _repositories = repositories;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.User)]
        public async Task<CatalogEntryModel> GetEntryByProductIdAsync(long productId)
        {
            var product = await _repositories.Products.FindAsync(productId);
            if (null == product)
                throw new NotFoundException("The product with id {0} was not found", productId);

            return new CatalogEntryModel
            {
                ProductId = product.Id,
                DateAdded = product.CreatedAt,
                Description = product.Description,
                Name = product.Name,
                QuantityAvailable = product.QuantityAvailable
            };
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.User)]
        public async Task<PagedResult<CatalogEntryModel>> SearchAsync(QueryOptions options, string criteria = null)
        {
            var products = await _repositories.Products.Search(options, criteria);
            var models = products.Items.Select(product => new CatalogEntryModel
            {
                ProductId = product.Id,
                DateAdded = product.CreatedAt,
                Description = product.Description,
                Name = product.Name,
                QuantityAvailable = product.QuantityAvailable
            });
            return new PagedResult<CatalogEntryModel>(models.ToList(), products.TotalRecords);
        }
    }
}