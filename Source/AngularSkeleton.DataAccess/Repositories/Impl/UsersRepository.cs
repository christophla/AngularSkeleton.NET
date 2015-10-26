//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AngularSkeleton.DataAccess.Entities;
using AngularSkeleton.Domain.Accounts;

namespace AngularSkeleton.DataAccess.Repositories.Impl
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(EntityContext entityContext) : base(entityContext)
        {
        }

        public User FindByUsername(string username)
        {
            return DbSet.SingleOrDefault(o => o.Username == username);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await DbSet.SingleOrDefaultAsync(o => o.Username == username);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await DbSet.AnyAsync(o => o.Username == username);
        }
    }
}