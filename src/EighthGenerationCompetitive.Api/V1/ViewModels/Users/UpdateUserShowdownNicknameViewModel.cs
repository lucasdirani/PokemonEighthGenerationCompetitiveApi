using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the data required for a user to update their showdown nickname
    /// </summary>
    public class UpdateUserShowdownNicknameViewModel
    {
        /// <summary>
        /// The username in PlayPokemonShowdown.
        /// </summary>
        [StringLength(40, ErrorMessage = "The field {0} must be between {2} and {1} characters.", MinimumLength = 1)]
        public string ShowdownNickname { get; set; }
    }
}