using AutoMapper;
using EighthGenerationCompetitive.Api.Attributes;
using EighthGenerationCompetitive.Api.BaseControllers;
using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Api.Parameters.NaturesController;
using EighthGenerationCompetitive.Api.V1.ViewModels.Natures;
using EighthGenerationCompetitive.Business.Errors;
using EighthGenerationCompetitive.Business.Errors.Factory;
using EighthGenerationCompetitive.Business.Interfaces;
using EighthGenerationCompetitive.Business.Repositories;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Accept(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/natures")]
    public class NaturesController : MainController
    {
        private readonly INatureRepository _natureRepository;
        private readonly INatureService _natureService;
        private readonly IMapper _mapper;

        public NaturesController(
            INotifier notifier, 
            IUser user, 
            ILinksService linksService,
            INatureRepository natureRepository,
            INatureService natureService,
            IMapper mapper,
            ILogger logger) 
            : base(notifier, user, linksService, logger)
        {
            _natureRepository = natureRepository;
            _natureService = natureService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all natures based on selection criteria
        /// </summary>
        /// <param name="naturesParameters">Parameters to filter the search for natures</param>
        /// <returns>Natures along with their name and relationships with stats</returns>
        /// <response code="200">Returns all natures based on selection criteria</response>
        /// <response code="400">Parameter values used to filter natures types are invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">No natures found for selected filter</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpGet(Name = "GetNatures")]
        public async Task<ActionResult<GetNaturesViewModel>> GetNaturesAsync(
            [FromQuery] GetNaturesParameters naturesParameters)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            var findNatureParameters = naturesParameters.MakeFindNatureParameters();

            var natures = await _natureRepository.FindNaturesAsync(findNatureParameters);

            if (natures.IsEmpty) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var naturesPaginated = _mapper.MapPagedList<GetNaturesViewModel>(natures);

            AddHateoasFor(naturesPaginated);

            SetXPaginationHeaderFor(naturesPaginated);

            return CustomResponseForGetEndpoint(naturesPaginated.AsApiResponse());
        }

        /// <summary>
        /// Gets a nature based on its name
        /// </summary>
        /// <param name="natureName">The name of the nature to be fetched</param>
        /// <returns>Nature along with all its properties</returns>
        /// <response code="200">Returns the nature searched</response>
        /// <response code="400">The nature searched is invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">The nature searched was not found</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpGet("{natureName}", Name = "GetNatureByName")]
        public async Task<ActionResult<GetNatureViewModel>> GetNatureByNameAsync(string natureName)
        {
            var natureSearching = await _natureService.SearchNatureByNameAsync(natureName);

            if (natureSearching.HasNoValue) return CustomResponseForGetEndpoint(resource: Resource.NotFound);
            
            var nature = _mapper.Map<GetNatureViewModel>(natureSearching.Value);

            await AddHateoasForAsync(nature);

            return CustomResponseForGetEndpoint(nature.AsApiResponse());
        }

        /// <summary>
        /// Gets all monsters associated with a Nature
        /// </summary>
        /// <param name="natureName">The name of the nature to be fetched</param>
        /// <returns></returns>
        /// <response code="200">A list of all pokemon with strategies that have the searched nature</response>
        /// <response code="400">The nature searched is invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">The nature searched was not found</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpGet("{natureName}/pokemon", Name = "GetNatureMonstersByName")]
        public async Task<ActionResult<GetNatureMonstersByNameViewModel>> GetNatureMonstersByNameAsync(string natureName)
        {
            var naturesSearching = await _natureService.SearchNatureOnlyWithOurMonstersAsync(natureName);

            if (naturesSearching.HasNoValue) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var natureMonsters = _mapper.Map<GetNatureMonstersByNameViewModel>(naturesSearching.Value);

            return CustomResponseForGetEndpoint(natureMonsters.AsApiResponse());
        }

        /// <summary>
        /// Gets all monsters forms associated with a Nature
        /// </summary>
        /// <param name="natureName">The name of the nature to be fetched</param>
        /// <returns></returns>
        /// <response code="200">A list of all pokemon forms with strategies that have the searched nature</response>
        /// <response code="400">The nature searched is invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">The nature searched was not found</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpGet("{natureName}/pokemon-forms", Name = "GetNatureMonstersFormsByName")]
        public async Task<ActionResult<GetNatureMonstersFormsByNameViewModel>> GetNatureMonstersFormsByNameAsync(string natureName)
        {
            var natureMonstersFormsSearching = await _natureService.SearchNatureOnlyWithOurMonstersFormsAsync(natureName);

            if (natureMonstersFormsSearching.HasNoValue) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var natureMonstersForms = _mapper.Map<GetNatureMonstersFormsByNameViewModel>(natureMonstersFormsSearching.Value);

            return CustomResponseForGetEndpoint(natureMonstersForms.AsApiResponse());
        }
    }
}