// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using AngularSkeleton.Common;
using AngularSkeleton.Common.Exceptions;
using AngularSkeleton.DataAccess.Repositories;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Domain.Accounts;
using AngularSkeleton.Domain.Catalog;
using AngularSkeleton.Domain.Security;
using AngularSkeleton.Service.Model.Products;
using AngularSkeleton.Service.Model.Users;
using AutoMapper;
using CuttingEdge.Conditions;

namespace AngularSkeleton.Service.Impl
{
    public class ManagementService : IManagementService
    {
        private readonly IRepositoryFacade _repositories;

        public ManagementService(IRepositoryFacade repositories)
        {
            Condition.Requires(repositories, "repositories").IsNotNull();
            _repositories = repositories;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<ProductModel> CreateProductAsync(ProductAddModel model)
        {
            if (await _repositories.Products.AnyAsync(o => o.Name == model.Name))
                throw new BusinessException(string.Format("A product already exists with name: {0}", model.Name));

            var product = new Product(model.Name)
            {
                Description = model.Description,
                QuantityAvailable = model.QuantityAvailable
            };

            _repositories.Products.Insert(product);

            await _repositories.SaveChangesAsync();

            return Mapper.Map<ProductModel>(product);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<UserModel> CreateUserAsync(UserAddModel model)
        {
            if (await UsernameExistsAsync(model.Username))
                throw new BusinessException("The username {0} is in-use", model.Username);

            var user = new User(model.Username, model.Email, model.NameFirst, model.NameLast, model.IsAdmin) {TimezoneUtcOffset = model.TimezoneUtcOffset};
            user.SetPassword(model.Password);

            _repositories.Users.Insert(user);
            await _repositories.SaveChangesAsync();

            //TODO: Send email

            return Mapper.Map<UserModel>(user);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<int> DeleteProductAsync(long productId)
        {
            var product = await _repositories.Products.FindAsync(productId);
            if (null == product)
                return 0;

            _repositories.Products.Remove(product);

            return await _repositories.SaveChangesAsync();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<int> DeleteUserAsync(long userId)
        {
            if (Principal.Current.UserId == userId)
                throw new BusinessException("Logged-in user cannot delete their own account");

            var user = await _repositories.Users.FindAsync(userId);
            if (user == null)
                throw new NotFoundException("The user was not found.");

            _repositories.Users.Remove(user);

            return await _repositories.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var found = await _repositories.Products.GetAllAsync(QueryOptions.AllItems);
            return Mapper.Map<IEnumerable<ProductModel>>(found.Items);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var found = await _repositories.Users.GetAllAsync(QueryOptions.AllItems);
            return Mapper.Map<IEnumerable<UserModel>>(found.Items);
        }

        public async Task<UserModel> GetCurrentUserAsync()
        {
            var principal = Principal.Current;
            if (null == principal)
                throw new SecurityException("Unauthorized");

            var user = await _repositories.Users.FindAsync(principal.UserId);
            if (user == null)
                throw new NotFoundException("The current user was not found");

            return Mapper.Map<User, UserModel>(user);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.User)]
        public async Task<ProductModel> GetProductAsync(long productId)
        {
            var product = await _repositories.Products.FindAsync(productId);
            if (null == product)
                throw new NotFoundException("No product exists with given id.");

            return Mapper.Map<Product, ProductModel>(product);
        }

        public async Task<UserModel> GetUserAsync(long userId)
        {
            var user = await _repositories.Users.FindAsync(userId);
            if (user == null)
                throw new NotFoundException("The user was not found");

            return Mapper.Map<User, UserModel>(user);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<int> ToggleProductAsync(long productId)
        {
            var product = await _repositories.Products.FindAsync(productId);
            product.Archived = !product.Archived;

            return await _repositories.SaveChangesAsync();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<int> ToggleUserAsync(long userId)
        {
            var user = await _repositories.Users.FindAsync(userId);
            user.Archived = !user.Archived;

            return await _repositories.SaveChangesAsync();
        }

        public async Task<int> UpdateProductAsync(ProductUpdateModel model, long productId)
        {
            var product = await _repositories.Products.FindAsync(productId);

            product.Description = model.Description;
            product.Name = model.Name;
            product.QuantityAvailable = model.QuantityAvailable;

            return await _repositories.SaveChangesAsync();
        }

        public async Task<int> UpdateUserAsync(UserUpdateModel model, long userId)
        {
            // user can update themselves

            if (!Principal.Current.IsAdministrator || Principal.Current.UserId != userId)
            {
                throw new SecurityException("You are not authorized to update this user.");
            }

            var user = await _repositories.Users.FindAsync(userId);

            user.Email = model.Email;
            user.IsAdmin = model.IsAdmin;
            user.NameFirst = model.NameFirst;
            user.NameLast = model.NameLast;
            user.Theme = model.Theme;
            user.TimezoneUtcOffset = model.TimezoneUtcOffset;

            // TODO: Send Email

            return await _repositories.SaveChangesAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _repositories.Users.UsernameExistsAsync(username);
        }
    }
}