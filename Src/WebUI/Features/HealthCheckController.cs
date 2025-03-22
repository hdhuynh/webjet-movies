// using System.Net.Mime;
// using AutoMapper;
// using Backend.Features.Account;
// using Portal.Code;
// using Portal.Code.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using PI.CQRS.Contracts;
//
// namespace Portal.Features.HealthCheck;
//
// public class HealthCheckController : ApiControllerBase
// {
//     public HealthCheckController(IBus bus, IWebHostEnvironment env, IMapper mapper) : base(bus, env, mapper)
//     {
//     }
//
//     /// <summary>
//     /// Default health check endpoint for keeping the API running (eg AppService always on feature)
//     /// </summary>
//     /// <returns></returns>
//     [HttpGet]
//     [Route(Routes.V1.HealthCheck)]
//     [Consumes(MediaTypeNames.Application.Json)]
//     [Produces(MediaTypeNames.Application.Json)]
//     [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
//     public IActionResult Get()
//     {
//         return new OkObjectResult($"Welcome @{DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
//     }
//
//     /// <summary>
//     /// Health check endpoint called be mobile users to ensure they have access and cloud backend is up and running 
//     /// </summary>
//     /// <returns></returns>
//     [HttpGet]
//     [Route(Routes.V1.HealthCheckForUser)]
//     [Consumes(MediaTypeNames.Application.Json)]
//     [Produces(MediaTypeNames.Application.Json)]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ApiAuthorize(UserRole.Admin, UserRole.Patient, UserRole.HCP)]
//     public IActionResult GetForAppUser()
//     {
//         return Ok();
//     }
// }
