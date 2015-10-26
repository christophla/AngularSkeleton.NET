namespace AngularSkeleton.DataAccess.Entities.Mappings
{
    /// <summary>
    ///     Database table mappings.
    /// </summary>
    internal static class Tables
    {
        public static string Product = Configuration.Database.Prefix + "Product";
        public static string Schema = Configuration.Database.Schema;
        public static string User = Configuration.Database.Prefix + "User";
    }
}