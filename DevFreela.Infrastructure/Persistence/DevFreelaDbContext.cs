using DevFreela.Core.Entities;


namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            { 
                new Project("Meu projeto ASPNET Core1", "Minha Descricao de Projeto 1", 1, 1, 10000),
                new Project("Meu projeto ASPNET Core2", "Minha Descricao de Projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASPNET Core3", "Minha Descricao de Projeto 3", 1, 1, 30000)
                
            };

            Users = new List<User>
            {
                new User("Danilo Queiroz", "danilo123queiroz@gmail.com", new DateTime(2000,1,1)),
                new User("Davi Queiroz", "davi123queiroz@gmail.com", new DateTime(1994,11,24)),
                new User("Seila Queiroz", "Seila@gmail.com", new DateTime(1994,11,24))
            };


            Skills = new List<Skill>
            {
                new Skill(".NET Core"),
                new Skill("C#"),
                new Skill("SQL")
            };
        }

        public List<Project> Projects { get; private set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
