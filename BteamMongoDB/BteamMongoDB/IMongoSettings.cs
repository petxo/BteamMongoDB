using MongoDB.Driver;

namespace BteamMongoDB
{
    public interface IMongoSettings
    {
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        string Database { get; set; }

        /// <summary>
        /// Gets the settigns.
        /// </summary>
        /// <returns></returns>
        MongoServerSettings GetSettigns();
    }
}