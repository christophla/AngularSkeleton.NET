//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AngularSkeleton.DataAccess.Util
{
    /// <summary>
    ///     Models a query search result.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    [DataContract]
    public class PagedResult<T>
    {
        internal PagedResult()
        {
        }

        public PagedResult(T item)
        {
            Items = new List<T> {item};
            TotalRecords = Items.Count();
        }

        public PagedResult(ICollection<T> items)
        {
            Items = items ?? new List<T>();
            if (items != null) TotalRecords = items.Count();
        }

        public PagedResult(ICollection<T> items, int totalRecords)
        {
            Items = items;
            TotalRecords = totalRecords;
        }

        /// <summary>
        ///     The data items.
        /// </summary>
        [DataMember]
        public ICollection<T> Items { get; protected set; }

        /// <summary>
        ///     The number of filtered records.
        /// </summary>
        [DataMember]
        public int TotalRecords { get; protected set; }
    }
}