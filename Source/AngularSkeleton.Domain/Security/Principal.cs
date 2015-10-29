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
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using AngularSkeleton.Common;
using AngularSkeleton.Common.Util;
using CuttingEdge.Conditions;

namespace AngularSkeleton.Domain.Security
{
    /// <summary>
    ///     Application claims principal
    /// </summary>
    public class Principal : ClaimsPrincipal
    {
        public const string IssuedKey = "http://skeletonapp.com/identity/claims/issued";
        public const string UserIdKey = "http://skeletonapp.com/identity/claims/userid";

        public Principal(IIdentity identity) : base(identity)
        {
        }

        /// <summary>
        ///     Indicates if the principal is an administrator.
        /// </summary>
        public bool IsAdministrator
        {
            get { return IsInRole(Constants.Permissions.Administrator); }
        }

        /// <summary>
        ///     The user id
        /// </summary>
        public long UserId
        {
            get
            {
                var claim = FindFirst(o => o.Type == UserIdKey);
                return claim != null ? ConversionUtil.Long(claim.Value) : default(long);
            }
        }

        /// <summary>
        ///     The username
        /// </summary>
        public string Username
        {
            get { return Identity.Name; }
        }

        /// <summary>
        ///     The current principal
        /// </summary>
        public new static Principal Current
        {
            get
            {
                var principal = Thread.CurrentPrincipal as ClaimsPrincipal;
                return null == principal ? null : new Principal(principal.Identities.First());
            }
        }

        /// <summary>
        ///     creates a new principal
        /// </summary>
        /// <param name="authenticationType"></param>
        /// <param name="username">The username</param>
        /// <param name="userId">The user id</param>
        /// <param name="isAdministrator">Indicates if user is administrator</param>
        public static ClaimsIdentity CreateIdentity(string authenticationType, string username, long userId = 0, bool isAdministrator = false)
        {
            Condition.Requires(authenticationType, "authenticationType").IsNotNullOrWhiteSpace();
            Condition.Requires(username, "username").IsNotNullOrWhiteSpace().IsLongerOrEqual(1);

            var claims = new List<Claim>
            {
                new Claim(IssuedKey, DateTime.UtcNow.ToUnixTime().ToString()),
                new Claim(UserIdKey, userId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, Constants.Permissions.User)
            };

            if (isAdministrator)
                claims.Add(new Claim(ClaimTypes.Role, Constants.Permissions.Administrator));

            var identity = new ClaimsIdentity(authenticationType);
            identity.AddClaims(claims);

            return identity;
        }
    }
}