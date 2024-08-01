namespace Pomodoro.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Descpription { get; set; }
        public int Estimate { get; set; }
        public virtual ICollection<TaskDuration> TasksDuration { get; set; }
    }
}
