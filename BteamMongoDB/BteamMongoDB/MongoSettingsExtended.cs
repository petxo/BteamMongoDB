using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace BteamMongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoSettingsExtended : IMongoSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoSettingsExtended"/> class.
        /// </summary>
        public MongoSettingsExtended()
        {
            Servers = new List<Server>();
        }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        public Credentials UserCredentials { get; set; }

        /// <summary>
        /// Gets or sets the connection mode.
        /// </summary>
        /// <value>The connection mode.</value>
        public ConnectionMode ConnectionMode { get; set; }

        /// <summary>
        /// Gets or sets the read preference.
        /// </summary>
        /// <value>The read preference.</value>
        public ReadPreferenceMode ReadPreferenceMode { get; set; }

        /// <summary>
        /// Gets or sets the connect timeout.
        /// </summary>
        /// <value>The connect timeout.</value>
        public int ConnectTimeout { get; set; }

        /// <summary>
        /// Gets or sets the max connection idle time.
        /// </summary>
        /// <value>The max connection idle time.</value>
        public int MaxConnectionIdleTime { get; set; }

        /// <summary>
        /// Gets or sets the max connection life time.
        /// </summary>
        /// <value>The max connection life time.</value>
        public int MaxConnectionLifeTime { get; set; }

        /// <summary>
        /// Gets or sets the size of the max connection pool.
        /// </summary>
        /// <value>The size of the max connection pool.</value>
        public int MaxConnectionPoolSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the min connection pool.
        /// </summary>
        /// <value>The size of the min connection pool.</value>
        public int MinConnectionPoolSize { get; set; }

        /// <summary>
        /// Gets or sets the name of the replica set.
        /// </summary>
        /// <value>The name of the replica set.</value>
        public string ReplicaSetName { get; set; }

        /// <summary>
        /// Gets or sets the servers.
        /// </summary>
        /// <value>The servers.</value>
        public IList<Server> Servers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [slave ok].
        /// </summary>
        /// <value><c>true</c> if [slave ok]; otherwise, <c>false</c>.</value>
        public bool SlaveOk { get; set; }

        /// <summary>
        /// Gets or sets the socket timeout.
        /// </summary>
        /// <value>The socket timeout.</value>
        public int SocketTimeout { get; set; }

        private int _waitQueueSize;

        /// <summary>
        /// Gets or sets the size of the wait queue.
        /// </summary>
        /// <value>The size of the wait queue.</value>
        public int WaitQueueSize
        {
            get { return _waitQueueSize != 0 ? _waitQueueSize : MaxConnectionPoolSize; }
            set { _waitQueueSize = value; }
        }

        /// <summary>
        /// Gets or sets the wait queue timeout.
        /// </summary>
        /// <value>The wait queue timeout.</value>
        public int WaitQueueTimeout { get; set; }

        /// <summary>
        /// Adds the server.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="port">The port.</param>
        public void AddServer(string name, int port)
        {
            Servers.Add(new Server { Name = name, Port = port });
        }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        public string Database { get; set; }

        /// <summary>
        /// Gets the settigns.
        /// </summary>
        /// <returns></returns>
        public MongoServerSettings GetSettigns()
        {
            var mongoServerSettings = new MongoServerSettings
                                          {
                                              MaxConnectionPoolSize = MaxConnectionPoolSize,
                                              MinConnectionPoolSize = MinConnectionPoolSize,
                                              MaxConnectionIdleTime = TimeSpan.FromSeconds(MaxConnectionIdleTime),
                                              MaxConnectionLifeTime = TimeSpan.FromSeconds(MaxConnectionLifeTime),
                                              ConnectTimeout = TimeSpan.FromSeconds(ConnectTimeout),
                                              ConnectionMode = ConnectionMode,
                                              SocketTimeout = TimeSpan.FromSeconds(SocketTimeout),
                                              WaitQueueSize = WaitQueueSize,
                                              WaitQueueTimeout = TimeSpan.FromMilliseconds(WaitQueueTimeout),
                                              ReadPreference = new ReadPreference(ReadPreferenceMode),
                                              Servers = Servers.Select(
                                                      server => new MongoServerAddress(server.Name, server.Port)).ToList()
                                          };

            if (!string.IsNullOrEmpty(ReplicaSetName))
            {
                mongoServerSettings.ReplicaSetName = ReplicaSetName;
                mongoServerSettings.ConnectionMode = ConnectionMode.ReplicaSet;
            }

            if (UserCredentials != null)
            {
                //mongoServerSettings.Credentials = new MongoCredential(UserCredentials.User, UserCredentials.Password, UserCredentials.Admin);
            }

            return mongoServerSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        public class Server
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the port.
            /// </summary>
            /// <value>The port.</value>
            public int Port { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Credentials
        {
            /// <summary>
            /// Gets or sets the admin.
            /// </summary>
            /// <value>The admin.</value>
            public bool Admin { get; set; }


            /// <summary>
            /// Gets or sets the user.
            /// </summary>
            /// <value>The user.</value>
            public string User { get; set; }


            /// <summary>
            /// Gets or sets the password.
            /// </summary>
            /// <value>The password.</value>
            public string Password { get; set; }
        }

        /// <summary>
        /// Adds the credentials.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="admin">if set to <c>true</c> [admin].</param>
        public void AddCredentials(string user, string password, bool admin)
        {
            UserCredentials = new Credentials()
                                  {
                                      Admin = admin,
                                      User = user,
                                      Password = password
                                  };
        }
    }

}