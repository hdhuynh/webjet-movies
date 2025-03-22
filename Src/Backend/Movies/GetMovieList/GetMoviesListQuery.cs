using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webjet.Backend.Common.Interfaces;
using Webjet.Backend.Products.Queries.GetProductsList;

namespace Webjet.Backend.Movies.GetMovieList;

public record GetMoviesListQuery : IRequest<MoviesListVm>;

// ReSharper disable once UnusedType.Global
public class GetMoviesListQueryHandler(IWebjetDbContext context, IMapper mapper) : IRequestHandler<GetMoviesListQuery, MoviesListVm>
{
    public async Task<MoviesListVm> Handle(GetMoviesListQuery request, CancellationToken cancellationToken)
    {
        // var products = await context.Products
        //     .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
        //     .OrderBy(p => p.ProductName)
        //     .ToListAsync(cancellationToken);

        var vm = new MoviesListVm
        {
            Movies = new List<MovieDto>
            {
                //generate random movies, using realistic random ProductName, SupplierCompanyName, CategoryName
                new MovieDto { MovieId = "1", Title = "Star Wars: Episode IV - A New Hope", Poster = "https://m.media-amazon.com/images/M/MV5BOTIyMDY2NGQtOGJjNi00OTk4LWFhMDgtYmE3M2NiYzM0YTVmXkEyXkFqcGdeQXVyNTU1NTcwOTk@._V1_SX300.jpg" },
                new MovieDto { MovieId = "2", Title = "Star Wars: Episode V - The Empire Strikes Back", Poster = "https://m.media-amazon.com/images/M/MV5BMjE2MzQwMTgxN15BMl5BanBnXkFtZTcwMDQzNjk2OQ@@._V1_SX300.jpg" },
                new MovieDto { MovieId = "3", Title = "Star Wars: Episode VI - Return of the Jedi", Poster = "https://m.media-amazon.com/images/M/MV5BMTQ0MzI1NjYwOF5BMl5BanBnXkFtZTgwODU3NDU2MTE@._V1._CR93,97,1209,1861_SX89_AL_.jpg_V1_SX300.jpg" },
                new MovieDto { MovieId = "4", Title = "Star Wars: The Force Awakens", Poster = "https://m.media-amazon.com/images/M/MV5BOTAzODEzNDAzMl5BMl5BanBnXkFtZTgwMDU1MTgzNzE@._V1_SX300.jpg" },
                new MovieDto { MovieId = "5", Title = "The Shawshank Redemption 2", Poster = "https://image.tmdb.org/t/p/w500/9O7gLzmreU0nGkIB6K3BsJbzvNv.jpg" },
                new MovieDto { MovieId = "6", Title = "The Godfather 2", Poster = "https://image.tmdb.org/t/p/w500/3bhkrj58Vtu7enYsRolD1fZdja1.jpg" },
                new MovieDto { MovieId = "7", Title = "The Dark Knight 2", Poster = "https://image.tmdb.org/t/p/w500/qJ2tW6WMUDux911r6m7haRef0WH.jpg" },
                new MovieDto { MovieId = "8", Title = "The Godfather: Part III", Poster = "https://image.tmdb.org/t/p/w500/bVq65huQ8vHDd1a4Z37QtuyEvpA.jpg" }
            }
        };

        return vm;
    }
}