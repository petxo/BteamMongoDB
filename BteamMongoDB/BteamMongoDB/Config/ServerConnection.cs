using System.Configuration;

namespace BteamMongoDB.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerConnection : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        [ConfigurationProperty("server", IsRequired = true, IsKey = true)]
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
        [ConfigurationProperty("port", DefaultValue = 27017 )]
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
         
    }
}