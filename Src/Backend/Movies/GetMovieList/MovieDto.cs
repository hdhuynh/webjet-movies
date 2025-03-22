namespace Webjet.Backend.Movies.GetMovieList;

public class MovieDto
{
    public required string MovieId { get; init; }

    public required string Title { get; init; }

    public required string Poster { get; init; }

    // public void Mapping(Profile profile)
    // {
    //     profile.CreateMap<Product, ProductDto>()
    //         .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Id.Value))
    //         .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.SupplierId.GetValueOrDefault().Value))
    //         .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId.GetValueOrDefault().Value))
    //         .ForMember(d => d.SupplierCompanyName, opt => opt.MapFrom(s => s.Supplier != null ? s.Supplier.CompanyName : string.Empty))
    //         .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.CategoryName : string.Empty));
    // }
}