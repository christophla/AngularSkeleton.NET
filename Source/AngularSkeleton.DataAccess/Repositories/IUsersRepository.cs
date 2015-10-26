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
using AngularSkeleton.Domain.Accounts;

namespace AngularSkeleton.DataAccess.Repositories
{
    /// <summary>
    ///     Repository for accessing <see cref="User" /> entities
    /// </summary>
    public interface IUsersRepository : IGenericRepository<User>
    {
        /// <summary>
        ///     Finds a user by username
        /// </summary>
        /// <param name="username">The username</param>
        User FindByUsername(string username);

        /// <summary>
        ///     Finds a user by username async
        /// </summary>
        /// <param name="username">The username</param>
        Task<User> FindByUsernameAsync(string username);

        /// <summary>
        ///     Indicates if username is in-use
        /// </summary>
        /// <param name="username">The username</param>
        Task<bool> UsernameExistsAsync(string username);
    }
}