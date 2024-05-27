using Grpc.Net.Client;
using GrpcDictionary.Protos;
using GrpcDictionary.TestClient;

var address = "http://localhost:5075";
for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--urls" && (i + 1) < args.Length)
    {
        address = args[i + 1];
        break;
    }
}

using var channel = GrpcChannel.ForAddress(address);
var client = new Dictionary.DictionaryClient(channel);
var testClient = new TestClient(client);

testClient.Add("key_1", "value_1");
testClient.Add("key_2", null);
testClient.Add("key_3", "value_3");

testClient.Remove("key_3");

var item = testClient.Get("key_1");
Console.WriteLine("Get:");
Console.WriteLine($"{item.Key}: {item.Value ?? "null"}");

var items = testClient.GetAll();
Console.WriteLine("Get All:");
foreach (var currentItem in items)
{
    Console.WriteLine($"{currentItem.Key}: {currentItem.Value ?? "null"}");
}

var count = testClient.Count();
Console.WriteLine($"Count:\n{count}");

testClient.Clear();
Console.WriteLine("Clear");

var count2 = testClient.Count();
Console.WriteLine($"Count:\n{count2}");
