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

namespace AngularSkeleton.Service.Model.Users
{
    /// <summary>
    ///     Models a user
    /// </summary>
    public class UserModel : ModelBase
    {
        /// <summary>
        ///     The user id
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        ///     Indicates if the user is archived
        /// </summary>
        [DataMember]
        public bool Archived { get; set; }

        /// <summary>
        ///     The avatar image url
        /// </summary>
        [DataMember]
        public string Avatar { get; set; }

        /// <summary>
        ///     The user email
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        ///     Indicates if the user is an admin
        /// </summary>
        [DataMember]
        public bool IsAdmin { get; set; }

        /// <summary>
        ///     The user last successfful login date
        /// </summary>
        [DataMember]
        public DateTime? LastLogin { get; internal set; }

        /// <summary>
        ///     The user first name
        /// </summary>
        [DataMember]
        public string NameFirst { get; set; }

        /// <summary>
        ///     The user last name
        /// </summary>
        [DataMember]
        public string NameLast { get; set; }

        /// <summary>
        ///     The user theme
        /// </summary>
        [DataMember]
        public string Theme { get; set; }

        /// <summary>
        ///     The user locale offset
        /// </summary>
        [DataMember]
        public short TimezoneUtcOffset { get; set; }

        /// <summary>
        ///     The user username
        /// </summary>
        [DataMember]
        public string Username { get; internal set; }
    }
}