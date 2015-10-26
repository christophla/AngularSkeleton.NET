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
using System.Runtime.Serialization;

namespace AngularSkeleton.Service.Model.Catalog
{
    /// <summary>
    ///     Models a catalog item
    /// </summary>
    public class CatalogEntryModel : ModelBase
    {
        /// <summary>
        ///     The product id
        /// </summary>
        [DataMember]
        public long ProductId { get; set; }

        /// <summary>
        ///     The product friendly name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     The product description
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        ///     The quantity available
        /// </summary>
        [DataMember]
        public int QuantityAvailable { get; set; }

        /// <summary>
        ///     The date the product was added
        /// </summary>
        [DataMember]
        public DateTimeOffset DateAdded { get; set; }
    }
}