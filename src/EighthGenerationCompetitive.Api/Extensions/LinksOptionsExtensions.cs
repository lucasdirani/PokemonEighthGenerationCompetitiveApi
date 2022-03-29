using EighthGenerationCompetitive.Api.BaseControllers;
using EighthGenerationCompetitive.Api.Handlers;
using EighthGenerationCompetitive.Api.V1.ViewModels.Natures;
using EighthGenerationCompetitive.Api.V1.ViewModels.Types;
using EighthGenerationCompetitive.Api.V1.ViewModels.Users;
using RiskFirst.Hateoas;

namespace EighthGenerationCompetitive.Api.Extensions
{
    public static class LinksOptionsExtensions
    {
        public static LinksOptions AddPolicyForGetPokemonTypes(this LinksOptions config)
        {
            config.AddPolicy<GetPokemonTypesViewModel>(policy => {
                policy.RequireSelfLink()
                      .RequireRoutedLink("getPokemonType", "GetPokemonTypeByName", x => new { typeName = x.TypeName.ToLower() })
                      .RequireRoutedLink("getPokemonTypesMoves", "GetPokemonTypesMovesByName", x => new { typeName = x.TypeName.ToLower() })
                      .RequireRoutedLink("getPokemonTypesMonsters", "GetPokemonTypesMonstersByName", x => new { typeName = x.TypeName.ToLower() })
                      .RequireRoutedLink("getPokemonTypesMonstersForms", "GetPokemonTypesMonstersFormsByName", x => new { typeName = x.TypeName.ToLower() })
                      .Requires<VersionLinkRequirement<MainController>>();
            });

            return config;
        }

        public static LinksOptions AddPolicyForGetPokemonTypeByName(this LinksOptions config)
        {
            config.AddPolicy<GetPokemonTypeViewModel>(policy => {
                policy.RequireSelfLink()
                      .RequireRoutedLink("getPokemonTypesMoves", "GetPokemonTypesMovesByName", x => new { typeName = x.TypeName.ToLower() })
                      .RequireRoutedLink("getPokemonTypesMonsters", "GetPokemonTypesMonstersByName", x => new { typeName = x.TypeName.ToLower() })
                      .RequireRoutedLink("getPokemonTypesMonstersForms", "GetPokemonTypesMonstersFormsByName", x => new { typeName = x.TypeName.ToLower() })
                      .Requires<VersionLinkRequirement<MainController>>();
            });

            return config;
        }

        public static LinksOptions AddPolicyForGetNatureByName(this LinksOptions config)
        {
            config.AddPolicy<GetNatureViewModel>(policy => {
                policy.RequireSelfLink()
                      .RequireRoutedLink("getNaturesMonsters", "GetNatureMonstersByName", x => new { natureName = x.NatureName.ToLower() })
                      .RequireRoutedLink("getNaturesMonstersForms", "GetNatureMonstersFormsByName", x => new { natureName = x.NatureName.ToLower() })
                      .Requires<VersionLinkRequirement<MainController>>();
            });

            return config;
        }

        public static LinksOptions AddPolicyForGetNatures(this LinksOptions config)
        {
            config.AddPolicy<GetNaturesViewModel>(policy => {
                policy.RequireSelfLink()
                      .RequireRoutedLink("getNature", "GetNatureByName", x => new { natureName = x.NatureName.ToLower() })
                      .RequireRoutedLink("getNaturesMonsters", "GetNatureMonstersByName", x => new { natureName = x.NatureName.ToLower() })
                      .RequireRoutedLink("getNaturesMonstersForms", "GetNatureMonstersFormsByName", x => new { natureName = x.NatureName.ToLower() })
                      .Requires<VersionLinkRequirement<MainController>>();
            });

            return config;
        }

        public static LinksOptions AddPolicyForUserLogin(this LinksOptions config)
        {
            config.AddPolicy<UserLoginViewModel>(policy => {
                policy.RequireSelfLink()
                      .RequireRoutedLink("getUser", "GetUserByName", x => new { name = x.UserToken.UserName.ToLower() })
                      .RequireRoutedLink("getUserContacts", "GetUserContacts", x => new { name = x.UserToken.UserName.ToLower() })
                      .RequireRoutedLink("registerUserContact", "RegisterUserContact", x => new { name = x.UserToken.UserName.ToLower() })
                      .RequireRoutedLink("updateUser3dsFriendCode", "UpdateUser3dsFriendCode", x => new { name = x.UserToken.UserName.ToLower() })
                      .RequireRoutedLink("updateUserSwitchFriendCode", "UpdateUserSwitchFriendCode", x => new { name = x.UserToken.UserName.ToLower() })
                      .RequireRoutedLink("updateUserShowdownNickname", "UpdateUserShowdownNickname", x => new { name = x.UserToken.UserName.ToLower() })
                      .RequireRoutedLink("updateUserMainInfo", "UpdateUserMainInfo", x => new { name = x.UserToken.UserName.ToLower() })
                      .RequireRoutedLink("updateUserContacts", "UpdateUserContacts", x => new { name = x.UserToken.UserName.ToLower() })
                      .RequireRoutedLink("updateShowUserProfile", "ShowUserProfile", x => new { name = x.UserToken.UserName.ToLower() })
                      .RequireRoutedLink("updateHideUserProfile", "HideUserProfile", x => new { name = x.UserToken.UserName.ToLower() })
                      .Requires<VersionLinkRequirement<MainController>>();
            });

            return config;
        }

        public static LinksOptions AddPolicyForGetUser(this LinksOptions config)
        {
            config.AddPolicy<GetUserViewModel>(policy => {
                policy.RequireSelfLink()
                      .RequireRoutedLink("updateUser3dsFriendCode", "UpdateUser3dsFriendCode", x => new { name = x.UserName.ToLower() })
                      .RequireRoutedLink("updateUserSwitchFriendCode", "UpdateUserSwitchFriendCode", x => new { name = x.UserName.ToLower() })
                      .RequireRoutedLink("updateUserShowdownNickname", "UpdateUserShowdownNickname", x => new { name = x.UserName.ToLower() })
                      .RequireRoutedLink("getUserContacts", "GetUserContacts", x => new { name = x.UserName.ToLower() })
                      .RequireRoutedLink("registerUserContact", "RegisterUserContact", x => new { name = x.UserName.ToLower() })
                      .RequireRoutedLink("updateUserMainInfo", "UpdateUserMainInfo", x => new { name = x.UserName.ToLower() })
                      .RequireRoutedLink("updateUserContacts", "UpdateUserContacts", x => new { name = x.UserName.ToLower() })
                      .RequireRoutedLink("updateShowUserProfile", "ShowUserProfile", x => new { name = x.UserName.ToLower() })
                      .RequireRoutedLink("updateHideUserProfile", "HideUserProfile", x => new { name = x.UserName.ToLower() })
                      .RequireRoutedLink("login", "Login")
                      .Requires<VersionLinkRequirement<MainController>>();
            });

            return config;
        }

        public static LinksOptions AddPolicyForGetUserContact(this LinksOptions config)
        {
            config.AddPolicy<GetUserContactViewModel>(policy => {
                policy.RequireSelfLink()
                      .RequireRoutedLink("updateUserContact", "UpdateUserContact", x => new { name = x.ApplicationUser.UserName.ToLower(), id = x.Id })
                      .RequireRoutedLink("removeUserContact", "RemoveUserContact", x => new { name = x.ApplicationUser.UserName.ToLower(), id = x.Id })
                      .Requires<VersionLinkRequirement<MainController>>();
            });

            return config;
        }

        public static LinksOptions AddPolicyForGetUserContacts(this LinksOptions config)
        {
            config.AddPolicy<GetUserContactsViewModel>(policy => {
                policy.RequireSelfLink()
                      .RequireRoutedLink("getUserContactById", "GetUserContactById", x => new { name = x.ApplicationUser.UserName.ToLower(), id = x.Id })
                      .RequireRoutedLink("updateUserContact", "UpdateUserContact", x => new { name = x.ApplicationUser.UserName.ToLower(), id = x.Id })
                      .RequireRoutedLink("removeUserContact", "RemoveUserContact", x => new { name = x.ApplicationUser.UserName.ToLower(), id = x.Id })
                      .Requires<VersionLinkRequirement<MainController>>();
            });

            return config;
        }
    }
}