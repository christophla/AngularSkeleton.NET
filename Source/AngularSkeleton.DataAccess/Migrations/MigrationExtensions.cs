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
using AngularSkeleton.DataAccess.Entities;
using AngularSkeleton.Domain.Accounts;
using AngularSkeleton.Domain.Catalog;
using CuttingEdge.Conditions;

namespace AngularSkeleton.DataAccess.Migrations
{
    /// <summary>
    ///     Extensions for support database migrations
    /// </summary>
    internal static class MigrationExtensions
    {
        /// <summary>
        ///     Idempotent user creation
        /// </summary>
        /// <param name="context">The entity context</param>
        /// <param name="username">The user name</param>
        /// <param name="email">The user email</param>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="isAdministrator">Indicates if the user is an admin</param>
        /// <param name="password">The password</param>
        public static User TryAddUser(this EntityContext context, string username, string email, string firstName, string lastName, bool isAdministrator, string password)
        {
            Condition.Requires(context, "context").IsNotNull();

            var user = context.Users.SingleOrDefault(o => o.Username == username);
            if (null == user)
            {
                user = new User(username, email, firstName, lastName, isAdministrator);
                user.SetPassword(password);
                context.Users.Add(user);
            }
            return user;
        }

        /// <summary>
        ///     Idempotent product creation
        /// </summary>
        /// <param name="context">The entity context</param>
        /// <param name="name">The product name</param>
        /// <param name="description">The product description</param>
        /// <param name="quantityAvailable">The product quantity available</param>
        public static Product TryAddProduct(this EntityContext context, string name, string description, int quantityAvailable)
        {
            Condition.Requires(context, "context").IsNotNull();

            var product = context.Products.FirstOrDefault(o => o.Name == name);
            if (null == product)
            {
                product = new Product(name) {Description = description, QuantityAvailable = quantityAvailable};
                context.Products.Add(product);
            }
            return product;
        }
    }
}