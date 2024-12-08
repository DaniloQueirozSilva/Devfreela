using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.Test.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels() 
        {
            //Arrange
            var projects = new List<Project>
            {
                new Project("Nome do Teste 1", "Descricao do teste 1",1,2,10000),
                new Project("Nome do Teste 2", "Descricao do teste 2",1,2,20000),
                new Project("Nome do Teste 3", "Descricao do teste 3",1,2,30000)
            };

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

            var getAllProjects = new GetAllProjectsQuery("");
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepository.Object);

            //Act
            var proectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjects, new CancellationToken());

            //Assert

            Assert.NotNull(proectViewModelList);
            Assert.NotEmpty(proectViewModelList);
            Assert.Equal(3, proectViewModelList.Count);


            projectRepository.Verify(pr => pr.GetAllAsync().Result, Times.Once);
        }
    }
}
