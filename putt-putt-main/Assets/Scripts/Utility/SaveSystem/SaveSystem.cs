using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Base class for file saving.
/// </summary>
public abstract class SaveSystem<T> where T : ISaveable
{
    private readonly SaveChannelId SaveChannelId;
    private readonly SaveRecordId SaveRecordId;
    protected readonly string SavePath;
    private readonly string BackupSavePath;
    protected abstract string GetFileExtension();

    public SaveSystem(SaveChannelId saveChannelId, SaveRecordId saveRecordId)
    {
        this.SaveChannelId = saveChannelId;
        this.SaveRecordId = saveRecordId;
        this.SavePath = $"{Application.persistentDataPath}/{saveChannelId}/{saveRecordId}{GetFileExtension()}";
        this.BackupSavePath = SavePath + ".old";
    }
    
    /// <summary>
    /// Safely attempt to serialise and save data to a file
    /// </summary>
    public void Save(T saveData)
    {
        var hasSaveFile = File.Exists(SavePath);

        if (hasSaveFile)
        {
            System.IO.File.Move(SavePath, BackupSavePath);
        }

        Directory.CreateDirectory(Path.GetDirectoryName(SavePath));

        try
        {
            SerialiseToFile(saveData);

            if (hasSaveFile) File.Delete(BackupSavePath);
        }
        catch
        {
            if (hasSaveFile) System.IO.File.Move(BackupSavePath, SavePath);
        }
    }

    /// <summary>
    /// Load data from a file
    /// </summary>
    public T Load()
    {
        if (!File.Exists(SavePath)) return default(T);

        return DeserialiseFromFile();
    }

    /// <summary>
    /// Delete the save file
    /// </summary>
    public bool Delete()
    {
        if (!File.Exists(SavePath)) return false;

        File.Delete(SavePath);

        return true;
    }

    /// <summary>
    /// Serialise data into a file of the given format
    /// </summary>
    protected abstract void SerialiseToFile(T saveData);

    /// <summary>
    /// Deserialise data from a file in a given format
    /// </summary>
    protected abstract T DeserialiseFromFile();
}
