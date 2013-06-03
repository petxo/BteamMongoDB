using MongoDB.Bson.Serialization.Attributes;

namespace BteamMongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractMongoEntity<TId>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [BsonId]
        public TId Id { get; set; }
    }
}