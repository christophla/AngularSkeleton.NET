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
using System.Threading;

namespace AngularSkeleton.Domain
{
    /// <summary>
    ///     Base class for all entities.
    /// </summary>
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            CreatedBy = ModifiedBy = Thread.CurrentPrincipal.Identity.Name;
            CreatedAt = ModifiedAt = DateTime.UtcNow;
        }

        /// <summary>
        ///     The date that the record was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; internal set; }

        /// <summary>
        ///     The user that originally created the record.
        /// </summary>
        public string CreatedBy { get; internal set; }

        /// <summary>
        ///     The primary key.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     The date that the record was last modified.
        /// </summary>
        public DateTimeOffset ModifiedAt { get; internal set; }

        /// <summary>
        ///     The user that last modified the record.
        /// </summary>
        public string ModifiedBy { get; internal set; }

        /// <summary>
        ///     The record version.
        /// </summary>
        public int RowVersion { get; set; }
    }
}