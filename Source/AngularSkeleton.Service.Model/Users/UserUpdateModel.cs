using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AngularSkeleton.Service.Model.Users
{
    /// <summary>
    ///     Model for updating a user
    /// </summary>
    public class UserUpdateModel : ModelBase
    {
        /// <summary>
        ///     The email
        /// </summary>
        [Required]
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        ///     Indicates if user is an administrator
        /// </summary>
        [Required]
        [DataMember]
        public bool IsAdmin { get; set; }

        /// <summary>
        ///     The first name
        /// </summary>
        [Required]
        [DataMember]
        public string NameFirst { get; set; }

        /// <summary>
        ///     The last name
        /// </summary>
        [Required]
        [DataMember]
        public string NameLast { get; set; }

        /// <summary>
        ///     The user theme
        /// </summary>
        [DataMember]
        public string Theme { get; set; }

        /// <summary>
        ///     The user timezone offset
        /// </summary>
        [Required]
        [DataMember]
        public short TimezoneUtcOffset { get; set; }
    }
}