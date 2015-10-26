//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Configuration;

namespace AngularSkeleton.Common.Configuration
{
    /// <summary>
    ///     A configuration store.
    /// </summary>
    public interface IConfigurationStore
    {
        /// <summary>
        ///     Returns an application setting casted to defined type.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="key">The application settings key</param>
        /// <param name="defaultValue">The default value</param>
        T GetSetting<T>(string key, T defaultValue);

        /// <summary>
        ///     Returns an application setting as a string.
        /// </summary>
        /// <param name="key">The application settings key</param>
        /// <param name="defaultValue">The default value</param>
        string GetSetting(string key, string defaultValue);

        /// <summary>
        ///     Returns a configuration section.
        /// </summary>
        /// <typeparam name="T">The type for the config section</typeparam>
        /// <param name="sectionName">The section name.</param>
        T GetSection<T>(string sectionName) where T : ConfigurationSection;
    }
}