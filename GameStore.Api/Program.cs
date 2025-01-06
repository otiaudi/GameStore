using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoint;

var builder = WebApplication.CreateBuilder(args);

var connString =builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString); 
var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();
await app.MigrateDbAsync();

app.Run();


