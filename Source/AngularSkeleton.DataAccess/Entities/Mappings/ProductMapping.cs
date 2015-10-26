//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using AngularSkeleton.Domain.Catalog;

namespace AngularSkeleton.DataAccess.Entities.Mappings
{
    /// <summary>
    ///     Mapping for a <see cref="Product" /> entity.
    /// </summary>
    internal class ProductMapping : MappingBase<Product>
    {
        public ProductMapping()
        {
            Property(client => client.Archived).IsRequired();
            Property(client => client.Description).IsOptional();
            Property(client => client.Name).HasMaxLength(100).IsRequired();
            Property(client => client.QuantityAvailable).IsRequired();
        }

        public override string TableName
        {
            get { return Tables.Product; }
        }
    }
}