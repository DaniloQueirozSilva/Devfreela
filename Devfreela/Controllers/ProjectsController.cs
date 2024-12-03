using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers
{

    [Route("api/projects")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public ProjectsController(IProjectService projectService, IMediator mediator)
        {
            _projectService = projectService;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "client,freelancer")]
        public async Task<IActionResult> Get()
        {
            var queryAllProjectsQuery = new GetAllProjectsQuery("");

            var projects = await _mediator.Send(queryAllProjectsQuery);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "client,freelancer")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);

        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {                                 
            
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);            
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public IActionResult Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
                return BadRequest();

            _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(DeleteProjectCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // api/projects/1/comments POST
        [Authorize(Roles = "client, freelancer")]
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateProjectCommand command)
        {
             await _mediator.Send(command);            

            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);

            return NoContent();
        }

        // api/projects/1/finish
        [Authorize(Roles = "client")]
        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);

            return NoContent();
        }
    }
}
