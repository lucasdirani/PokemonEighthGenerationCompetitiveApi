using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.Attributes
{
    public class NintendoSwitchFriendCodeAttribute : ValidationAttribute
    {
        private const int NintendoSwitchFriendCodeLength = 14;
        private const string NintendoSwitchFriendCodePrefix = "SW";

        public override bool IsValid(object value)
        {
            if (value is not string) return false;

            string nintendoSwitchFriendCode = value as string;

            if (CheckIfDoesntHaveSwitchPrefix(nintendoSwitchFriendCode)) return false;

            string nintendoSwitchFriendCodeWithoutPrefix = GetNintendoSwitchFriendCodeWithoutPrefix(nintendoSwitchFriendCode);

            return nintendoSwitchFriendCodeWithoutPrefix.Length == NintendoSwitchFriendCodeLength;
        }

        private static string GetNintendoSwitchFriendCodeWithoutPrefix(string nintendoSwitchFriendCode)
        {
            int indexOfSwitchPrefix = nintendoSwitchFriendCode.IndexOf(NintendoSwitchFriendCodePrefix);

            indexOfSwitchPrefix += NintendoSwitchFriendCodePrefix.Length;

            return nintendoSwitchFriendCode[(indexOfSwitchPrefix + 1)..];
        }

        private static bool CheckIfDoesntHaveSwitchPrefix(string nintendoSwitchFriendCode)
            => !nintendoSwitchFriendCode.StartsWith(NintendoSwitchFriendCodePrefix);
    }
}