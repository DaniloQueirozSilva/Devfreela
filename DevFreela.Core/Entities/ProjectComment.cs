namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string content, int idUser, int idProject)
        {
            Content = content;
            IdUser = idUser;
            IdProject = idProject;
        }

        public string Content { get; private set; }
        public int IdUser { get; private set; }
        public User User { get; set; }
        public int IdProject { get; private set; }
        public UserSkill Project { get; set; }
        public DateTime CreatedAt { get; private set; }
    }
}
