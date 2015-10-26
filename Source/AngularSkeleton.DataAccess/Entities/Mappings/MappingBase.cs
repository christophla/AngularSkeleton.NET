//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AngularSkeleton.Domain;

namespace AngularSkeleton.DataAccess.Entities.Mappings
{
    /// <summary>
    ///     Base class for entity mappings.
    /// </summary>
    /// <remarks>
    ///     Provides mapping support for identity and concurrency.
    /// </remarks>
    /// <typeparam name="T">The entity type</typeparam>
    public abstract class MappingBase<T> : EntityTypeConfiguration<T> where T : EntityBase
    {
        protected MappingBase()
        {
            Property(x => x.RowVersion).IsConcurrencyToken();
            Init();
        }

        /// <summary>
        ///     Defines the schema for the database table.
        /// </summary>
        public virtual string Schema
        {
            get { return Tables.Schema; }
        }

        /// <summary>
        ///     Defines the name for the database table.
        /// </summary>
        public abstract string TableName { get; }

        private void Init()
        {
            ToTable(TableName, Schema);
            MapIdentity();
            MapAuditable();
        }

        /// <summary>
        ///     Maps the auditable fields.
        /// </summary>
        protected virtual void MapAuditable()
        {
            Property(x => x.CreatedAt).IsRequired();
            Property(x => x.CreatedBy).HasMaxLength(120).IsRequired();
            Property(x => x.ModifiedAt).IsOptional();
            Property(x => x.ModifiedBy).HasMaxLength(120).IsOptional();
        }

        /// <summary>
        ///     Maps the identity to <see cref="EntityBase.Id" /> and uses <see cref="DatabaseGeneratedOption.Identity" />
        /// </summary>
        /// <remarks>
        ///     Can be overridden to provide a non-standard identity.
        /// </remarks>
        protected virtual void MapIdentity()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}