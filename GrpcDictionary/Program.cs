using GrpcDictionary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton<IDictionarySrorage, DictionaryStorageService>();

var app = builder.Build();

app.MapGrpcService<DictionaryService>();

app.Run();
