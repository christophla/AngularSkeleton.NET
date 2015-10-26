//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

namespace AngularSkeleton.Service
{
    /// <summary>
    ///     Facade for accessing services
    /// </summary>
    public interface IServiceFacade
    {
        /// <summary>
        ///     The product catalog management service
        /// </summary>
        ICatalogService Catalog { get; set; }

        /// <summary>
        ///     The system management service
        /// </summary>
        IManagementService Management { get; set; }

        /// <summary>
        ///     The security service
        /// </summary>
        ISecurityService Security { get; set; }
    }
}