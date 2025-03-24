using System.Data.Common;
using Dapper;
using Webjet.Backend.Services.Movies.GetMovieDetail;
using Webjet.Backend.Services.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Read
{
    public class MovieReadRepository(Func<DbConnection> sqlConnectionFactory)
        : BaseReadRepository(sqlConnectionFactory), IMovieReadRepository
    {
        public async Task<IEnumerable<MovieDto>> GetMovieSummaries()
        {
            const string sql = @"
                SELECT
                    MovieId as Id
                    ,Title
                    ,Poster
                    ,Price
                    ,BestPriceProvider   
                FROM
                    MovieSummaries
                ORDER BY 
                    Title";
            return await Connection.QueryAsync<MovieDto>(sql);
        }

        public async Task<MovieDetailVm> GetMovieDetails(string movieId)
        {
            const string sql = @"
                 SELECT
                   s.MovieId as Id
                  ,Title
                  ,Poster
                  ,Price
                  ,BestPriceProvider
	              ,Year
                  ,Rated
                  ,Released
                  ,Runtime
                  ,Genre
                  ,Director
                  ,Writer
                  ,Actors
                  ,Plot
                  ,Language
                  ,Country
                  ,Awards
                  ,Metascore
                  ,Rating
                  ,Votes
                  ,Type
                  ,d.UpdatedAt
             FROM
                 MovieSummaries s
            INNER JOIN MovieDetails d ON d.MovieId = s.MovieId
            WHERE s.MovieId=@movieId";

            return await Connection.QueryFirstOrDefaultAsync<MovieDetailVm>(sql, new { movieId });
        }
    }
}