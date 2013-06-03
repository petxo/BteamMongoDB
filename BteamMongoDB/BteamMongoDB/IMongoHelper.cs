using MongoDB.Driver;

namespace BteamMongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMongoHelper : IContext
    {
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        MongoDatabase Repository { get; }

        /// <summary>
        /// Gets a new Instance
        /// </summary>
        void Generate();
    }
}