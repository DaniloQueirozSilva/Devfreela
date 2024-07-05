using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers
{
    
    public class ProjectsController : ControllerBase
    {
        public ProjectsController(IProjectService projectService)
        {
            
        }
    }
}
