namespace GrpcDictionary.Services
{
    public class DictionaryService : IDictionary
    {
        private readonly ILogger<DictionaryService> _logger;
        private readonly IDictionary<string, string?> _dictionary;

        public DictionaryService(ILogger<DictionaryService> logger)
        {
            _logger = logger;
            _dictionary = new Dictionary<string, string?>();
        }

        public void Add(string key, string? value)
        {
            _dictionary.Add(key, value);
        }

        public void Remove(string key)
        {
            _dictionary.Remove(key);
        }

        public (string Key, string? Value) Get(string key)
        {
            return (key, _dictionary[key]);
        }

        public IEnumerable<(string Key, string? Value)> GetAll()
        {
            return _dictionary.Select(item => (item.Key, item.Value)).ToArray();
        }
    }
}
