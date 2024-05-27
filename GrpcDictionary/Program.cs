using GrpcDictionary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton<IDictionary, DictionaryStorageService>();

var app = builder.Build();

app.MapGrpcService<DictionaryGrpcService>();

app.Run();
