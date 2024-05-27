
# GrpcDictionary

This is a simple project that provide the service that can CRUD a simple data in dictionary format (key-value pair).

## Requirements

Install SDK for .NET 8.0 if not already.

You can install this with Visual Studio or manually download it here https://dotnet.microsoft.com/en-us/download/dotnet/8.0.

## Build and start

To start the service you can open GrpcDictionary project directory by terminal and enter ```dotnet run``` command.

Additionally you can append ```--urls``` argument to setup special service host address.

**Example:**

Service will starts on default address (http://localhost:5075):
```
dotnet run
```

Service will starts on address http://localhost:1234:
```
dotnet run --urls http://localhost:1234
```

## Tests

To test service you may to use next options:
- Build and start **GrpcDictionary.TestClient** project that contains a simple client script _(it also allow to setup service address by ```urls``` argument)_
- Start basic unit tests from **GrpcDictionary.Tests** project directory
- Use postman to send request ([How to use postman](https://learning.postman.com/docs/sending-requests/grpc/grpc-request-interface))
