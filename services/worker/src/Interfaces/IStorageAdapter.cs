namespace bot
{
    public interface IStorageAdapter
    {
        string GetItem(string key);
        void SetItem(string key, string value);
    }
}
