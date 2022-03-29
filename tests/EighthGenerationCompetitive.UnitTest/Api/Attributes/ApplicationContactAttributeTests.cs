using EighthGenerationCompetitive.Api.Attributes;
using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.V1.ViewModels.Users;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EighthGenerationCompetitive.UnitTest.Api.Attributes
{
    public class ApplicationContactAttributeTests
    {
        [Theory]
        [MemberData(nameof(UniqueApplicationContacts))]
        public static void IsValid_ListWithUniqueApplicationContacts_ReturnsTrue(
            IEnumerable<RegisterUserContactViewModel> uniqueApplicationContacts)
        {
            var applicationContactAttribute = new ApplicationContactAttribute();

            bool isValid = applicationContactAttribute.IsValid(uniqueApplicationContacts);

            Assert.True(isValid);
        }

        [Theory]
        [MemberData(nameof(RepeatedApplicationContacts))]
        public static void IsValid_ListWithRepeatedApplicationContacts_ReturnsFalse(
            IEnumerable<RegisterUserContactViewModel> repeatedApplicationContacts)
        {
            var applicationContactAttribute = new ApplicationContactAttribute();

            bool isValid = applicationContactAttribute.IsValid(repeatedApplicationContacts);

            Assert.False(isValid);
        }

        [Fact]
        public static void IsValid_EmptyListOfApplicationContacts_ReturnsFalse()
        {
            var applicationContactAttribute = new ApplicationContactAttribute();

            var emptyApplicationContacts = Enumerable.Empty<RegisterUserContactViewModel>();

            bool isValid = applicationContactAttribute.IsValid(emptyApplicationContacts);

            Assert.False(isValid);
        }

        public static IEnumerable<object[]> UniqueApplicationContacts =>
            new List<object[]>
            {
                new object[] 
                { 
                    new List<RegisterUserContactViewModel>() 
                    {
                        new RegisterUserContactViewModel() { Description = "Facebook", Type = ApplicationContactType.Facebook },
                        new RegisterUserContactViewModel() { Description = "Github", Type = ApplicationContactType.Github },
                        new RegisterUserContactViewModel() { Description = "Instagram", Type = ApplicationContactType.Instagram }
                    }          
                },
                new object[]
                {
                    new List<RegisterUserContactViewModel>() 
                    {
                        new RegisterUserContactViewModel() { Description = "Facebook", Type = ApplicationContactType.Facebook },
                    }
                }
            };

        public static IEnumerable<object[]> RepeatedApplicationContacts =>
            new List<object[]>
            {
                new object[]
                {
                    new List<RegisterUserContactViewModel>()
                    {
                        new RegisterUserContactViewModel() { Description = "Facebook", Type = ApplicationContactType.Facebook },
                        new RegisterUserContactViewModel() { Description = "Facebook", Type = ApplicationContactType.Facebook },
                        new RegisterUserContactViewModel() { Description = "Github", Type = ApplicationContactType.Github },
                    }
                },
                new object[]
                {
                    new List<RegisterUserContactViewModel>()
                    {
                        new RegisterUserContactViewModel() { Description = "Facebook", Type = ApplicationContactType.Facebook },
                        new RegisterUserContactViewModel() { Description = "Twitter", Type = ApplicationContactType.Twitter },
                        new RegisterUserContactViewModel() { Description = "Website", Type = ApplicationContactType.Website },
                        new RegisterUserContactViewModel() { Description = "Twitter", Type = ApplicationContactType.Twitter },
                    }
                }
            };
    }
}