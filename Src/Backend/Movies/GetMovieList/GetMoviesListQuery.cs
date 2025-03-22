using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using Webjet.Backend.Common.Exceptions;
using Webjet.Backend.Common.Interfaces;
using Webjet.Backend.Products.Queries.GetProductsList;
using Webjet.Backend.Services;

namespace Webjet.Backend.Movies.GetMovieList;

public record GetMoviesListQuery : IRequest<MoviesListVm>;

// ReSharper disable once UnusedType.Global
public class GetMoviesListQueryHandler(IWebjetDbContext context, IMapper mapper, IExternalApiService externalApiService) : IRequestHandler<GetMoviesListQuery, MoviesListVm>
{
    public async Task<MoviesListVm> Handle(GetMoviesListQuery request, CancellationToken cancellationToken)
    {
        // var products = await context.Products
        //     .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
        //     .OrderBy(p => p.ProductName)
        //     .ToListAsync(cancellationToken);
        var apiResponse = await externalApiService.GetAsync("/movies","");
        if (apiResponse.IsSuccessStatusCode)
        {
            var result = await apiResponse.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<MoviesListVm>(result) ?? throw new InvalidDataException("Invalid data from External API");
            //
            // if (string.IsNullOrWhiteSpace(movie.MovieId) || string.IsNullOrWhiteSpace(movie.Title) || string.IsNullOrWhiteSpace(movie.Poster))
            // {
            //     //Dentist and Dental clinic are required to link an appliance
            //     throw new InvalidDataException("Missing required field(s)");
            // }
            //
            // var currentTime = DateTimeOffset.UtcNow;
            // var dentistClinics = await _hcpWriteRepository.UpdateOrCreateClinics(new[] { movie.Dentist?.Clinic }, currentTime);
            // await _hcpWriteRepository.UpdateHcp(movie.Dentist, dentistClinics, currentTime);
            //
            // //Only sleep clinic is required to link an appliance. Sleep physician is optional
            // if (string.IsNullOrWhiteSpace(movie.SleepPhysician?.Clinic?.IntegrationClinicId))
            // {
            //     movie.SleepPhysician = null; //unknown sleep physician clinic -> discard sleep physician details
            // }
            // else
            // {
            //     var sleepClinics = await _hcpWriteRepository.UpdateOrCreateClinics(new[] { movie.SleepPhysician?.Clinic }, currentTime);
            //     await _hcpWriteRepository.UpdateHcp(movie.SleepPhysician, sleepClinics, currentTime);
            // }
            //
            // appliance = new Appliance
            // {
            //     ApplianceId = Guid.NewGuid(),
            //     SerialNumber = movie!.SerialNumber,
            //     ExternalDentistId = movie!.Dentist!.IntegrationHcpId,
            //     ExternalDentistClinicId = movie!.Dentist!.Clinic.IntegrationClinicId,
            //     ExternalSleepPhysicianId = movie!.SleepPhysician?.IntegrationHcpId,
            //     ExternalSleepPhysicianClinicId = movie!.SleepPhysician?.Clinic?.IntegrationClinicId,
            //     Type = (byte)movie.Type,
            //     LastUpdated = currentTime
            // };
            //
            // await _applianceWriteRepository.AddApplianceAsync(appliance);
            return movies;
        }
        else
        {
            throw new ();
        }
        // var vm = new MoviesListVm
        // {
        //     Movies = new List<MovieDto>
        //     {
        //         //generate random movies, using realistic random ProductName, SupplierCompanyName, CategoryName
        //         new MovieDto { MovieId = "1", Title = "Star Wars: Episode IV - A New Hope", Poster = "https://m.media-amazon.com/images/M/MV5BOTIyMDY2NGQtOGJjNi00OTk4LWFhMDgtYmE3M2NiYzM0YTVmXkEyXkFqcGdeQXVyNTU1NTcwOTk@._V1_SX300.jpg" },
        //         new MovieDto { MovieId = "2", Title = "Star Wars: Episode V - The Empire Strikes Back", Poster = "https://m.media-amazon.com/images/M/MV5BMjE2MzQwMTgxN15BMl5BanBnXkFtZTcwMDQzNjk2OQ@@._V1_SX300.jpg" },
        //         new MovieDto { MovieId = "3", Title = "Star Wars: Episode VI - Return of the Jedi", Poster = "https://m.media-amazon.com/images/M/MV5BMTQ0MzI1NjYwOF5BMl5BanBnXkFtZTgwODU3NDU2MTE@._V1._CR93,97,1209,1861_SX89_AL_.jpg_V1_SX300.jpg" },
        //         new MovieDto { MovieId = "4", Title = "Star Wars: The Force Awakens", Poster = "https://m.media-amazon.com/images/M/MV5BOTAzODEzNDAzMl5BMl5BanBnXkFtZTgwMDU1MTgzNzE@._V1_SX300.jpg" },
        //         new MovieDto { MovieId = "5", Title = "The Shawshank Redemption 2", Poster = "https://image.tmdb.org/t/p/w500/9O7gLzmreU0nGkIB6K3BsJbzvNv.jpg" },
        //         new MovieDto { MovieId = "6", Title = "The Godfather 2", Poster = "https://image.tmdb.org/t/p/w500/3bhkrj58Vtu7enYsRolD1fZdja1.jpg" },
        //         new MovieDto { MovieId = "7", Title = "The Dark Knight 2", Poster = "https://image.tmdb.org/t/p/w500/qJ2tW6WMUDux911r6m7haRef0WH.jpg" },
        //         new MovieDto { MovieId = "8", Title = "The Godfather: Part III", Poster = "https://image.tmdb.org/t/p/w500/bVq65huQ8vHDd1a4Z37QtuyEvpA.jpg" }
        //     }
        // };

        
    }
}