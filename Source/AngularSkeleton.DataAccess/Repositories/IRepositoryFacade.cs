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

namespace AngularSkeleton.DataAccess.Repositories
{
    /// <summary>
    ///     Facade for accessing repositories
    /// </summary>
    public interface IRepositoryFacade
    {
        /// <summary>
        ///     The products entity repository
        /// </summary>
        IProductsRepository Products { get; }

        /// <summary>
        ///     The users entity repository
        /// </summary>
        IUsersRepository Users { get; }

        /// <summary>
        ///     Saves context changes async
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        ///     Saves context changes
        /// </summary>
        int SaveChanges();
    }
}