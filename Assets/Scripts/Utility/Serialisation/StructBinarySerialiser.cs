using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// TODO: Does this need to be specific to structs or can we just adapt it for anything?
namespace Utility.Serialisation
{
    public static class StructBinarySerialiser
    {
        public static byte[] Serialise<T>(T toSerialise) where T : struct
        {
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, toSerialise);
                return memoryStream.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] toDeserialise) where T : struct
        {
            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                memoryStream.Write(toDeserialise, 0, toDeserialise.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var serialised = (T) binaryFormatter.Deserialize(memoryStream);
                return serialised;
            }
        }
    }
}