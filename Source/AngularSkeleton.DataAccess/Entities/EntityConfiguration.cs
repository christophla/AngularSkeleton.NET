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
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using AngularSkeleton.DataAccess.Entities.Cache;
using EFCache;
using EFCache.Redis;

namespace AngularSkeleton.DataAccess.Entities
{
    /// <summary>
    ///     Entity database context configuration.
    /// </summary>
    /// <remarks>
    ///     This class will automatically be located by the entity framework
    ///     at runtime. It provides level-2 cache support (in-memory and redis)
    /// </remarks>
    public class EntityConfiguration : DbConfiguration
    {
        private static readonly Lazy<ICache> _instance = new Lazy<ICache>(() =>
        {
            switch (Configuration.Cache.Mode)
            {
                case CacheMode.Redis:
                    return new RedisCache(Configuration.Cache.Redis.ConnectionSting);
                case CacheMode.InMemory:
                    return new InMemoryCache();
                default:
                    throw new Exception("Unsupported cache mode.");
            }
        });

        public EntityConfiguration()
        {
            if (!Configuration.Cache.Enabled)
                return;

            var transactionHandler = new CacheTransactionHandler(_instance.Value);

            AddInterceptor(transactionHandler);

            Loaded += (sender, args) => args.ReplaceService<DbProviderServices>(
                (s, _) => new CachingProviderServices(s, transactionHandler));
        }
    }
}