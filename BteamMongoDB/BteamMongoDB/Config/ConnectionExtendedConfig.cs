using System.Collections.Generic;
using System.Configuration;
using MongoDB.Driver;

namespace BteamMongoDB.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionExtendedConfig : ConfigurationElement
    {
        private IEnumerable<MongoServerAddress> _servers;

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
        /// Gets or sets the connection mode.
        /// </summary>
        /// <value>The connection mode.</value>
        [ConfigurationProperty("connectionMode", DefaultValue = ConnectionMode.Direct)]
        public ConnectionMode ConnectionMode
        {
            get
            {
                return (ConnectionMode)this["connectionMode"];
            }
            set
            {
                this["connectionMode"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the read preference mode.
        /// </summary>
        /// <value>The read preference mode.</value>
        [ConfigurationProperty("readPreferenceMode", DefaultValue = ReadPreferenceMode.Primary)]
        public ReadPreferenceMode ReadPreferenceMode
        {
            get
            {
                return (ReadPreferenceMode)this["readPreferenceMode"];
            }
            set
            {
                this["readPreferenceMode"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the connect timeout (seconds).
        /// </summary>
        /// <value>The connect timeout.</value>
        [ConfigurationProperty("connectTimeout", DefaultValue = 30)]
        public int ConnectTimeout
        {
            get
            {
                return (int)this["connectTimeout"];
            }
            set
            {
                this["connectTimeout"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the default credentials.
        /// </summary>
        /// <value>The default credentials.</value>
        [ConfigurationProperty("defaultCredentials")]
        public ConnectionCredentials DefaultCredentials
        {
            get
            {
                return (ConnectionCredentials)this["defaultCredentials"] ?? new ConnectionCredentials();
            }
            set
            {
                this["defaultCredentials"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the max connection idle time (seconds).
        /// </summary>
        /// <value>The max connection idle time.</value>
        [ConfigurationProperty("maxConnectionIdleTime", DefaultValue = 300)]
        public int MaxConnectionIdleTime
        {
            get
            {
                return (int)this["maxConnectionIdleTime"];
            }
            set
            {
                this["maxConnectionIdleTime"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the max connection life time (seconds).
        /// </summary>
        /// <value>The max connection life time.</value>
        [ConfigurationProperty("maxConnectionLifeTime", DefaultValue = 600)]
        public int MaxConnectionLifeTime
        {
            get
            {
                return (int)this["maxConnectionLifeTime"];
            }
            set
            {
                this["maxConnectionLifeTime"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the max connection pool.
        /// </summary>
        /// <value>The size of the max connection pool.</value>
        [ConfigurationProperty("maxConnectionPoolSize", DefaultValue = 100)]
        public int MaxConnectionPoolSize
        {
            get
            {
                return (int)this["maxConnectionPoolSize"];
            }
            set
            {
                this["maxConnectionPoolSize"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the min connection pool.
        /// </summary>
        /// <value>The size of the min connection pool.</value>
        [ConfigurationProperty("minConnectionPoolSize", DefaultValue = 0)]
        public int MinConnectionPoolSize
        {
            get
            {
                return (int)this["minConnectionPoolSize"];
            }
            set
            {
                this["minConnectionPoolSize"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the replica set.
        /// </summary>
        /// <value>The name of the replica set.</value>
        [ConfigurationProperty("replicaSetName")]
        public string ReplicaSetName
        {
            get
            {
                return (string)this["replicaSetName"];
            }
            set
            {
                this["replicaSetName"] = value;
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
        /// Gets or sets the safe mode.
        /// </summary>
        /// <value>The safe mode.</value>
        [ConfigurationProperty("safeMode", DefaultValue = false)]
        public bool SafeMode
        {
            get
            {
                return (bool)this["safeMode"];
            }
            set
            {
                this["safeMode"] = value;
            }
        }


        /// <summary>
        /// Gets the sessions.
        /// </summary>
        /// <value>The sessions.</value>
        [ConfigurationProperty("servers", IsRequired = true)]
        public ServerList Servers
        {
            get
            {
                return (ServerList)this["servers"] ?? new ServerList();
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [slave ok].
        /// </summary>
        /// <value><c>true</c> if [slave ok]; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("slaveOk", DefaultValue = false)]
        public bool SlaveOk
        {
            get
            {
                return (bool)this["slaveOk"];
            }
            set
            {
                this["slaveOk"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the socket timeout (seconds).
        /// </summary>
        /// <value>The socket timeout.</value>
        [ConfigurationProperty("socketTimeout", DefaultValue = 30)]
        public int SocketTimeout
        {
            get
            {
                return (int)this["socketTimeout"];
            }
            set
            {
                this["socketTimeout"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the wait queue.
        /// </summary>
        /// <value>The size of the wait queue.</value>
        [ConfigurationProperty("waitQueueSize", DefaultValue = 0)]
        public int WaitQueueSize
        {
            get
            {
                return (int)this["waitQueueSize"];
            }
            set
            {
                this["waitQueueSize"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the wait queue timeout (miliseconds).
        /// </summary>
        /// <value>The wait queue timeout.</value>
        [ConfigurationProperty("waitQueueTimeout", DefaultValue = 500)]
        public int WaitQueueTimeout
        {
            get
            {
                return (int)this["waitQueueTimeout"];
            }
            set
            {
                this["waitQueueTimeout"] = value;
            }
        }
    }
}