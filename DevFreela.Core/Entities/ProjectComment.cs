namespace DevFreela.Core.Entities
{
    public class ProjectComment : BaseEntity
    {
        public ProjectComment(string content, int idUser, int idProject)
        {
            Content = content;
            IdUser = idUser;
            IdProject = idProject;
            CreatedAt = DateTime.Now;
        }

        public string Content { get; private set; }
        public int IdUser { get; private set; }
        public User User { get; set; }
        public int IdProject { get; private set; }
        public Project Project { get; set; }
        public DateTime CreatedAt { get; private set; }
    }
}
