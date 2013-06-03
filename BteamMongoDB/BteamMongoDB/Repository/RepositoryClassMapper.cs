using System;
using MongoDB.Bson.Serialization;

namespace BteamMongoDB.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public static class RepositoryClassMapper
    {

        /// <summary>
        /// Registers the class map.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        public static void RegisterClassMap<TClass>()
        {
            BsonClassMap.RegisterClassMap<TClass>();
        }

        /// <summary>
        /// Registers the serializer.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bsonSerializer">The bson serializer.</param>
        public static void RegisterSerializer(Type type, IBsonSerializer bsonSerializer)
        {
            BsonSerializer.RegisterSerializer(type, bsonSerializer);
        }

    }
}