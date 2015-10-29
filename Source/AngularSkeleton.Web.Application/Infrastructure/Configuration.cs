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
using AngularSkeleton.Common.Configuration;

namespace AngularSkeleton.Web.Application.Infrastructure
{
    /// <summary>
    ///     Configuration for the web application.
    /// </summary>
    internal sealed class Configuration
    {
        private const string OAuthAccessTokenExpireTimeSpanKey = "SkeletonApp.Web.OAuth.AccessTokenExpireTimeSpan";
        private const string OAuthAllowInsecureHttpKey = "SkeletonApp.Web.OAuth.AllowInsecureHttp";

        private static readonly IConfigurationStore _configurationStore;

        static Configuration()
        {
            var factory = new ConfigurationStoreFactory();
            _configurationStore = factory.GetStore();
        }

        public static class AccessToken
        {
            public static bool AllowInsecureHttp
            {
                get { return _configurationStore.GetSetting(OAuthAllowInsecureHttpKey, false); }
            }

            public static TimeSpan ExpireTimeSpan
            {
                get { return _configurationStore.GetSetting(OAuthAccessTokenExpireTimeSpanKey, TimeSpan.FromDays(365)); }
            }
        }
    }
}