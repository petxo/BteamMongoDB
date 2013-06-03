using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace BteamMongoDB.Repository
{
    /// <summary>
    /// Mongo Index basic information
    /// </summary>
    public class MongoIndex
    {
        public string Key { get; set; }
        public bool Ascending { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Create the index key.
        /// </summary>
        /// <returns></returns>
        public IMongoIndexKeys GetKey()
        {
            return Ascending ? IndexKeys.Ascending(Key) : IndexKeys.Descending(Key);
        }
    }
}