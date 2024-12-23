﻿using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Auths.Roles.Queries.GetRoles;
using Ecommerce.Application.Features.Auths.Users.Commands.LoginUser;
using Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPasswordByToken;
using Ecommerce.Application.Features.Auths.Users.Commands.SendPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserById;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserByToken;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserByUserNameQuery;
using Ecommerce.Application.Features.Auths.Users.Queries.PaginationUsers;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Application.Models.ImageManagement;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;
        private IManageImageService _manageImageService;

        public UserController(IMediator mediator, IManageImageService manageImageService)
        {
            _mediator = mediator;
            _manageImageService = manageImageService;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpPost("register", Name = "Register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Register([FromForm] RegisterUserCommand request)
        {
            if (request.Photo is not null)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                {
                    ImageStream = request.Photo!.OpenReadStream(),
                    Name = request.Photo.Name
                });

                request.PhotoId = resultImage.PublicId;
                request.PhotoUrl = resultImage.Url;
            }
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpPost("forgotpassword", Name = "ForgotPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] SendPasswordCommand request)
        {
            return await _mediator.Send(request);
        }
        [AllowAnonymous]
        [HttpPost("resetpassword", Name = "ResetPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordByTokenCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("updatepassword", Name = "UpdatePassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> UpdatePassword([FromBody] ResetPasswordCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("update", Name = "Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Update([FromForm] UpdateUserCommand request)
        {
            if (request.Photo is not null)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                {
                    ImageStream = request.Photo!.OpenReadStream(),
                    Name = request.Photo.Name
                });

                request.PhotoId = resultImage.PublicId;
                request.PhotoUrl = resultImage.Url;
            }

            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("updateadminuser", Name = "UpdateAdminUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> UpdateAdminUser([FromBody] UpdateAdminUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("updateadminstatususer", Name = "UpdateAdminStatusUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> UpdateAdminStatusUser([FromBody] UpdateAdminStatusUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> GetUserById(string id)
        {
            var query = new GetUserByIdQuery(id);
            return await _mediator.Send(query);
        }

        [HttpGet("", Name = "CurrentUser")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> CurrentUser()
        {
            var query = new GetUserByTokenQuery();
            return await _mediator.Send(query);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("username/{username}", Name = "GetUserByUserName")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> GetUserByUserName(string username)
        {
            var query = new GetUserByUserNameQuery(username);
            return await _mediator.Send(query);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("paginationAdmin", Name = "PaginationUser")]
        [ProducesResponseType(typeof(PaginationVm<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<User>>> PaginationUser([FromQuery] PaginationUsersQuery paginationUsersQuery)
        {
            var paginationUser = await _mediator.Send(paginationUsersQuery);
            return Ok(paginationUser);
        }

        [AllowAnonymous]
        [HttpGet("roles", Name = "GerRoleList")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<string>>> GetRoleList()
        {
            var query = new GetRolesQuery();
            return await _mediator.Send(query);
        }

    }
}
