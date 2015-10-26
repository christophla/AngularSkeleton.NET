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
using AngularSkeleton.Common;
using AngularSkeleton.Domain.Util;
using CuttingEdge.Conditions;

namespace AngularSkeleton.Domain.Accounts
{
    /// <summary>
    ///     User entity.
    /// </summary>
    public class User : EntityBase
    {
        /// <summary>
        ///     Creates a new user
        /// </summary>
        /// <param name="username">The user name</param>
        /// <param name="email">The user email</param>
        /// <param name="nameFirst">The first name</param>
        /// <param name="nameLast">The last name</param>
        /// <param name="isAdministrator">Indicates if the user is an administrator</param>
        public User(string username, string email, string nameFirst, string nameLast, bool isAdministrator) : this(username)
        {
            Condition.Requires(email, "email").IsNotNullOrWhiteSpace().IsShorterOrEqual(128);
            Condition.Requires(nameFirst, "nameFirst").IsNotNullOrWhiteSpace().IsShorterOrEqual(50);
            Condition.Requires(nameLast, "nameLast").IsNotNullOrWhiteSpace().IsShorterOrEqual(50);

            Email = email;
            IsAdmin = isAdministrator;
            NameFirst = nameFirst;
            NameLast = nameLast;
        }

        /// <summary>
        ///     Creates a new user
        /// </summary>
        /// <param name="username">The user name</param>
        public User(string username) : this()
        {
            Condition.Requires(username, "username").IsNotNullOrWhiteSpace().IsShorterOrEqual(50);

            Theme = Constants.Profile.DefaultTheme;
            Username = username;
        }

        protected User()
        {
            PasswordFailuresSinceLastSuccess = 0;
            TimezoneUtcOffset = -5;
        }

        public string Email { get; set; }

        public string Avatar
        {
            get
            {
                var hash = GravatarUtil.HashEmailForGravatar(Email);
                return string.Format(Configuration.Gravatar.Url, hash);
            }
        }

        public bool Archived { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? LastPasswordFailureDate { get; set; }

        public string NameFirst { get; set; }

        public string NameLast { get; set; }

        public DateTime? PasswordChangeDate { get; set; }

        public int PasswordFailuresSinceLastSuccess { get; set; }

        public string Theme { get; set; }

        public short TimezoneUtcOffset { get; set; }

        public string Username { get; protected set; }

        internal string PasswordHash { get; set; }

        /// <summary>
        ///     Set user password.
        /// </summary>
        /// <param name="password">The password</param>
        public void SetPassword(string password)
        {
            Condition.Requires(password, "password").IsNotNullOrWhiteSpace();
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            PasswordChangeDate = DateTime.UtcNow;
        }

        /// <summary>
        ///     Validates a user password.
        /// </summary>
        /// <param name="password">The password to validate</param>
        public bool VerifyPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(PasswordHash))
                return false;

            var valid = BCrypt.Net.BCrypt.Verify(password, PasswordHash);

            if (valid)
            {
                LastLogin = DateTime.UtcNow;
                PasswordFailuresSinceLastSuccess = 0;
            }
            else
            {
                LastPasswordFailureDate = DateTime.UtcNow;
                PasswordFailuresSinceLastSuccess++;
            }

            return valid;
        }
    }
}