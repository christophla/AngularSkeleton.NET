//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

namespace AngularSkeleton.Common
{
    public static class Constants
    {
        public class Api
        {
            public class V1
            {
                public const string AccessTokenRoute = "/" + RoutePrefix + "/accesstoken"; // oauth requires leading slash
                public const string CatalogRoutePrefix = RoutePrefix + "/catalog";
                public const string ManageRoutePrefix = RoutePrefix + "/manage";
                public const string RoutePrefix = "api/rest/v1";
            }
        }

        public static class DataAccess
        {
            public const string ConnectionStringName = "SKELETONAPP";
            public const int DefaultRecordsPerQuery = 100;
            public const string Schema = "dbo";
        }

        public class Permissions
        {
            public const string Administrator = "admin";
            public const string User = "user";
        }

        public static class ResponseHeaders
        {
            public static string ErrorCode = "X-Error-Code";
            public static string Hint = "X-Hint";
            public static string HintCode = "X-Hint-Code";
            public static string TotalCount = "X-Total-Count";
        }

        public static class Profile
        {
            public static string DefaultTheme = "dust";
        }
    }
}