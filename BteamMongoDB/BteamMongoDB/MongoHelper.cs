using System;
using MongoDB.Driver;
using log4net;

namespace BteamMongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoHelper : IMongoHelper
    {
        private readonly IMongoSettings _mongoSettings;
        private static readonly object SyncGenerate = new object();

        private MongoServer _mongo;
        private MongoDatabase _database;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILog Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoHelper"/> class.
        /// </summary>
        /// <param name="mongoSettings">The mongo settings.</param>
        internal MongoHelper(IMongoSettings mongoSettings)
        {
            _mongoSettings = mongoSettings;
            _mongo = MongoServer.Create(mongoSettings.GetSettigns());
            _database = _mongo.GetDatabase(mongoSettings.Database);
        }

        #region IMongoHelper Members

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        public MongoDatabase Repository
        {
            get { return _database; }
        }

        /// <summary>
        /// Gets a new Instance
        /// </summary>
        public void Generate()
        {
            lock (SyncGenerate)
            {
                _mongo = MongoServer.Create(_mongoSettings.GetSettigns());
                _database = _mongo.GetDatabase(_mongoSettings.Database);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logger.Debug("Disposing Mongo Helper");
                _mongo.Disconnect();
            }
        }
    }
}