using Grpc.Net.Client;
using GrpcDictionary.Protos;

var address = "http://localhost:5075";
using var channel = GrpcChannel.ForAddress(address);
var client = new Dictionary.DictionaryClient(channel);

client.Add(new AddRequest { Item = new Item { Key = "", Value = "" } });
client.Remove(new RemoveRequest { Key = "" });

var getResponse = client.Get(new GetRequest { Key = "" });
var getAllResponse = client.GetAll(new GetAllRequest());

Console.WriteLine("Get response:");
Console.WriteLine(getResponse.Item);

Console.WriteLine("Get all response:");
getAllResponse.Items.ToList().ForEach(Console.WriteLine);
