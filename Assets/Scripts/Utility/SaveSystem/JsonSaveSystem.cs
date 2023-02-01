using UnityEngine;
using System.IO;

public class JsonSaveSystem<T> : SaveSystem<T> where T : ISaveable
{
    protected override string GetFileExtension() => ".json";

    public JsonSaveSystem(SaveChannelId saveChannel, SaveRecordId fileName) : base(saveChannel, fileName) {}

    /// <summary>
    /// Implements <see cref="SaveSystem.SerialiseToFile"/>
    /// </summary>
    protected override void SerialiseToFile(T saveData)
    {
        string jsonData = JsonUtility.ToJson(saveData);

        File.WriteAllText(SavePath, jsonData);
    }

    /// <summary>
    /// Implements <see cref="SaveSystem.DeserialiseFromFile"/>
    /// </summary>
    protected override T DeserialiseFromFile()
    {
        var fileContents = File.ReadAllText(SavePath);

        var saveData = JsonUtility.FromJson<T>(fileContents);

        return saveData;
    }
}
