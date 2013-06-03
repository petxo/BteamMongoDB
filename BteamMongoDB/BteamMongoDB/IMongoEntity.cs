namespace BteamMongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMongoEntity<TId>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        TId Id { get; set; }
    }
}