using System.Configuration;

namespace BteamMongoDB.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoConnectionConfig : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get
            {
                return (string)this["server"];
            }
            set
            {
                this["server"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        [ConfigurationProperty("port", IsRequired = true)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set
            {
                this["port"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [ConfigurationProperty("username")]
        public string Username
        {
            get
            {
                return (string)this["username"];
            }
            set
            {
                this["username"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [ConfigurationProperty("password")]
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

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        [ConfigurationProperty("database", IsRequired = true)]
        public string Database
        {
            get
            {
                return (string)this["database"];
            }
            set
            {
                this["database"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        [ConfigurationProperty("query")]
        public string Query
        {
            get
            {
                return (string)this["query"];
            }
            set
            {
                this["query"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        [ConfigurationProperty("pooling", DefaultValue = false)]
        public bool Pooling
        {
            get
            {
                return (bool)this["pooling"];
            }
            set
            {
                this["pooling"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        [ConfigurationProperty("poolsize", DefaultValue = 40)]
        public int PoolSize
        {
            get
            {
                return (int)this["poolsize"];
            }
            set
            {
                this["poolsize"] = value;
            }
        }
    }
}