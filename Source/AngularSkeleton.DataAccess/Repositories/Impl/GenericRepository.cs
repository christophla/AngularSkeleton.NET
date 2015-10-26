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
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AngularSkeleton.DataAccess.Entities;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Domain;
using CuttingEdge.Conditions;

namespace AngularSkeleton.DataAccess.Repositories.Impl
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly EntityContext EntityContext;

        public GenericRepository(EntityContext entityContext)
        {
            Condition.Requires(entityContext, "entityContext").IsNotNull();

            EntityContext = entityContext;
            DbSet = entityContext.Set<TEntity>();
        }

        protected IDbSet<TEntity> DbSet { get; set; }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }

        public TEntity Find(long id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(long id)
        {
            return await EntityContext.Set<TEntity>().FindAsync(id);
        }

        public virtual PagedResult<TEntity> GetAll(QueryOptions options, Expression<Func<TEntity, bool>> filter = null)
        {
            options = options ?? new QueryOptions();
            var query = DbSet.AsQueryable();

            if (null != filter)
                query = query.Where(filter);

            return options.RetrieveAll ?
                new PagedResult<TEntity>(query.ToList()) :
                new PagedResult<TEntity>(query.OrderBy(o => o.Id).Skip(options.Skip).Take(options.Take).ToList(), query.Count());
        }

        public virtual async Task<PagedResult<TEntity>> GetAllAsync(QueryOptions options, Expression<Func<TEntity, bool>> filter = null)
        {
            options = options ?? new QueryOptions();
            var query = DbSet.AsQueryable();

            if (null != filter)
                query = query.Where(filter);

            var count = await query.CountAsync();
            return options.RetrieveAll ?
                new PagedResult<TEntity>(await query.ToListAsync()) :
                new PagedResult<TEntity>(await query.OrderBy(o => o.Id).Skip(options.Skip).Take(options.Take).ToListAsync(), await query.CountAsync());
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            EntityContext.Entry(entity).State = EntityState.Modified;
        }
    }
}