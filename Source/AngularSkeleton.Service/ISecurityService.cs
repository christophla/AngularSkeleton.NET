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

namespace AngularSkeleton.Service
{
    /// <summary>
    ///     Provides authentication services
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        ///     Authorizes user credentials
        /// </summary>
        /// <remarks>
        ///     Returns null if invalid
        /// </remarks>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The authenticated user</returns>
        Task<User> AuthorizeAsync(string username, string password);

        /// <summary>
        ///     Resets a user's password and sends an email with change token.
        /// </summary>
        /// <param name="userId">The userId</param>
        Task<bool> ResetPasswordAsync(long userId);
    }
}