﻿using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {

            _mediator = mediator;
        }

        // api/users/login
        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand login)
        {
            var loginUserViewModel = await _mediator.Send(login);

            if (loginUserViewModel == null)
                return BadRequest("Usúario ou senha inválidos");

            return Ok(loginUserViewModel);
        }

        //api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var getUserByIdQuery = new GetUserByIdQuery(id);
            var user = _mediator.Send(getUserByIdQuery);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // api/users
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] CreateUserCommand inputModel)
        {

            var id = _mediator.Send(inputModel);

            return Created();
        }
    }
}
