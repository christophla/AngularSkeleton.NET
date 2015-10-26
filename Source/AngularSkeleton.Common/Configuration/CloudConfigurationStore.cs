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
using System.Configuration;
using Microsoft.Azure;

namespace AngularSkeleton.Common.Configuration
{
    /// <summary>
    ///     Store for accessing settings in the cloud configuration manager.
    /// </summary>
    public class CloudConfigurationStore : IConfigurationStore
    {
        public T GetSetting<T>(string key, T defaultValue)
        {
            try
            {
                var setting = CloudConfigurationManager.GetSetting(key);

                if (string.IsNullOrWhiteSpace(setting))
                    return defaultValue;

                var result = (T) Convert.ChangeType(setting, typeof (T));

                if (null == result)
                    throw new Exception();

                return result;
            }
            catch
            {
                return defaultValue;
            }
        }

        public string GetSetting(string key, string defaultValue)
        {
            var setting = CloudConfigurationManager.GetSetting(key);
            return (string.IsNullOrWhiteSpace(setting)) ? defaultValue : setting;
        }

        public T GetSection<T>(string sectionName) where T : ConfigurationSection
        {
            throw new NotImplementedException("Cloud configuration does not support sections");
        }
    }
}