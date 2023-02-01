using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Saves files in a binary format
/// TODO: Apparently default Binary serialisation isn't great, maybe sunset this system
/// If we want to hide the data from the player, just implement encoding
/// https://www.reddit.com/r/Unity3D/comments/odu2fs/the_nightmare_that_is_binary_saving_in_unity_game/
/// </summary>
public class BinarySaveSystem<T> : SaveSystem<T> where T : class, ISaveable
{
    protected override string GetFileExtension() => ".bin";

    public BinarySaveSystem(SaveChannelId saveChannel, SaveRecordId fileName) : base(saveChannel, fileName) {}

    /// <summary>
    /// Implements <see cref="SaveSystem.SerialiseToFile"/>
    /// </summary>
    protected override void SerialiseToFile(T saveData)
    {
        var formatter = new BinaryFormatter();
        var fileStream = new FileStream(SavePath, FileMode.Create);

        formatter.Serialize(fileStream, saveData);

        fileStream.Close();
    }

    /// <summary>
    /// Implements <see cref="SaveSystem.DeserialiseFromFile"/>
    /// </summary>
    protected override T DeserialiseFromFile()
    {
        var formatter = new BinaryFormatter();
        var fileStream = new FileStream(SavePath, FileMode.Open);

        var saveData = formatter.Deserialize(fileStream) as T;

        fileStream.Close();

        return saveData;
    }
}
