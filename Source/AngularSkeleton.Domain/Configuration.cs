//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using AngularSkeleton.Common.Configuration;

namespace AngularSkeleton.Domain
{
    /// <summary>
    ///     Configuration for the domain layer.
    /// </summary>
    internal sealed class Configuration
    {
        private const string GravatarUrlKey = "SkeletonApp.Domain.Url";
        private static readonly IConfigurationStore _configurationStore;

        static Configuration()
        {
            var factory = new ConfigurationStoreFactory();
            _configurationStore = factory.GetStore();
        }

        public static class Gravatar
        {
            public static string Url
            {
                get { return _configurationStore.GetSetting(GravatarUrlKey, "http://www.gravatar.com/avatar/{0}").TrimEnd('/'); }
            }
        }
    }
}