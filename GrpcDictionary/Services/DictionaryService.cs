using Grpc.Core;
using GrpcDictionary.Protos;

namespace GrpcDictionary.Services
{
    public class DictionaryService : Dictionary.DictionaryBase
    {
        private readonly ILogger<DictionaryService> _logger;
        public DictionaryService(ILogger<DictionaryService> logger)
        {
            _logger = logger;
        }

        public override Task<AddResponse> Add(AddRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AddResponse());
        }

        public override Task<RemoveResponse> Remove(RemoveRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RemoveResponse());
        }

        public override Task<GetResponse> Get(GetRequest request, ServerCallContext context)
        {
            var response = new GetResponse();

            response.Item = new Item
            {
                Key = request.Key,
                Value = "get_value",
            };

            return Task.FromResult(response);
        }

        public override Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
        {
            var response = new GetAllResponse();

            response.Items.Add(new Item
            {
                Key = "get_all_key_1",
                Value = "get_all_value_1",
            });

            response.Items.Add(new Item
            {
                Key = "get_all_key_2",
                Value = null,
            });

            response.Items.Add(new Item
            {
                Key = "get_all_key_3",
                Value = "get_all_value_3",
            });

            return Task.FromResult(response);
        }
    }
}
