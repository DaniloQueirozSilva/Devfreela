using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService 
    {
        private readonly DevFreelaDbContext _dbContext;
        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Create(NewProjectInputModel inputModel)
        {

            
            var project = new Project(inputModel.Title,inputModel.Description,
                inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);
            
            _dbContext.Projects.Add(project);

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment()
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Finish(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProjectViewModel> GellAll(string query)
        {
            throw new NotImplementedException();
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Start(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}
