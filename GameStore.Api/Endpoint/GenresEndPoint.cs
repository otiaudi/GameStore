using GameStore.Api.Data;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoint;

public static class GenreEndpoint
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group =app.MapGroup("genres");
        group.MapGet("/", async(GameStoreContext dbContext) =>
        await  dbContext.Genres
                        .Select(genre => genre.ToDto())
                        .AsNoTracking()
                        .ToListAsync());
        return group;
    }

}