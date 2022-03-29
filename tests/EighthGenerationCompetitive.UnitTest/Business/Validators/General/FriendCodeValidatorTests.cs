using EighthGenerationCompetitive.Business.Validators.General;
using Xunit;

namespace EighthGenerationCompetitive.UnitTest.Business.Validators.General
{
    public class FriendCodeValidatorTests
    {
        [Theory]
        [InlineData("3884-2421-7162")]
        [InlineData("2853-3667-8491")]
        [InlineData("0259-0850-4995")]
        [InlineData("5112-4368-0792")]
        [InlineData("1436-0603-9720")]
        public static void Validate_ValidFriendCode_ReturnsTrue(string friendCode)
        {
            bool friendCodeValidation = FriendCodeValidator.Validate(friendCode);

            Assert.True(friendCodeValidation);
        }

        [Theory]
        [InlineData("")]
        [InlineData("2853-3667-849")]
        [InlineData("125908504995")]
        [InlineData("9999-0603-9720")]
        public static void Validate_InvalidFriendCode_ReturnsFalse(string friendCode)
        {
            bool friendCodeValidation = FriendCodeValidator.Validate(friendCode);

            Assert.False(friendCodeValidation);
        }
    }
}