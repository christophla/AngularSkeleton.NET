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
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Security.Principal;
using System.Text;
using System.Threading;
using AngularSkeleton.Common.Resources;
using AngularSkeleton.DataAccess.Entities;
using NLog;

namespace AngularSkeleton.DataAccess.Migrations
{
    /// <summary>
    ///     Configures the database.
    /// </summary>
    /// <remarks>
    ///     Seeds data depending on the build mode (Debug or Release).
    /// </remarks>
    internal sealed class Configuration : DbMigrationsConfiguration<EntityContext>
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        internal static string DefaultAdminUsername = "administrator";
        internal static string DefaultPassword = "password";
        internal static string DefaultUserUsername = "user";
        internal static string DefaultEmail = "test@superexpert.com";

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EntityContext context)
        {
            try
            {
                // sql auditing
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("MIGRATION"), null);

                SeedCommonData.Seed(context);

#if DEBUG
                SeedDevelopmentData.Seed(context);
#endif
#if RELEASE
                SeedReleaseData.Seed(context);
#endif

                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var message = new StringBuilder();

                foreach (var eve in ex.EntityValidationErrors)
                {
                    message.AppendFormat(ExceptionResources.Error_EntityValidation, eve.Entry.Entity.GetType().Name, eve.Entry.State, ex);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message.AppendLine().AppendFormat(ExceptionResources.Error_EntityValidation_Property, ve.PropertyName, ve.ErrorMessage);
                    }
                }

                _logger.Error(message.ToString());
                throw new Exception(message.ToString());
            }
        }
    }
}