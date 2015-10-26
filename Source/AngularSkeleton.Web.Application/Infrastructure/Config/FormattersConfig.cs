using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AngularSkeleton.NET.WebApplication.Infrastructure.Config
{
    /// <summary>
    ///     Configures the json formatters
    /// </summary>
    public class FormattersConfig
    {
        /// <summary>
        ///     Registers the configuration
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            var isoConverter = new IsoDateTimeConverter {DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffK"};
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(isoConverter);
        }
    }
}