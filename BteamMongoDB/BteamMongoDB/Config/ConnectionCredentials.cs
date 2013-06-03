using System.Configuration;

namespace BteamMongoDB.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionCredentials : ConfigurationElement
    {

        /// <summary>
        /// Gets or sets the admin.
        /// </summary>
        /// <value>The admin.</value>
        [ConfigurationProperty("admin", DefaultValue = true)]
        public bool Admin
        {
            get
            {
                return (bool)this["admin"];
            }
            set
            {
                this["admin"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        [ConfigurationProperty("user", DefaultValue = "")]
        public string User
        {
            get
            {
                return (string)this["user"];
            }
            set
            {
                this["user"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [ConfigurationProperty("password", DefaultValue = "")]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
            set
            {
                this["password"] = value;
            }
        }
    }
}