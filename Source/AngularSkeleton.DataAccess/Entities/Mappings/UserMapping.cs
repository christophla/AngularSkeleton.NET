//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using AngularSkeleton.Domain.Accounts;

namespace AngularSkeleton.DataAccess.Entities.Mappings
{
    /// <summary>
    ///     Mapping for <see cref="User" /> entity
    /// </summary>
    internal class UserMapping : MappingBase<User>
    {
        public UserMapping()
        {
            Property(user => user.Archived).IsRequired();
            Property(user => user.Email).HasMaxLength(128).IsRequired();
            Property(user => user.IsAdmin).IsRequired();
            Property(user => user.LastLogin).IsOptional();
            Property(user => user.LastPasswordFailureDate).IsOptional();
            Property(user => user.NameFirst).HasMaxLength(50).IsOptional();
            Property(user => user.NameLast).HasMaxLength(50).IsOptional();
            Property(user => user.PasswordChangeDate).IsOptional();
            Property(user => user.PasswordFailuresSinceLastSuccess).IsRequired();
            Property(user => user.PasswordHash).HasMaxLength(128).IsOptional();
            Property(user => user.TimezoneUtcOffset).IsRequired();
            Property(user => user.Username).HasMaxLength(50).IsRequired();
        }

        public override string TableName
        {
            get { return Tables.User; }
        }
    }
}