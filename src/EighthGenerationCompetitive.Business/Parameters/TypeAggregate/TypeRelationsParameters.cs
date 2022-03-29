using EighthGenerationCompetitive.Business.Extensions;
using System.Linq;

namespace EighthGenerationCompetitive.Business.Parameters.TypeAggregate
{
    public class TypeRelationsParameters
    {
        const char RelationsSeparator = ',';

        public string[] NoDamageTo { get; private set; }
        public string[] HalfDamageTo { get; private set; }
        public string[] DoubleDamageTo { get; private set; }
        public string[] NoDamageFrom { get; private set; }
        public string[] HalfDamageFrom { get; private set; }
        public string[] DoubleDamageFrom { get; private set; }

        public TypeRelationsParameters(
            string noDamageTo,
            string halfDamageTo,
            string doubleDamageTo,
            string noDamageFrom,
            string halfDamageFrom,
            string doubleDamageFrom)
        {
            NoDamageTo = SplitParameter(noDamageTo);
            HalfDamageTo = SplitParameter(halfDamageTo);
            DoubleDamageTo = SplitParameter(doubleDamageTo);
            NoDamageFrom = SplitParameter(noDamageFrom);
            HalfDamageFrom = SplitParameter(halfDamageFrom);
            DoubleDamageFrom = SplitParameter(doubleDamageFrom);
        }

        private static string[] SplitParameter(string parameter) =>
            parameter?
                .Split(RelationsSeparator)
                .Select(p => p.FirstCharToUpper().AllLettersAfterFirstCharToLower())
                .ToArray();
    }
}