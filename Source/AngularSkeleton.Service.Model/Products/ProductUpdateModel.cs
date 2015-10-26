//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Runtime.Serialization;

namespace AngularSkeleton.Service.Model.Products
{
    /// <summary>
    ///     Model for updating a product
    /// </summary>
    public class ProductUpdateModel : ModelBase
    {
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
    }
}