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
using AngularSkeleton.Common.Configuration;
using AngularSkeleton.DataAccess.Entities.Cache;

namespace AngularSkeleton.DataAccess
{
    /// <summary>
    ///     Configuration for the data access layer.
    /// </summary>
    internal sealed class Configuration
    {
        private const string CacheEnabledKey = "SkeletonApp.DataAccess.Cache.Enabled";
        private const string CacheModeKey = "SkeletonApp.DataAccess.Cache.Mode";
        private const string CacheModeSilidingAbsoluteMinutesKey = "SkeletonApp.DataAccess.Cache.AbsoluteMinutes";
        private const string CacheModeSilidingExpirationKey = "SkeletonApp.DataAccess.Cache.Expiration";
        private const string CacheRedisConnectionStringKey = "SkeletonApp.DataAccess.Cache.Redis.ConnectionString";
        private const string DatabasePrefixKey = "SkeletonApp.DataAccess.Database.Prefix";
        private const string DatabaseSchemaKey = "SkeletonApp.DataAccess.Database.Schema";
        private const string EnableViewCacheKey = "SkeletonApp.DataAccess.ViewCache.Enabled";
        private static readonly IConfigurationStore _configurationStore;

        static Configuration()
        {
            var factory = new ConfigurationStoreFactory();
            _configurationStore = factory.GetStore();
        }

        public static class Cache
        {
            public static int AbsoluteExpirationMinutes
            {
                get { return _configurationStore.GetSetting(CacheModeSilidingAbsoluteMinutesKey, 10); }
            }

            public static bool Enabled
            {
                get { return _configurationStore.GetSetting(CacheEnabledKey, false); }
            }

            public static CacheMode Mode
            {
                get { return _configurationStore.GetSetting(CacheModeKey, CacheMode.InMemory); }
            }

            public static int SlidingExpiration
            {
                get { return _configurationStore.GetSetting(CacheModeSilidingExpirationKey, 5); }
            }

            public static class Redis
            {
                public static string ConnectionSting
                {
                    get { return _configurationStore.GetSetting(CacheRedisConnectionStringKey, string.Empty); }
                }
            }
        }

        public static class Database
        {
            public static bool EnableViewCache
            {
                get { return _configurationStore.GetSetting(EnableViewCacheKey, false); }
            }

            public static string Prefix
            {
                get { return _configurationStore.GetSetting(DatabasePrefixKey, string.Empty); }
            }

            public static string Schema
            {
                get { return _configurationStore.GetSetting(DatabaseSchemaKey, Constants.DataAccess.Schema); }
            }
        }
    }
}