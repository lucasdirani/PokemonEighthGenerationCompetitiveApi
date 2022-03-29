using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using EighthGenerationCompetitive.Business.Errors;
using EighthGenerationCompetitive.Business.Errors.Factory;
using EighthGenerationCompetitive.Business.Interfaces;
using EighthGenerationCompetitive.Business.Monads;
using EighthGenerationCompetitive.Business.Repositories;
using System;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Business.Services
{
    public class PokemonTypeService : BaseService, IPokemonTypeService
    {
        private readonly IPokemonTypeRepository _pokemonTypeRepository;

        private bool _disposed = false;

        public PokemonTypeService(IPokemonTypeRepository pokemonTypeRepository, INotifier notifier) 
            : base(notifier)
        {
            _pokemonTypeRepository = pokemonTypeRepository;
        }

        public async Task<Maybe<Entities.TypeAggregate.Type>> SearchPokemonTypeByNameAsync(string typeName)
        {
            Maybe<Entities.TypeAggregate.Type> pokemonType = await _pokemonTypeRepository.FindPokemonTypeByNameAsync(typeName);

            if (pokemonType.HasNoValue)
            {
                Notify(appError: AppErrorFactory.Make(AppErrorType.PokemonTypeNotFound));
            }

            return pokemonType;
        }

        public async Task<Maybe<Entities.TypeAggregate.Type>> SearchPokemonTypeOnlyWithOurMovesAsync(string typeName)
        {
            Maybe<Entities.TypeAggregate.Type> pokemonType = await _pokemonTypeRepository.FindPokemonTypeByNameOnlyWithOurMovesAsync(typeName);

            if (pokemonType.HasNoValue)
            {
                Notify(appError: AppErrorFactory.Make(AppErrorType.PokemonTypeNotFound));
            }

            return pokemonType;
        }

        public async Task<Maybe<Entities.TypeAggregate.Type>> SearchPokemonTypeOnlyWithOurMonstersAsync(string typeName)
        {
            Maybe<Entities.TypeAggregate.Type> pokemonType = await _pokemonTypeRepository.FindPokemonTypeByNameOnlyWithOurMonstersAsync(typeName);

            if (pokemonType.HasNoValue)
            {
                Notify(appError: AppErrorFactory.Make(AppErrorType.PokemonTypeNotFound));
            }

            return pokemonType;
        }

        public async Task<Maybe<Entities.TypeAggregate.Type>> SearchPokemonTypeOnlyWithOurMonstersFormsAsync(string typeName)
        {
            Maybe<Entities.TypeAggregate.Type> pokemonType = await _pokemonTypeRepository.FindPokemonTypeByNameOnlyWithOurMonstersFormsAsync(typeName);

            if (pokemonType.HasNoValue)
            {
                Notify(appError: AppErrorFactory.Make(AppErrorType.PokemonTypeNotFound));
            }

            return pokemonType;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _pokemonTypeRepository?.Dispose();
                _disposed = true;
            }
        }
    }
}