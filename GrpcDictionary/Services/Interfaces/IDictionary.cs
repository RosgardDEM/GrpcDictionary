namespace GrpcDictionary.Services
{
    public interface IDictionary
    {
        void Add(string key, string? value);

        void Remove(string key);

        (string Key, string? Value) Get(string key);

        IEnumerable<(string Key, string? Value)> GetAll();
    }
}
