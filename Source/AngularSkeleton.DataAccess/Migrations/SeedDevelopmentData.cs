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
using Faker;

namespace AngularSkeleton.DataAccess.Migrations
{
    /// <summary>
    ///     Development data seed helper.
    /// </summary>
    internal static class SeedDevelopmentData
    {
        public static void Seed(EntityContext context)
        {
            // Create users

            context.TryAddUser(Configuration.DefaultUserUsername, Configuration.DefaultEmail, "User", "Account", false, Configuration.DefaultPassword);
            context.TryAddUser(Configuration.DefaultAdminUsername, Configuration.DefaultEmail, "Admin", "Account", true, Configuration.DefaultPassword);

            // Create 500 products

            for (var i = 1; i < 500; i++)
            {
                context.TryAddProduct(Company.Name(), Lorem.Sentence(), RandomNumber.Next(0, 1000));
            }
        }
    }
}