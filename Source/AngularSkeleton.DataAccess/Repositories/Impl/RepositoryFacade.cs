//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

//TODO: Implement Lazy<T> support for autofac (cpt)

using System.Threading.Tasks;
using AngularSkeleton.DataAccess.Entities;
using CuttingEdge.Conditions;

namespace AngularSkeleton.DataAccess.Repositories.Impl
{
    public class RepositoryFacade : IRepositoryFacade
    {
        private readonly EntityContext _context;
        private IProductsRepository _productsRepository;
        private IUsersRepository _usersRepository;

        public RepositoryFacade(EntityContext context)
        {
            Condition.Requires(context, "context").IsNotNull();
            _context = context;
        }

        public IProductsRepository Products
        {
            get { return _productsRepository ?? (_productsRepository = new ProductsRepository(_context)); }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IUsersRepository Users
        {
            get { return _usersRepository ?? (_usersRepository = new UsersRepository(_context)); }
        }
    }
}