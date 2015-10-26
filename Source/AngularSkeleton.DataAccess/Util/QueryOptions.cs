//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using AngularSkeleton.Common;

namespace AngularSkeleton.DataAccess.Util
{
    /// <summary>
    ///     Query options for data paging support
    /// </summary>
    public class QueryOptions
    {
        public QueryOptions()
        {
            Skip = 0;
            Take = Constants.DataAccess.DefaultRecordsPerQuery;
        }

        /// <summary>
        ///     Returns all items.
        /// </summary>
        public static QueryOptions AllItems
        {
            get { return new QueryOptions {RetrieveAll = true}; }
        }

        public int Skip { get; set; }

        public int Take { get; set; }

        internal bool RetrieveAll { get; private set; }
    }
}