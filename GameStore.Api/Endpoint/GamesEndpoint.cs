using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoint;

public static class GamestoreEndpoints
{
    const string GetGameEndPointName ="GetGame";
private static readonly List<GameDto> games =
[
    new (
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992,7,15) 
    ),

    new(
        2,
        "Final Fantasy XIV",
        "Roleplay", 
        59.99M, 
        new DateOnly(2010, 9,30)
        ),
    new (
        3,
        "FIFA 23", 
        "Sports", 
        69.99M,
        new DateOnly(2022,9,27) 
    )  
];
public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
{
    var group =app.MapGroup("games").WithParameterValidation();


// GET games

group.MapGet("/", () => games);

//GET /games/1

group.MapGet("/{id}", (int id) =>
{
    GameDto? game =games.Find(game =>  game.Id==id);
    return game is null ? Results.NotFound(): Results.Ok(game);
})
.WithName(GetGameEndPointName);

// POST /games

group.MapPost("/", (CreateGameDto newGame) =>
{
    GameDto game =new
    (
        games.Count +1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndPointName, new{id =game.Id}, game);// this the environment
} 
);
// PUT /games
group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
{
 var index =games.FindIndex(game => game.Id == id);
 if (index == -1)
 {
    return Results.NotFound();
 }
 games[index] =new GameDto(
    id,
    updateGame.Name,
    updateGame.Genre,
    updateGame.Price,
    updateGame.ReleaseDate
 );
 return Results.NoContent();
    
});

// DELETE /games/1
group.MapDelete("/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();

});
return group;
}
}