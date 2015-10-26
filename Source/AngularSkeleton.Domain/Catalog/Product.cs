//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using CuttingEdge.Conditions;

namespace AngularSkeleton.Domain.Catalog
{
    public class Product : EntityBase
    {
        /// <summary>
        ///     Initializes a new product.
        /// </summary>
        /// <param name="name">The product name</param>
        public Product(string name) : this()
        {
            Condition.Requires(name, "name").IsNotNullOrWhiteSpace().IsNotLongerThan(100);
            Name = name;
        }

        protected Product()
        {
        }

        /// <summary>
        ///     Indicates if the product is active
        /// </summary>
        public bool Archived { get; set; }

        /// <summary>
        ///     The product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The product quantity available
        /// </summary>
        public int QuantityAvailable { get; set; }
    }
}