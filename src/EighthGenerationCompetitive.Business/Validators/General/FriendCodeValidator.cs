using EighthGenerationCompetitive.Business.Extensions;
using EighthGenerationCompetitive.Business.Utils;

namespace EighthGenerationCompetitive.Business.Validators.General
{
    public static class FriendCodeValidator
    {
        private static readonly int FriendCodeLength = 14;
        private static readonly int FriendCodeLengthWithoutDashes = 12;
        private static readonly long MaxPossibleFriendCode = 549755813887;

        public static bool Validate(string friendCode)
        {
            if (string.IsNullOrEmpty(friendCode)) return false;

            if (IsInvalidLength(friendCode)) return false;

            string friendCodeWithoutDashes = friendCode.Replace("-", string.Empty);

            if (IsInvalidLengthWithoutDashes(friendCodeWithoutDashes)) return false;

            if (IsNotANumber(friendCodeWithoutDashes)) return false;

            var u64FriendCode = long.Parse(friendCodeWithoutDashes);

            if (IsGreaterThanMaxPossibleFriendCode(u64FriendCode)) return false;

            return IsValidChecksum(u64FriendCode);
        }

        private static bool IsValidChecksum(long friendCode)
        {
            long principalId = friendCode.GetLower32Bits();

            long theirChecksum = friendCode.GetUpper32Bits();

            byte[] digestPrincipalId = Sha1Crypto.ComputeHash(principalId.ReadAsLittleEndian());

            long myChecksum = digestPrincipalId[0] >> 1;

            return theirChecksum == myChecksum;
        }

        private static bool IsGreaterThanMaxPossibleFriendCode(long friendCode)
            => friendCode > MaxPossibleFriendCode;

        private static bool IsNotANumber(string friendCodeWithoutDashes)
            => !friendCodeWithoutDashes.IsNumeric();

        private static bool IsInvalidLength(string friendCode)
            => friendCode.Length != FriendCodeLength;

        private static bool IsInvalidLengthWithoutDashes(string friendCodeWithoutDashes)
            => friendCodeWithoutDashes.Length != FriendCodeLengthWithoutDashes;
    }
}