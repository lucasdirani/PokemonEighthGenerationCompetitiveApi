using EighthGenerationCompetitive.Api.V1.ViewModels.Users;
using EighthGenerationCompetitive.Business.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EighthGenerationCompetitive.Api.Attributes
{
    public class ApplicationContactAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not IEnumerable<RegisterUserContactViewModel>) return false;

            var applicationContacts = value as IEnumerable<RegisterUserContactViewModel>;

            if (!applicationContacts.Any()) return false;

            return HasUniqueContactTypes(applicationContacts);
        }

        private static bool HasUniqueContactTypes(IEnumerable<RegisterUserContactViewModel> applicationContacts)
        {
            var applicationContactsTypes = applicationContacts.Select(c => c.Type);

            var distinctApplicationContacts = applicationContactsTypes.GetUniqueItems();

            return applicationContacts.Count() == distinctApplicationContacts.Count();
        }
    }
}