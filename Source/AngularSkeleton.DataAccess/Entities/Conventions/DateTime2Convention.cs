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
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AngularSkeleton.DataAccess.Entities.Conventions
{
    /// <summary>
    ///     Convention for mapping DATETIME2 sql type.
    /// </summary>
    public class DateTime2Convention : Convention
    {
        public const string Datetime2Name = "datetime2";

        public DateTime2Convention()
        {
            Properties<DateTime>().Configure(o => o.HasColumnType(Datetime2Name));
        }
    }
}