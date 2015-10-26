//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using AngularSkeleton.Domain.Accounts;
using AngularSkeleton.Domain.Catalog;
using AngularSkeleton.Service.Model.Products;
using AngularSkeleton.Service.Model.Users;
using AutoMapper;

namespace AngularSkeleton.Service.Config
{
    /// <summary>
    ///     Configures the domain to model mappings
    /// </summary>
    public class MapperConfig
    {
        private static bool _isStarting;

        public static void Initialize()
        {
            if (_isStarting) return;

            _isStarting = true;

            Mapper.CreateMap<Product, ProductModel>()
                .ForMember(m => m.DateAdded, opt => opt.MapFrom(src => src.CreatedAt));

            Mapper.CreateMap<User, UserModel>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}