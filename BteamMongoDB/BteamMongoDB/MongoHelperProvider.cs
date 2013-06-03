using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using BteamMongoDB.Config;
using log4net.Core;

namespace BteamMongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoHelperProvider
    {
        private const string SectionName = "mongodb";
        private readonly IDictionary<string, IMongoHelper> _mongoHelpers;
        private SpinLock _spinLock;

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static MongoHelperProvider Instance { get; set; }

        /// <summary>
        /// Initializes the <see cref="MongoHelperProvider"/> class.
        /// </summary>
        static MongoHelperProvider()
        {
            Instance = new MongoHelperProvider();
        }

        private MongoHelperProvider()
        {
            _spinLock = new SpinLock();
            _mongoHelpers = new Dictionary<string, IMongoHelper>();

            var mongoDbConfig = ConfigurationManager.GetSection(SectionName);

            if (mongoDbConfig is MongoConfig)
            {
                CreateBasicHelpers((MongoConfig)mongoDbConfig);
                return;
            }
            if(mongoDbConfig is MongoFullConfig)
            {
                CreateExtendedHelpers((MongoFullConfig)mongoDbConfig);
            }
        }

        private void CreateBasicHelpers(MongoConfig mongoDbConfig)
        {
            foreach (MongoConnectionConfig connection in mongoDbConfig.Connections)
            {
                var settings = new MongoSettings
                                   {
                                       Database = connection.Database,
                                       Password = connection.Password,
                                       Port = connection.Port,
                                       Query = connection.Query,
                                       Server = connection.Server,
                                       Username = connection.Username,
                                       Pooling = connection.Pooling,
                                       PoolSize = connection.PoolSize
                                   };

                _mongoHelpers.Add(connection.Name, new MongoHelper(settings));
            }
        }

        private void CreateExtendedHelpers(MongoFullConfig mongoDbConfig)
        {
            foreach (ConnectionExtendedConfig connection in mongoDbConfig.Connections)
            {
                var settings = new MongoSettingsExtended
                {
                    ConnectionMode = connection.ConnectionMode,
                    ConnectTimeout = connection.ConnectTimeout,
                    MaxConnectionIdleTime = connection.MaxConnectionIdleTime,
                    MaxConnectionLifeTime = connection.MaxConnectionLifeTime,
                    MaxConnectionPoolSize = connection.MaxConnectionPoolSize,
                    MinConnectionPoolSize = connection.MinConnectionPoolSize,
                    ReplicaSetName = connection.ReplicaSetName,
                    SlaveOk = connection.SlaveOk,
                    SocketTimeout = connection.SocketTimeout,
                    WaitQueueSize = connection.WaitQueueSize,
                    WaitQueueTimeout = connection.WaitQueueTimeout,
                    Database = connection.Database,
                    ReadPreferenceMode = connection.ReadPreferenceMode
                };

                foreach (ServerConnection server in connection.Servers)
                {
                    settings.AddServer(server.Server, server.Port);
                }


                if(!string.IsNullOrEmpty(connection.DefaultCredentials.User) )
                {
                    settings.AddCredentials(connection.DefaultCredentials.User, connection.DefaultCredentials.Password,
                                            connection.DefaultCredentials.Admin);
                }


                _mongoHelpers.Add(connection.Name, new MongoHelper(settings));
            }
        }


        /// <summary>
        /// Adds the connection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="mongoSettings">The mongo settings.</param>
        public void AddConnection(string key, MongoSettingsExtended mongoSettings)
        {
            bool lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            if (lockTaken)
            {
                if (!_mongoHelpers.ContainsKey(key))
                    _mongoHelpers.Add(key, new MongoHelper(mongoSettings) {  });

                _spinLock.Exit();
            }
        }

        /// <summary>
        /// Gets the helper.
        /// </summary>
        /// <param name="keyConnectionName">Name of the key connection.</param>
        /// <returns></returns>
        public IMongoHelper GetHelper(string keyConnectionName)
        {
            return _mongoHelpers[keyConnectionName];
        }

        ~MongoHelperProvider()
        {
            foreach (var mongoHelper in _mongoHelpers.Values)
            {
                mongoHelper.Dispose();
            }
        }

    }
}