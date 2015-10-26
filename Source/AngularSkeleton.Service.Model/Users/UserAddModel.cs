using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AngularSkeleton.Service.Model.Users
{
    /// <summary>
    ///     Model for adding a user
    /// </summary>
    public class UserAddModel : ModelBase
    {
        /// <summary>
        ///     The user's email
        /// </summary>
        [Required]
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        ///     Indicates if user is an administrator
        /// </summary>
        [DataMember]
        public bool IsAdmin { get; set; }

        /// <summary>
        ///     The first name
        /// </summary>
        [DataMember]
        public string NameFirst { get; set; }

        /// <summary>
        ///     The last name
        /// </summary>
        [DataMember]
        public string NameLast { get; set; }

        /// <summary>
        ///     The login password
        /// </summary>
        [Required]
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        ///     The timezone utc offset
        /// </summary>
        [DataMember]
        public short TimezoneUtcOffset { get; set; }

        /// <summary>
        ///     The login user name
        /// </summary>
        [Required]
        [DataMember]
        public string Username { get; set; }
    }
}