using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webjet.Application.Common.Interfaces;
using Webjet.Application.Products.Queries.GetProductsList;

namespace Webjet.Application.Movies.GetMovieList;

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
                new()
                {
                    ProductId = 1,
                    ProductName = "The Shawshank Redemption",
                    UnitPrice = 9.99m,
                    SupplierId = 1,
                    SupplierCompanyName = "Warner Bros.",
                    CategoryId = 1,
                    CategoryName = "Drama",
                    Discontinued = false
                },
                new()
                {
                    ProductId = 2,
                    ProductName = "The Godfather",
                    UnitPrice = 9.99m,
                    SupplierId = 1,
                    SupplierCompanyName = "Paramount Pictures",
                    CategoryId = 1,
                    CategoryName = "Crime",
                    Discontinued = false
                },
                new()
                {
                    ProductId = 3,
                    ProductName = "The Dark Knight",
                    UnitPrice = 9.99m,
                    SupplierId = 1,
                    SupplierCompanyName = "Warner Bros.",
                    CategoryId = 1,
                    CategoryName = "Action",
                    Discontinued = false
                }
            }
        };

        return vm;
    }
}