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
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Service.Model.Catalog;

namespace AngularSkeleton.Service
{
    /// <summary>
    ///     Service for accessing the product catalog
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        ///     Retrieves a single entry
        /// </summary>
        /// <param name="productId">The product id</param>
        Task<CatalogEntryModel> GetEntryByProductIdAsync(long productId);

        /// <summary>
        ///     Searches the catalog
        /// </summary>
        /// <param name="options">The query paging options</param>
        /// <param name="criteria">The optional search criteria</param>
        /// <returns>A paged result of products</returns>
        Task<PagedResult<CatalogEntryModel>> SearchAsync(QueryOptions options, string criteria = null);
    }
}