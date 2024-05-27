using Microsoft.Extensions.Logging;
using GrpcDictionary.Services;
using GrpcDictionary.Protos;
using Moq;
using Xunit;
using Grpc.Core;

namespace GrpcDictionary.Tests
{
    public class DictionaryServiceTests
    {
        [Fact]
        public async Task CountTest()
        {
            var logger = getLoggerMock();
            var storage = getStorageMock();
            var context = getContextMock();
            var service = new DictionaryService(logger, storage);

            var count = await service.Count(new CountRequest { }, context);

            Assert.IsType<CountResponse>(count);
            Assert.Equal(3, count.Count);
        }

        [Fact]
        public async Task GetTest()
        {
            var logger = getLoggerMock();
            var storage = getStorageMock();
            var context = getContextMock();
            var service = new DictionaryService(logger, storage);

            var get1 = await service.Get(new GetRequest { Key = "key1" }, context);
            var get2 = await service.Get(new GetRequest { Key = "key2" }, context);
            var get3 = await service.Get(new GetRequest { Key = "key3" }, context);

            Assert.IsType<GetResponse>(get1);
            Assert.IsType<GetResponse>(get2);
            Assert.IsType<GetResponse>(get3);

            Assert.Equal("key1", get1.Item.Key);
            Assert.Equal("key2", get2.Item.Key);
            Assert.Equal("key3", get3.Item.Key);

            Assert.Equal("value1", get1.Item.Value);
            Assert.Null(get2.Item.Value);
            Assert.Equal("value3", get3.Item.Value);
        }

        [Fact]
        public async Task GetAllTest()
        {
            var logger = getLoggerMock();
            var storage = getStorageMock();
            var context = getContextMock();
            var service = new DictionaryService(logger, storage);

            var getAll = await service.GetAll(new GetAllRequest(), context);

            Assert.IsType<GetAllResponse>(getAll);
            Assert.Equal(3, getAll.Items.Count);

            foreach (var item in getAll.Items)
            {
                switch (item.Key)
                {
                    case "key1":
                        Assert.Equal("value1", item.Value);
                        break;
                    case "key2":
                        Assert.Null(item.Value);
                        break;
                    case "key3":
                        Assert.Equal("value3", item.Value);
                        break;
                    default:
                        Assert.Fail();
                        break;
                }
            }
        }

        private ILogger<DictionaryService> getLoggerMock()
        {
            var mock = new Mock<ILogger<DictionaryService>>();

            return mock.Object;
        }

        private IDictionarySrorage getStorageMock()
        {
            var mock = new Mock<IDictionarySrorage>();

            mock.Setup(storage => storage.Count()).Returns(3);

            mock.Setup(storage => storage.Get("key1")).Returns(("key1", "value1"));
            mock.Setup(storage => storage.Get("key2")).Returns(("key2", null));
            mock.Setup(storage => storage.Get("key3")).Returns(("key3", "value3"));

            mock.Setup(storage => storage.GetAll()).Returns([
                ("key1", "value1"),
                ("key2", null),
                ("key3", "value3"),
            ]);

            return mock.Object;
        }

        private ServerCallContext getContextMock()
        {
            var mock = new Mock<ServerCallContext>();

            return mock.Object;
        }
    }
}
