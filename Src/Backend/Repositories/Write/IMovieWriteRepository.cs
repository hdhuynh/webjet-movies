using Microsoft.EntityFrameworkCore;
using Webjet.Backend.Models.Data;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Write;

public interface IMovieWriteRepository
{
    Task AddOrUpdateMovieSummary(MovieDto movieDto, MovieDetailsDto movieDetailsDto);
}