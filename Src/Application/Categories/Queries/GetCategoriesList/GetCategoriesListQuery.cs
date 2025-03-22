using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webjet.Application.Common.Interfaces;

namespace Webjet.Application.Categories.Queries.GetCategoriesList;

public record GetCategoriesListQuery : IRequest<CategoriesListVm>;

// ReSharper disable once UnusedType.Global
public class GetCategoriesListQueryHandler(IWebjetDbContext context, IMapper mapper) : IRequestHandler<GetCategoriesListQuery, CategoriesListVm>
{
    public async Task<CategoriesListVm> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var categories = await context.Categories
            .ProjectTo<CategoryLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var vm = new CategoriesListVm
        {
            Categories = categories
        };

        return vm;
    }
}