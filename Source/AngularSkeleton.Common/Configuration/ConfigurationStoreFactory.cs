//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using Microsoft.WindowsAzure.ServiceRuntime;

namespace AngularSkeleton.Common.Configuration
{
    public class ConfigurationStoreFactory
    {
        /// <summary>
        ///     Returns a configuration store.
        /// </summary>
        /// <remarks>
        ///     A cloud store will be returned when running on azure.
        /// </remarks>
        /// <returns></returns>
        public IConfigurationStore GetStore()
        {
            if (RoleEnvironment.IsAvailable)
                return new CloudConfigurationStore();

            return new ConfigurationManagerStore();
        }
    }
}