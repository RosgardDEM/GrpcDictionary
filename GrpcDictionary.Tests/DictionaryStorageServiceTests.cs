using Grpc.Core;
using GrpcDictionary.Services;
using Xunit;

namespace GrpcDictionary.Tests
{
    public class DictionaryStorageServiceTests
    {
        [Fact]
        public void AddTest()
        {
            var storage = new DictionaryStorageService();

            storage.Add("key1", "value1");
            storage.Add("key2", null);
            storage.Add("key3", "value3");

            Assert.Equal(3, storage.Count());
            Assert.Equal("value1", storage.Get("key1").Value);
            Assert.Null(storage.Get("key2").Value);
            Assert.Equal("value3", storage.Get("key3").Value);
        }

        [Fact]
        public void AddExceptionTest()
        {
            var storage = new DictionaryStorageService();

            storage.Add("key1", "value1");

            try
            {
                storage.Add("key1", "value1");
                Assert.Fail();
            }
            catch (RpcException exeption)
            {
                Assert.Equal(StatusCode.AlreadyExists, exeption.StatusCode);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Fact]
        public void RemoveTest()
        {
            var storage = new DictionaryStorageService();

            storage.Add("key1", "value1");
            storage.Add("key2", null);
            storage.Remove("key1");
            storage.Remove("key2");

            Assert.Equal(0, storage.Count());
            try
            {
                storage.Get("key1");
                Assert.Fail();
            } catch(RpcException exeption) {
                Assert.Equal(StatusCode.NotFound, exeption.StatusCode);
            } catch {
                Assert.Fail();
            }
            try
            {
                storage.Get("key2");
                Assert.Fail();
            }
            catch (RpcException exeption)
            {
                Assert.Equal(StatusCode.NotFound, exeption.StatusCode);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Fact]
        public void RemoveExceptionTest()
        {
            var storage = new DictionaryStorageService();

            try
            {
                storage.Get("key1");
                Assert.Fail();
            }
            catch (RpcException exeption)
            {
                Assert.Equal(StatusCode.NotFound, exeption.StatusCode);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Fact]
        public void GetAllTest()
        {
            var storage = new DictionaryStorageService();

            storage.Add("key1", "value1");
            storage.Add("key2", null);
            storage.Add("key3", "value3");
            storage.Add("key4", null);
            storage.Add("key5", "value5");

            storage.Remove("key4");
            storage.Remove("key1");

            var getAll = storage.GetAll();

            Assert.Equal(3, getAll.Count());

            foreach (var item in getAll)
            {
                switch (item.Key) {
                    case "key2":
                        Assert.Null(item.Value);
                        break;
                    case "key3":
                        Assert.Equal("value3", item.Value);
                        break;
                    case "key5":
                        Assert.Equal("value5", item.Value);
                        break;
                    default:
                        Assert.Fail();
                        break;
                }
            }
        }
    }
}
