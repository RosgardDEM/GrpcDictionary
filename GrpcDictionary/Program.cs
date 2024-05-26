using GrpcDictionary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<DictionaryService>();
app.MapGet("/", () => "");

app.Run();
