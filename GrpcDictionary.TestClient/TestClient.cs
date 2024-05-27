using GrpcDictionary.Protos;
using GrpcDictionary.Services;

namespace GrpcDictionary.TestClient
{
    internal class TestClient : IDictionary
    {
        private readonly Dictionary.DictionaryClient _client;

        public TestClient(Dictionary.DictionaryClient client)
        {
            _client = client;
        }

        public void Add(string key, string? value)
        {
            var item = new Item { Key = key, Value = value };

            _client.Add(new AddRequest { Item = item });
        }

        public void Remove(string key)
        {
            _client.Remove(new RemoveRequest { Key = key });
        }

        public (string Key, string? Value) Get(string key)
        {
            var response = _client.Get(new GetRequest { Key = key });

            return (response.Item.Key, response.Item.Value);
        }

        public IEnumerable<(string Key, string? Value)> GetAll()
        {
            var response = _client.GetAll(new GetAllRequest());

            return response.Items.Select(item => (item.Key, (string?)item.Value)).ToArray();
        }
    }
}
