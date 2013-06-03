using MongoDB.Driver;

namespace BteamMongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoSettings : IMongoSettings
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MongoSettings"/> is pooled.
        /// </summary>
        /// <value><c>true</c> if pooled; otherwise, <c>false</c>.</value>
        public bool Pooling { get; set; }

        /// <summary>
        /// Gets or sets the size of the pool.
        /// </summary>
        /// <value>The size of the pool.</value>
        public int PoolSize { get; set; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get
            {
                string authentication = string.Empty;
                if (!string.IsNullOrEmpty(Username))
                {
                    authentication = string.Concat(Username, ':', Password, '@');
                }
                if (!string.IsNullOrEmpty(Query) && !Query.StartsWith("?"))
                {
                    Query = string.Concat('?', Query);
                }

                var connection = string.Format("mongodb://{0}{1}:{2}/{3}{4}?pooling={5}&poolsize={6}", authentication, Server, Port, Database, Query, Pooling, PoolSize);

                return connection;
            }
        }

        /// <summary>
        /// Gets the settigns.
        /// </summary>
        /// <returns></returns>
        public MongoServerSettings GetSettigns()
        {
            var mongoServerSettings = new MongoServerSettings
                                          {
                                              Server = new MongoServerAddress(Server, Port),
                                              MaxConnectionPoolSize = PoolSize
                                          };

            return mongoServerSettings;
        }
    }
}