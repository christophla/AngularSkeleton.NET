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
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using AngularSkeleton.Common;
using AngularSkeleton.Common.Resources;
using AngularSkeleton.DataAccess.Entities.Conventions;
using AngularSkeleton.DataAccess.Entities.Mappings;
using AngularSkeleton.Domain;
using AngularSkeleton.Domain.Accounts;
using AngularSkeleton.Domain.Catalog;
using InteractivePreGeneratedViews;
using NLog;

namespace AngularSkeleton.DataAccess.Entities
{
    /// <summary>
    ///     Entity database context
    /// </summary>
    public class EntityContext : DbContext
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public EntityContext() : base(Constants.DataAccess.ConnectionStringName)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;

            ObjectContext.SavingChanges += UpdateAuditable;
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<User> Users { get; set; }

        internal ObjectContext ObjectContext
        {
            get
            {
                try
                {
                    return ((IObjectContextAdapter) this).ObjectContext;
                }
                catch (Exception ex)
                {
                    _logger.Fatal(ex.Message);
                    throw new Exception(ExceptionResources.Error_UnableToInitDatabase, ex);
                }
            }
        }

        public static void EnableViewCache()
        {
            if (!DataAccess.Configuration.Database.EnableViewCache) return;

            using (var ctx = new EntityContext())
            {
                InteractiveViews.SetViewCacheFactory(ctx, new SqlServerViewCacheFactory(ctx.Database.Connection.ConnectionString));
            }
        }

        /// <summary>
        ///     Adds mappings to the context.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // conventions
            modelBuilder.Conventions.Add(new DateTime2Convention());

            // entities
            modelBuilder.Configurations.Add(new ProductMapping());
            modelBuilder.Configurations.Add(new UserMapping());
        }

        private static void UpdateAuditable(object sender, EventArgs e)
        {
            var context = sender as ObjectContext;
            if (context == null)
                return;

            // Created 

            foreach (var entry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added).Where(entry => entry.Entity is EntityBase))
            {
                ((EntityBase) entry.Entity).CreatedAt = ((EntityBase) entry.Entity).ModifiedAt = DateTime.UtcNow;
                ((EntityBase) entry.Entity).CreatedBy = ((EntityBase) entry.Entity).ModifiedBy = Thread.CurrentPrincipal.Identity.Name;
            }

            // Updated

            foreach (var entry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified).Where(entry => entry.Entity is EntityBase))
            {
                ((EntityBase) entry.Entity).ModifiedAt = DateTime.UtcNow;
                ((EntityBase) entry.Entity).ModifiedBy = Thread.CurrentPrincipal.Identity.Name;
            }
        }
    }
}