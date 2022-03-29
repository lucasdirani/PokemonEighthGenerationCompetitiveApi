using EighthGenerationCompetitive.Business.Validators.General;
using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.Attributes
{
    public class Nintendo3DsFriendCodeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not string) return false;

            string nintendo3dsFriendCode = value as string;
            
            return FriendCodeValidator.Validate(nintendo3dsFriendCode);
        }
    }
}