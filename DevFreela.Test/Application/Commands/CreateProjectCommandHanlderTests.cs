using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Test.Application.Commands
{
   
    public class CreateProjectCommandHanlderTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            //Arrange
            var projectRepository = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Test",
                Description = "Test",
                TotalCost = 1000,
                IdClient = 1,
                IdFreelancer = 2,

            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepository.Object);

            // Act

            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());            

            //Asset

            Assert.True(id >= 0);
            projectRepository.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
