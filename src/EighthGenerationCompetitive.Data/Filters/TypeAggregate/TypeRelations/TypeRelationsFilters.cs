using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;

namespace EighthGenerationCompetitive.Data.Filters.TypeAggregate.TypeRelations
{
    internal class TypeRelationsFilters :
        IDoubleDamageFromFilter,
        IDoubleDamageToFilter,
        IHalfDamageFromFilter,
        IHalfDamageToFilter,
        INoDamageFromFilter,
        INoDamageToFilter,
        ITypeRelationsFilter
    {
        const string DoubleDamageFromField = "typeRelations.typeRelationsDoubleDamageFrom.typeName";
        const string DoubleDamageToField = "typeRelations.typeRelationsDoubleDamageTo.typeName";
        const string HalfDamageFromField = "typeRelations.typeRelationsHalfDamageFrom.typeName";
        const string HalfDamageToField = "typeRelations.typeRelationsHalfDamageTo.typeName";
        const string NoDamageFromField = "typeRelations.typeRelationsNoDamageFrom.typeName";
        const string NoDamageToField = "typeRelations.typeRelationsNoDamageTo.typeName";

        private IMongoQueryable<Type> _pokemonTypes;

        private TypeRelationsFilters(IMongoQueryable<Type> pokemonTypes)
        {
            _pokemonTypes = pokemonTypes;
        }

        public static IHalfDamageToFilter ApplyFiltersFrom(IMongoQueryable<Type> pokemonTypes)
        {
            return new TypeRelationsFilters(pokemonTypes);
        }

        public IMongoQueryable<Type> ApplyFilters()
        {
            return _pokemonTypes;
        }

        public ITypeRelationsFilter ApplyDoubleDamageFrom(string[] doubleDamageFrom)
        {
            if (doubleDamageFrom?.Length > 0)
            {
                var doubleDamageFromFilter = Builders<Type>.Filter.All(DoubleDamageFromField, doubleDamageFrom.Select(d => d));

                _pokemonTypes = _pokemonTypes.Where(p => doubleDamageFromFilter.Inject());
            }

            return this;
        }

        public INoDamageFromFilter ApplyDoubleDamageTo(string[] doubleDamageTo)
        {
            if (doubleDamageTo?.Length > 0)
            {
                var doubleDamageToFilter = Builders<Type>.Filter.All(DoubleDamageToField, doubleDamageTo.Select(d => d));

                _pokemonTypes = _pokemonTypes.Where(p => doubleDamageToFilter.Inject());
            }

            return this;
        }

        public IDoubleDamageFromFilter ApplyHalfDamageFrom(string[] halfDamageFrom)
        {
            if (halfDamageFrom?.Length > 0)
            {
                var halfDamageFromFilter = Builders<Type>.Filter.All(HalfDamageFromField, halfDamageFrom.Select(h => h));

                _pokemonTypes = _pokemonTypes.Where(p => halfDamageFromFilter.Inject());
            }

            return this;
        }

        public INoDamageToFilter ApplyHalfDamageTo(string[] halfDamageTo)
        {
            if (halfDamageTo?.Length > 0)
            {
                var halfDamageToFilter = Builders<Type>.Filter.All(HalfDamageToField, halfDamageTo.Select(h => h));

                _pokemonTypes = _pokemonTypes.Where(p => halfDamageToFilter.Inject());
            }

            return this;
        }

        public IHalfDamageFromFilter ApplyNoDamageFrom(string[] noDamageFrom)
        {
            if (noDamageFrom?.Length > 0)
            {
                var noDamageFromFilter = Builders<Type>.Filter.All(NoDamageFromField, noDamageFrom.Select(n => n));

                _pokemonTypes = _pokemonTypes.Where(p => noDamageFromFilter.Inject());
            }

            return this;
        }

        public IDoubleDamageToFilter ApplyNoDamageTo(string[] noDamageTo)
        {
            if (noDamageTo?.Length > 0)
            {
                var noDamageToFilter = Builders<Type>.Filter.All(NoDamageToField, noDamageTo.Select(n => n));

                _pokemonTypes = _pokemonTypes.Where(p => noDamageToFilter.Inject());
            }

            return this;
        }
    }
}