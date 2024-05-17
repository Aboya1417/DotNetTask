namespace DotNetTask.Models;

public class CreateProgram
{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
}