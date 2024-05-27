using Grpc.Core;

namespace GrpcDictionary.Services
{
    public class DictionaryStorageService : IDictionary
    {
        private readonly IDictionary<string, string?> _dictionary;

        public DictionaryStorageService()
        {
            _dictionary = new Dictionary<string, string?>();
        }

        public void Add(string key, string? value)
        {
            if (_dictionary.ContainsKey(key))
            {
                throw new RpcException(new Status(StatusCode.AlreadyExists, $"Item with key \"{key}\" already exist."));
            }


            _dictionary.Add(key, value);
        }

        public void Remove(string key)
        {
            if (!_dictionary.ContainsKey(key)) {
                throw new RpcException(new Status(StatusCode.NotFound, $"Item with key \"{key}\" doesn't exist."));
            }

            _dictionary.Remove(key);
        }

        public (string Key, string? Value) Get(string key)
        {
            if (!_dictionary.ContainsKey(key))
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Item with key \"{key}\" doesn't exist."));
            }

            return (key, _dictionary[key]);
        }

        public IEnumerable<(string Key, string? Value)> GetAll()
        {
            return _dictionary.Select(item => (item.Key, item.Value)).ToArray();
        }
    }
}
