namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the user who owns a contact
    /// </summary>
    public class UserOwnerContact
    {
        /// <summary>
        /// The name of the searched user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's friend code on Nintendo 3DS
        /// </summary>
        public string Nintendo3dsFriendCode { get; set; }

        /// <summary>
        /// User's friend code on Nintendo Switch
        /// </summary>
        public string NintendoSwitchFriendCode { get; set; }

        /// <summary>
        /// The name of the user searched in PlayPokemonShowdown
        /// </summary>
        public string ShowdownNickname { get; set; }
    }
}