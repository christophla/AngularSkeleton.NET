// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Threading.Tasks;
using AngularSkeleton.Common.Exceptions;
using AngularSkeleton.DataAccess.Repositories;
using AngularSkeleton.Domain.Accounts;
using CuttingEdge.Conditions;

namespace AngularSkeleton.Service.Impl
{
    public class SecurityService : ISecurityService
    {
        private readonly IRepositoryFacade _repositories;

        public SecurityService(IRepositoryFacade repositories)
        {
            Condition.Requires(repositories, "repositories").IsNotNull();
            _repositories = repositories;
        }

        public async Task<User> AuthorizeAsync(string username, string password)
        {
            var user = await _repositories.Users.FindByUsernameAsync(username);

            if (null == user)
                return null;

            var valid = user.VerifyPassword(password);
            await _repositories.SaveChangesAsync(); // update audit data

            return valid ? user : null;
        }

        public Task<bool> ResetPasswordAsync(long userId)
        {
            throw new BusinessException("Password reset is not currently supported");
        }
    }
}