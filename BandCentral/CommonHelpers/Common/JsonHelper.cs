using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CommonHelpers.Common
{
    /// <summary>
    /// A dependency-free JSON serializer and deserializer.
    /// NOTE: When using this, you need to decorate the model with [DataContract] and the properties with [DataMember] attributes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class JsonHelper<T> where T : class
    {
        /// <summary>
        /// Serializes an object to JSON
        /// </summary>
        public static string Serialize(T instance)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, instance);
                return Encoding.UTF8.GetString(stream.ToArray());//.Default.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// DeSerializes an object from JSON
        /// </summary>
        public static T Deserialize(string json)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }
    }
}