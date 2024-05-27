using Grpc.Core;
using GrpcDictionary.Protos;

namespace GrpcDictionary.Services
{
    public class DictionaryService : Dictionary.DictionaryBase
    {
        private readonly IDictionarySrorage _storage;
        private readonly ILogger<DictionaryService> _logger;

        public DictionaryService(ILogger<DictionaryService> logger, IDictionarySrorage storage)
        {
            _logger = logger;
            _storage = storage;
        }

        public override Task<AddResponse> Add(AddRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"DictionaryGrpcService.Add was called with item {request.Item}");

            _storage.Add(request.Item.Key, request.Item.Value);

            return Task.FromResult(new AddResponse());
        }

        public override Task<RemoveResponse> Remove(RemoveRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"DictionaryGrpcService.Remove was called with key \"{request.Key}\"");

            _storage.Remove(request.Key);

            return Task.FromResult(new RemoveResponse());
        }

        public override Task<GetResponse> Get(GetRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"DictionaryGrpcService.Get was called with key \"{request.Key}\"");

            var response = new GetResponse();
            var item = _storage.Get(request.Key);

            response.Item = new Item
            {
                Key = item.Key,
                Value = item.Value,
            };

            return Task.FromResult(response);
        }

        public override Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"DictionaryGrpcService.GetAll was called");

            var response = new GetAllResponse();

            foreach (var (Key, Value) in _storage.GetAll())
            {
                response.Items.Add(new Item { Key = Key, Value = Value });
            }

            return Task.FromResult(response);
        }

        public override Task<CountResponse> Count(CountRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"DictionaryGrpcService.Count called");

            var response = new CountResponse { Count = _storage.Count() };

            return Task.FromResult(response);
        }

        public override Task<ClearResponse> Clear(ClearRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"DictionaryGrpcService.Clear called");

            _storage.Clear();

            return Task.FromResult(new ClearResponse());
        }
    }
}
