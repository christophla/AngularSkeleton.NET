using System.Collections.Generic;
using System.Threading.Tasks;
using AngularSkeleton.Service.Model.Products;
using AngularSkeleton.Service.Model.Users;

namespace AngularSkeleton.Service
{
    /// <summary>
    ///     Service for managing the system
    /// </summary>
    public interface IManagementService
    {
        /// <summary>
        ///     Creates a product
        /// </summary>
        /// <param name="model">The client model</param>
        Task<ProductModel> CreateProductAsync(ProductAddModel model);

        /// <summary>
        ///     Creates a new user
        /// </summary>
        Task<UserModel> CreateUserAsync(UserAddModel model);

        /// <summary>
        ///     Deletes a product
        /// </summary>
        /// <param name="productId">The product id</param>
        Task<int> DeleteProductAsync(long productId);

        /// <summary>
        ///     Removes a user
        /// </summary>
        /// <param name="userId">The user id</param>
        Task<int> DeleteUserAsync(long userId);

        /// <summary>
        ///     Returns a collection of all products
        /// </summary>
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();

        /// <summary>
        ///     Returns a collection of all users
        /// </summary>
        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        /// <summary>
        ///     Returns the currently logged-in user
        /// </summary>
        Task<UserModel> GetCurrentUserAsync();

        /// <summary>
        ///     Retrieves a single product
        /// </summary>
        /// <param name="productId">The product id</param>
        Task<ProductModel> GetProductAsync(long productId);

        /// <summary>
        ///     Returns a user with given id
        /// </summary>
        /// <param name="userId">The user id</param>
        Task<UserModel> GetUserAsync(long userId);

        /// <summary>
        ///     Toggles a product's archive status
        /// </summary>
        /// <param name="productId">The product id</param>
        Task<int> ToggleProductAsync(long productId);

        /// <summary>
        ///     Toggles a user's archive status
        /// </summary>
        /// <param name="userId">The user id</param>
        Task<int> ToggleUserAsync(long userId);

        /// <summary>
        ///     Updates a product
        /// </summary>
        /// <param name="model">The product model</param>
        /// <param name="productId">The product id</param>
        Task<int> UpdateProductAsync(ProductUpdateModel model, long productId);

        /// <summary>
        ///     Updates a user
        /// </summary>
        /// <param name="model">The user model</param>
        /// <param name="userId">The user id</param>
        Task<int> UpdateUserAsync(UserUpdateModel model, long userId);

        /// <summary>
        ///     Indicates if a username is already in-use
        /// </summary>
        /// <param name="username">The user name</param>
        Task<bool> UsernameExistsAsync(string username);
    }
}