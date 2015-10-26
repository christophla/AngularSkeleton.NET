//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Web.Http;
using AngularSkeleton.Service;
using AngularSkeleton.Web.Application.Infrastructure.Attributes;
using CuttingEdge.Conditions;

namespace AngularSkeleton.Web.Application.Controllers
{
    /// <summary>
    ///     Base controller.
    /// </summary>
    [Authorize]
    [ExceptionHandling]
    public abstract class ControllerBase : ApiController
    {
        /// <summary>
        ///     Protected abstract constructor.
        /// </summary>
        /// <param name="services">Instance of the services facade</param>
        protected ControllerBase(IServiceFacade services)
        {
            Condition.Requires(services, "services").IsNotNull();
            Services = services;
        }

        /// <summary>
        ///     The tenant secure context.
        /// </summary>
        protected IServiceFacade Services { get; private set; }
    }
}