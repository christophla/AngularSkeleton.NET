//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Threading.Tasks;
using AngularSkeleton.DataAccess.Entities;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Domain.Catalog;

namespace AngularSkeleton.DataAccess.Repositories.Impl
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        public ProductsRepository(EntityContext entityContext) : base(entityContext)
        {
        }

        public async Task<PagedResult<Product>> Search(QueryOptions options, string criteria = null)
        {
            return (null == criteria) ? await GetAllAsync(options) : await GetAllAsync(options, o => o.Name.Contains(criteria));
        }
    }
}