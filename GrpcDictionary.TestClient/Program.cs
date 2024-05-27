using Grpc.Net.Client;
using GrpcDictionary.Protos;
using GrpcDictionary.TestClient;

var address = "http://localhost:5075";
using var channel = GrpcChannel.ForAddress(address);
var client = new Dictionary.DictionaryClient(channel);
var testClient = new TestClient(client);

testClient.Add("key_1", "value_1");
testClient.Add("key_2", null);
testClient.Add("key_3", "value_3");

testClient.Remove("key_3");

var item = testClient.Get("key_1");
Console.WriteLine($"Get:");
Console.WriteLine($"{item.Key}: {item.Value ?? "null"}");

var items = testClient.GetAll();
Console.WriteLine($"Get All:");
foreach (var currentItem in items)
{
    Console.WriteLine($"{currentItem.Key}: {currentItem.Value ?? "null"}");
}
