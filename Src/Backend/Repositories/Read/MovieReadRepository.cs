using Dapper;
using System.Data.Common;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Read
{
    public class MovieReadRepository(Func<DbConnection> sqlConnectionFactory) : BaseReadRepository(sqlConnectionFactory), IMovieReadRepository
    {
        public async Task<IEnumerable<MovieDto>> GetMovieSummaries()
        {
            const string sql = @"
                SELECT
                    MovieId as Id,
                    Title,
                    Poster,
                    Price
                FROM
                    MovieSummaries";
            return await Connection.QueryAsync<MovieDto>(sql);
        }
    }

    public interface IMovieReadRepository
    {
        Task<IEnumerable<MovieDto>> GetMovieSummaries();
    }
}
