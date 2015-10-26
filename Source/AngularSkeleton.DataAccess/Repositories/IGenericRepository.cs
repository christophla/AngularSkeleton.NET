//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Domain;

namespace AngularSkeleton.DataAccess.Repositories
{
    /// <summary>
    ///     Generic repository for accessing <see cref="EntityBase" /> derived entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        ///     Returns true if any entities exist that meet the condition.
        /// </summary>
        /// <param name="predicate">The condition</param>
        bool Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Returns true if any entities exist that meet the condition.
        /// </summary>
        /// <param name="predicate">The condition</param>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Finds an entity by its id.
        /// </summary>
        /// <param name="id">The id</param>
        TEntity Find(long id);

        /// <summary>
        ///     Finds an entity by its id.
        /// </summary>
        /// <param name="id">The id</param>
        Task<TEntity> FindAsync(long id);

        /// <summary>
        ///     Returns all entities with options.
        /// </summary>
        /// <param name="options">The query options</param>
        /// <param name="filter">Optional filter predicate</param>
        PagedResult<TEntity> GetAll(QueryOptions options, Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        ///     Returns all entities with options.
        /// </summary>
        /// <param name="options">The query options</param>
        /// <param name="filter">Optional filter predicate</param>
        Task<PagedResult<TEntity>> GetAllAsync(QueryOptions options, Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        ///     Inserts an entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        void Insert(TEntity entity);

        /// <summary>
        ///     Removes an entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        void Remove(TEntity entity);

        /// <summary>
        ///     Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        void Update(TEntity entity);
    }
}