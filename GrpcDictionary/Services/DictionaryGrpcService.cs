using Grpc.Core;
using GrpcDictionary.Protos;

namespace GrpcDictionary.Services
{
    public class DictionaryGrpcService : Dictionary.DictionaryBase
    {
        private readonly IDictionary _dictionary;
        private readonly ILogger<DictionaryGrpcService> _logger;

        public DictionaryGrpcService(ILogger<DictionaryGrpcService> logger, IDictionary dictionary)
        {
            _logger = logger;
            _dictionary = dictionary;
        }

        public override Task<AddResponse> Add(AddRequest request, ServerCallContext context)
        {
            _dictionary.Add(request.Item.Key, request.Item.Value);

            return Task.FromResult(new AddResponse());
        }

        public override Task<RemoveResponse> Remove(RemoveRequest request, ServerCallContext context)
        {
            _dictionary.Remove(request.Key);

            return Task.FromResult(new RemoveResponse());
        }

        public override Task<GetResponse> Get(GetRequest request, ServerCallContext context)
        {
            var response = new GetResponse();
            var item = _dictionary.Get(request.Key);

            response.Item = new Item
            {
                Key = item.Key,
                Value = item.Value,
            };

            return Task.FromResult(response);
        }

        public override Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
        {
            var response = new GetAllResponse();

            foreach (var (Key, Value) in _dictionary.GetAll())
            {
                response.Items.Add(new Item { Key = Key, Value = Value });
            }

            return Task.FromResult(response);
        }
    }
}
