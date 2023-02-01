public static class SaveableExtensions
{
    public static bool IsValid(this ISaveable saveable)
    {
        if (saveable == null) return false;

        return saveable.ValidateSave();
    }
}
