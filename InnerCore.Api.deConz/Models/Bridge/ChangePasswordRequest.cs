using System;
using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Bridge
{
    [DataContract]
    public class ChangePasswordRequest
    {
        public ChangePasswordRequest(string usename, string oldPassword, string newPassword)
        {
            UserName = usename;

            OldPasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{usename}:{oldPassword}"));
            NewPasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{usename}:{newPassword}"));
        }

        public ChangePasswordRequest(string oldPassword, string newPassword) : this(Constants.DEFAULT_USERNAME, oldPassword, newPassword)
        {

        }

        /// <summary>
        /// The user name (currently only “delight” is supported).
        /// </summary>
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        /// <summary>
        /// The Base64 encoded combination of “username:old password”.
        /// </summary>
        [DataMember(Name = "oldhash")]
        public string OldPasswordHash { get; set; }

        /// <summary>
        /// The Base64 encoded combination of “username:new password”.
        /// </summary>
        [DataMember(Name = "newhash")]
        public string NewPasswordHash { get; set; }
    }
}
