using AutoMapper;

namespace AngularSkeleton.NET.WebApplication.Infrastructure.Config
{
    /// <summary>
    ///     Configures auto mapper
    /// </summary>
    public class MapperConfig
    {
        private static bool _isStarting;

        /// <summary>
        ///     Initializes the configuration
        /// </summary>
        public static void Initialize()
        {
            if (_isStarting) return;

            _isStarting = true;
            Mapper.AssertConfigurationIsValid();
        }
    }
}