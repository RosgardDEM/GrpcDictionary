namespace GrpcDictionary.Services
{
    public interface IDictionarySrorage
    {
        void Add(string key, string? value);

        void Remove(string key);

        (string Key, string? Value) Get(string key);

        IEnumerable<(string Key, string? Value)> GetAll();

        int Count();

        void Clear();
    }
}
