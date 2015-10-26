//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AngularSkeleton.Service.Model.Authentication
{
    /// <summary>
    ///     Model used to authenticate an account.
    /// </summary>
    public class LoginRequestModel : ModelBase
    {
        /// <summary>
        ///     The username to be validated.
        /// </summary>
        [DataMember]
        [Required]
        public string Username { get; set; }

        /// <summary>
        ///     The password associated with this ID.
        /// </summary>
        [DataMember]
        [Required]
        public string Password { get; set; }
    }
}