using AutoMapper;
using EighthGenerationCompetitive.Api.Attributes;
using EighthGenerationCompetitive.Api.Authentication;
using EighthGenerationCompetitive.Api.BaseControllers;
using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.Services.Interfaces;
using EighthGenerationCompetitive.Api.V1.ViewModels.Users;
using EighthGenerationCompetitive.Business.Errors;
using EighthGenerationCompetitive.Business.Errors.Factory;
using EighthGenerationCompetitive.Business.Interfaces;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Accept(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : MainController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtGenerator _jwtGenerator;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public UsersController(
            INotifier notifier,
            IUser user,
            ILinksService linksService,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            JwtGenerator jwtGenerator,
            IApplicationUserService applicationUserService,
            IMapper mapper,
            ILogger logger)
            : base(notifier, user, linksService, logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registers a user and returns an authentication token for some API endpoints
        /// </summary>
        /// <param name="registerUser">Data required to register a user</param>
        /// <returns>Authentication token with other registration data</returns>
        /// <response code="201">User has been registered successfully</response>
        /// <response code="400">User registration data entered is invalid</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [AllowAnonymous]
        [HttpPost("new-account", Name = "RegisterUser")]
        public async Task<ActionResult<UserLoginViewModel>> RegisterUserAsync(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            var user = _mapper.Map<ApplicationUser>(registerUser);

            var userCreation = await _userManager.CreateAsync(user, registerUser.Password);

            if (userCreation.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                var userLogin = await _jwtGenerator.GenerateJwtAsync(user.UserName);

                await AddHateoasForAsync(userLogin);

                Logger.Log(LogLevel.Information, $"User {user.UserName} registered at {DateTime.UtcNow}");

                return CustomResponseForPostEndpoint(userLogin.AsApiResponse(), user.UserName, nameof(GetUserByNameAsync));
            }

            return CustomResponseFor(userCreation.Errors, actionName: nameof(RegisterUserAsync));
        }

        /// <summary>
        /// Authenticates a user to consume private API endpoints
        /// </summary>
        /// <param name="login">Data required for a user to authenticate to the API</param>
        /// <returns>Authentication token with other login data</returns>
        /// <response code="200">User has successfully authenticated</response>
        /// <response code="400">Login data is invalid or user is locked out for too many attempts</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [HttpPost("login", Name = "Login")]
        public async Task<ActionResult<UserLoginViewModel>> LoginAsync(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent: true, lockoutOnFailure: true);

            if (loginResult.Succeeded)
            {
                Logger.Log(LogLevel.Information, $"User {login.UserName} logged at {DateTime.UtcNow}");

                var userLogin = await _jwtGenerator.GenerateJwtAsync(login.UserName);

                await AddHateoasForAsync(userLogin);

                return Ok(userLogin);
            }

            if (loginResult.IsLockedOut)
            {
                Logger.Log(LogLevel.Warning, $"User {login.UserName} locked out at {DateTime.UtcNow}: {Request.HttpContext.Connection.RemoteIpAddress}");

                NotifyError(AppErrorFactory.Make(AppErrorType.UserLockedOut));

                return CustomResponseForPostEndpoint(login);
            }

            NotifyError(AppErrorFactory.Make(AppErrorType.UserCredentialsInvalid));

            return CustomResponseForPostEndpoint(login);
        }

        /// <summary>
        /// Retrieves a user's registration data based on their name
        /// </summary>
        /// <param name="name">The name of the searched user</param>
        /// <returns>The username along with your contacts, friend codes and nickname on PlayPokemonShowdown</returns>
        /// <response code="200">User data returned successfully</response>
        /// <response code="400">Username is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">The searched user does not exist</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpGet("{name}", Name = "GetUserByName")]
        [ActionName(nameof(GetUserByNameAsync))]
        public async Task<ActionResult<GetUserViewModel>> GetUserByNameAsync(string name)
        {
            var userSearchResult = await _applicationUserService.GetApplicationUserByNameAsync(name);

            if (userSearchResult.Failure) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var user = _mapper.Map<GetUserViewModel>(userSearchResult.Value);

            await AddHateoasForAsync(user);

            return CustomResponseForGetEndpoint(user.AsApiResponse());
        }

        /// <summary>
        /// Retrieves the user contact registration data based on their id
        /// </summary>
        /// <param name="name">The name of the searched user</param>
        /// <param name="id">The unique identification of the searched contact</param>
        /// <returns>The user contact registration data</returns>
        /// <response code="200">User contact data returned successfully</response>
        /// <response code="400">Username or contact id is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">User contacts not found</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpGet("{name}/contacts/{id}", Name = "GetUserContactById")]
        [ActionName(nameof(GetUserContactByIdAsync))]
        public async Task<ActionResult<GetUserContactViewModel>> GetUserContactByIdAsync(string name, Guid id)
        {
            var contactSearchResult = await _applicationUserService.GetUserApplicationContactAsync(name, id);

            if (contactSearchResult.Failure) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            GetUserContactViewModel userContact = _mapper.Map<GetUserContactViewModel>(contactSearchResult.Value);

            await AddHateoasForAsync(userContact);

            return CustomResponseForGetEndpoint(userContact.AsApiResponse());
        }

        /// <summary>
        /// Retrieves the user's contacts registration data based on their name
        /// </summary>
        /// <param name="name">The name of the searched user</param>
        /// <returns>The user contacts registration data</returns>
        /// <response code="200">User contacts data returned successfully</response>
        /// <response code="400">Username is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="404">User contacts not found</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpGet("{name}/contacts", Name = "GetUserContacts")]
        public async Task<ActionResult<IEnumerable<GetUserContactsViewModel>>> GetUserContactsAsync(string name)
        {
            var contactsSearchResult = await _applicationUserService.GetUserApplicationContactsAsync(name);

            if (contactsSearchResult.Failure) return CustomResponseForGetEndpoint(resource: Resource.NotFound);

            var userContacts = _mapper.Map<IEnumerable<GetUserContactsViewModel>>(contactsSearchResult.Value);

            await AddHateoasForAsync(userContacts);

            return CustomResponseForGetEndpoint(userContacts.AsApiResponse());
        }

        /// <summary>
        /// Updates user's Nintendo 3DS friend code number
        /// </summary>
        /// <param name="name">The username that will have your friend code updated</param>
        /// <param name="updateUser3dsFriendCode">The user's new friend code</param>
        /// <response code="204">The friend code has been successfully updated</response>
        /// <response code="400">The friend code is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpPatch("{name}/update-3ds-friend-code", Name = "UpdateUser3dsFriendCode")]
        public async Task<ActionResult> UpdateUser3dsFriendCodeAsync(
            string name,
            [FromBody] UpdateUser3DsFriendCodeViewModel updateUser3dsFriendCode)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            string nintendo3dsFriendCode = updateUser3dsFriendCode.Nintendo3dsFriendCode;

            var updateResult = await _applicationUserService.UpdateUser3dsFriendCodeAsync(ApplicationUser, nintendo3dsFriendCode);

            return CustomResponseForPatchEndpoint(updateResult);
        }

        /// <summary>
        /// Updates user's Nintendo Switch friend code number
        /// </summary>
        /// <param name="name">The username that will have your friend code updated</param>
        /// <param name="updateUserSwitchFriendCode">The user's new friend code</param>
        /// <response code="204">The friend code has been successfully updated</response>
        /// <response code="400">The friend code is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpPatch("{name}/update-switch-friend-code", Name = "UpdateUserSwitchFriendCode")]
        public async Task<ActionResult> UpdateUserSwitchFriendCodeAsync(
            string name,
            [FromBody] UpdateUserSwitchFriendCodeViewModel updateUserSwitchFriendCode)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            string nintendoSwitchFriendCode = updateUserSwitchFriendCode.NintendoSwitchFriendCode;

            var updateResult = await _applicationUserService.UpdateUserSwitchFriendCodeAsync(ApplicationUser, nintendoSwitchFriendCode);

            return CustomResponseForPatchEndpoint(updateResult);
        }

        /// <summary>
        /// Update a contact type belonging to the user
        /// </summary>
        /// <param name="name">The username that will have your contact updated</param>
        /// <param name="id">The unique identification of the contact that will be updated</param>
        /// <param name="updateUserContact">The new user contact type description</param>
        /// <response code="204">The contact has been successfully updated</response>
        /// <response code="400">The contact is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpPatch("{name}/contacts/{id}/update-contact", Name = "UpdateUserContact")]
        public async Task<ActionResult> UpdateUserContactAsync(
            string name,
            Guid id,
            [FromBody] UpdateUserContactViewModel updateUserContact)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            string contactDescription = updateUserContact.Description;

            ApplicationContactType contactType = updateUserContact.Type;

            var updateResult = await _applicationUserService.UpdateUserContactAsync(ApplicationUser, id, contactDescription, contactType);

            return CustomResponseForPatchEndpoint(updateResult);
        }

        /// <summary>
        /// Register a contact type belonging to the user
        /// </summary>
        /// <param name="name">The username that will have your contact registered</param>
        /// <param name="registerUserContact">The new user contact type</param>
        /// <response code="201">The contact has been successfully registered</response>
        /// <response code="400">The contact is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpPost("{name}/contacts/register-contact", Name = "RegisterUserContact")]
        public async Task<ActionResult> RegisterUserContactAsync(
            string name,
            [FromBody] RegisterUserContactViewModel registerUserContact)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            var registerResult = await _applicationUserService.RegisterUserContactAsync(ApplicationUser, registerUserContact);

            if (registerResult.Failure)
            {
                return CustomResponseForPostEndpoint(registerUserContact);
            }

            GetUserContactViewModel userContact = _mapper.Map<GetUserContactViewModel>(registerResult.Value);

            await AddHateoasForAsync(userContact);

            return CreatedAtAction(nameof(GetUserContactByIdAsync), new { name, id = userContact.Id }, userContact.AsApiResponse());
        }

        /// <summary>
        /// Remove a contact type belonging to the user
        /// </summary>
        /// <param name="name">The username that will have your contact removed</param>
        /// <param name="id">The unique identification of the contact that will be removed</param>
        /// <param name="removeUserContact">The contact to be removed</param>
        /// <response code="200">The contact has been successfully removed</response>
        /// <response code="400">The contact is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpDelete("{name}/contacts/{id}/remove-contact", Name = "RemoveUserContact")]
        public async Task<ActionResult> RemoveUserContactAsync(
            string name,
            Guid id,
            [FromBody] RemoveUserContactViewModel removeUserContact)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            ApplicationContactType contactType = removeUserContact.Type;

            var removeResult = await _applicationUserService.RemoveUserContactAsync(ApplicationUser, id, contactType);

            return CustomResponseForDeleteEndpoint(removeResult);
        }

        /// <summary>
        /// Updates one or more contacts of the user
        /// </summary>
        /// <param name="name">The username that will have your contacts updated</param>
        /// <param name="updateUserContacts">The new user contacts types descriptions</param>
        /// <response code="204">The contacts have been successfully updated</response>
        /// <response code="400">The contacts are in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpPut("{name}/contacts/update-contacts", Name = "UpdateUserContacts")]
        public async Task<ActionResult> UpdateUserContactsAsync(
            string name,
            [FromBody] List<UpdateUserContactViewModel> updateUserContacts)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            var updateResult = await _applicationUserService.UpdateUserContactsAsync(ApplicationUser, updateUserContacts);

            return CustomResponseForPutEndpoint(updateResult);
        }

        /// <summary>
        /// Updates user's showdown nickname
        /// </summary>
        /// <param name="name">The username that will have your showdown nickname updated</param>
        /// <param name="updateUserShowdownNickname">The user's new showdown nickname</param>
        /// <response code="204">The showdown nickname has been successfully updated</response>
        /// <response code="400">The showdown nickname is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpPatch("{name}/update-showdown-nickname", Name = "UpdateUserShowdownNickname")]
        public async Task<ActionResult> UpdateUserShowdownNickname(
            string name,
            [FromBody] UpdateUserShowdownNicknameViewModel updateUserShowdownNickname)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            string showdownNickname = updateUserShowdownNickname.ShowdownNickname;

            var updateResult = await _applicationUserService.UpdateUserShowdownNicknameAsync(ApplicationUser, showdownNickname);

            return CustomResponseForPatchEndpoint(updateResult);
        }

        /// <summary>
        /// Updates user's main registration info
        /// </summary>
        /// <param name="name">The username that will have your main info updated</param>
        /// <param name="updateUserMainInfo">The user's new main info</param>
        /// <response code="204">The main registration info has been successfully updated</response>
        /// <response code="400">The main info data is in an invalid format</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpPut("{name}/update-main-info", Name = "UpdateUserMainInfo")]
        public async Task<ActionResult> UpdateUserMainInfo(
            string name,
            [FromBody] UpdateUserMainInfoViewModel updateUserMainInfo)
        {
            if (!ModelState.IsValid)
            {
                var invalidParameters = AppErrorFactory.Make(AppErrorType.InvalidParameters);

                return CustomResponseFor(ModelState, invalidParameters);
            }

            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            var updateResult = await _applicationUserService.UpdateUserMainInfoAsync(ApplicationUser, updateUserMainInfo);

            return CustomResponseForPutEndpoint(updateResult);
        }

        /// <summary>
        /// Enables user data to be shared in the API
        /// </summary>
        /// <param name="name">The username that will have your profile shared</param>
        /// <response code="204">The profile has been successfully shared</response>
        /// <response code="400">The request has invalid data</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpPatch("{name}/show-profile", Name = "ShowUserProfile")]
        public async Task<ActionResult> UpdateShowUserProfileAsync(string name)
        {
            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            var updateResult = await _applicationUserService.ShowUserProfileAsync(ApplicationUser);

            return CustomResponseForPatchEndpoint(updateResult);
        }

        /// <summary>
        /// Disable user data to be shared in the API
        /// </summary>
        /// <param name="name">The username that will have your profile hided</param>
        /// <response code="204">The profile has been successfully hided</response>
        /// <response code="400">The request has invalid data</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">The user does not have permission to make the request</response>
        /// <response code="406">Endpoint does not return the type of representation requested by the client</response>
        /// <response code="500">Error processing user request on server</response>
        [Authorize]
        [HttpPatch("{name}/hide-profile", Name = "HideUserProfile")]
        public async Task<ActionResult> UpdateHideUserProfileAsync(string name)
        {
            if (ApplicationUser.IsNotAuthenticatedUser(name))
            {
                return Forbid();
            }

            var updateResult = await _applicationUserService.HideUserProfileAsync(ApplicationUser);

            return CustomResponseForPatchEndpoint(updateResult);
        }
    }
}