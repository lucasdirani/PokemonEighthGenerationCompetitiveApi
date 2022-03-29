using EighthGenerationCompetitive.Api.Attributes;
using Xunit;

namespace EighthGenerationCompetitive.UnitTest.Api.Attributes
{
    public class NintendoSwitchFriendCodeAttributeTests
    {
        [Theory]
        [InlineData("SW-7744-0564-4116")]
        [InlineData("SW-4381-0461-8726")]
        [InlineData("SW-7508-7150-5713")]
        [InlineData("SW-6976-6183-2305")]
        [InlineData("SW-0367-2550-1235")]
        public static void IsValid_ValidSwitchFriendCodeNumber_ReturnsTrue(string nintendoSwitchFriendCode)
        {
            var nintendoSwitchFriendCodeAttribute = new NintendoSwitchFriendCodeAttribute();

            bool friendCodeValidation = nintendoSwitchFriendCodeAttribute.IsValid(nintendoSwitchFriendCode);

            Assert.True(friendCodeValidation);
        }

        [Theory]
        [InlineData("7744-0564-4116")]
        [InlineData("4381-0461-87")]
        [InlineData("SW750871505713")]
        [InlineData("697661832305")]
        [InlineData("")]
        public static void IsValid_InvalidSwitchFriendCodeNumber_ReturnsFalse(string nintendoSwitchFriendCode)
        {
            var nintendoSwitchFriendCodeAttribute = new NintendoSwitchFriendCodeAttribute();

            bool friendCodeValidation = nintendoSwitchFriendCodeAttribute.IsValid(nintendoSwitchFriendCode);

            Assert.False(friendCodeValidation);
        }
    }
}