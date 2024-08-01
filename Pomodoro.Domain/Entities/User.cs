namespace Pomodoro.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }

        public virtual ICollection<TaskDuration> TasksDuration { get; set; }
    }
}
