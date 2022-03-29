using AutoMapper;
using EighthGenerationCompetitive.Api.Attributes;
using EighthGenerationCompetitive.Api.BaseControllers;
using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Api.Parameters.TypesController;
using EighthGenerationCompetitive.Api.V1.ViewModels.Types;
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
    [Route("api/v{version:apiVersion}/types")]
    public class TypesController : MainController
    {
        private readonly IPokemonTypeService _pokemonTypeService;
        private readonly IPokemonTypeRepository _pokemonTypeRepository;
        private readonly IMapper _mapper;

        public TypesController(
            IPokemonTypeService pokemonTypeService,
            IPokemonTypeRepository pokemonTypeRepository,
            IMapper mapper,
            ILinksService linksService,
            INotifier notifier,
            ILogger logger,
            IUser user)
            : base(notifier, user, linksService, logger)
        {
            _pokemonTypeService = pokemonTypeService;
            _pokemonTypeRepository = pokemonTypeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all pokémon types based on selection criteria
        /// </summary>
        /// <param name="pokemonTypesParameters">Parameters to filter the search for pokemon types</param>
        /// <returns>Pokémon types along with their name and relationships with other types</returns>
        /// <response code="200">Returns all pokémon types based on selection criteria</response>
        /// <response code="400">Parameter values used to filter pokemon types are invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">No pokemon types found for selected filter</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpGet(Name = "GetPokemonTypes")]
        public async Task<ActionResult<GetPokemonTypesViewModel>> GetPokemonTypesAsync(
            [FromQuery] GetPokemonTypesParameters pokemonTypesParameters)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            var findPokemonTypesParameters = pokemonTypesParameters.MakeFindPokemonTypeParameters();

            var pokemonTypes = await _pokemonTypeRepository.FindPokemonTypesAsync(findPokemonTypesParameters);

            if (pokemonTypes.IsEmpty) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var pokemonTypesPaginated = _mapper.MapPagedList<GetPokemonTypesViewModel>(pokemonTypes);

            AddHateoasFor(pokemonTypesPaginated);

            SetXPaginationHeaderFor(pokemonTypesPaginated);

            return CustomResponseForGetEndpoint(pokemonTypesPaginated.AsApiResponse());
        }

        /// <summary>
        /// Gets a pokemon type based on its name
        /// </summary>
        /// <param name="typeName">The name of the pokemon type to be fetched</param>
        /// <returns>Pokemon type along with all its properties</returns>
        /// <response code="200">Returns the pokemon type searched</response>
        /// <response code="400">The pokemon type searched is invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">The pokemon type searched was not found</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpGet("{typeName}", Name = "GetPokemonTypeByName")]
        public async Task<ActionResult<GetPokemonTypeViewModel>> GetPokemonTypeByNameAsync(string typeName)
        {
            var pokemonTypeSearching = await _pokemonTypeService.SearchPokemonTypeByNameAsync(typeName);

            if (pokemonTypeSearching.HasNoValue) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var pokemonType = _mapper.Map<GetPokemonTypeViewModel>(pokemonTypeSearching.Value);

            await AddHateoasForAsync(pokemonType);

            return CustomResponseForGetEndpoint(pokemonType.AsApiResponse());
        }

        /// <summary>
        /// Gets all moves associated with a Pokémon type
        /// </summary>
        /// <param name="typeName">The name of the Pokémon type to be fetched</param>
        /// <returns></returns>
        /// <response code="200">A list of all moves of the searched type</response>
        /// <response code="400">The pokemon type searched is invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">The pokemon type searched was not found</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpGet("{typeName}/moves", Name = "GetPokemonTypesMovesByName")]
        public async Task<ActionResult<GetPokemonTypeMovesByNameViewModel>> GetPokemonTypesMovesByNameAsync(string typeName)
        {
            var pokemonTypeMovesSearching = await _pokemonTypeService.SearchPokemonTypeOnlyWithOurMovesAsync(typeName);

            if (pokemonTypeMovesSearching.HasNoValue) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var pokemonTypeMoves = _mapper.Map<GetPokemonTypeMovesByNameViewModel>(pokemonTypeMovesSearching.Value);

            return CustomResponseForGetEndpoint(pokemonTypeMoves.AsApiResponse());
        }

        /// <summary>
        /// Gets all monsters associated with a Pokémon type
        /// </summary>
        /// <param name="typeName">The name of the Pokémon type to be fetched</param>
        /// <returns></returns>
        /// <response code="200">A list of all pokemon of the searched type</response>
        /// <response code="400">The pokemon type searched is invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">The pokemon type searched was not found</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpGet("{typeName}/pokemon", Name = "GetPokemonTypesMonstersByName")]
        public async Task<ActionResult<GetPokemonTypeMonstersByNameViewModel>> GetPokemonTypesMonstersByNameAsync(string typeName)
        {
            var pokemonTypesMonstersSearching = await _pokemonTypeService.SearchPokemonTypeOnlyWithOurMonstersAsync(typeName);

            if (pokemonTypesMonstersSearching.HasNoValue) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var pokemonTypeMonsters = _mapper.Map<GetPokemonTypeMonstersByNameViewModel>(pokemonTypesMonstersSearching.Value);

            return CustomResponseForGetEndpoint(pokemonTypeMonsters.AsApiResponse());
        }

        /// <summary>
        /// Gets all monsters forms associated with a Pokémon type
        /// </summary>
        /// <param name="typeName">The name of the Pokémon type to be fetched</param>
        /// <returns></returns>
        /// <response code="200">A list of all pokemon forms of the searched type</response>
        /// <response code="400">The pokemon type searched is invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">The pokemon type searched was not found</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpGet("{typeName}/pokemon-forms", Name = "GetPokemonTypesMonstersFormsByName")]
        public async Task<ActionResult<GetPokemonTypeMonstersFormsByNameViewModel>> GetPokemonTypesMonstersFormsByNameAsync(string typeName)
        {
            var pokemonTypesMonstersFormsSearching = await _pokemonTypeService.SearchPokemonTypeOnlyWithOurMonstersFormsAsync(typeName);

            if (pokemonTypesMonstersFormsSearching.HasNoValue) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var pokemonTypeMonstersForms = _mapper.Map<GetPokemonTypeMonstersFormsByNameViewModel>(pokemonTypesMonstersFormsSearching.Value);

            return CustomResponseForGetEndpoint(pokemonTypeMonstersForms.AsApiResponse());
        }
    }
}