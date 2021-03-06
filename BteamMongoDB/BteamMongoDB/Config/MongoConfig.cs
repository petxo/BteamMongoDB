using System.Configuration;

namespace BteamMongoDB.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoConfig : ConfigurationSection
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
        /// Gets the sessions.
        /// </summary>
        /// <value>The sessions.</value>
        [ConfigurationProperty("connections", IsRequired = true)]
        public MongoConnectionListConfig Connections
        {
            get
            {
                return (MongoConnectionListConfig)this["connections"] ?? new MongoConnectionListConfig();
            }
        }
    }
}