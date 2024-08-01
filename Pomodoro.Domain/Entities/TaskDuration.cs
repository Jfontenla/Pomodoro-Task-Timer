namespace Pomodoro.Domain.Entities
{
    public class TaskDuration
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int TaskId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Task Task { get; set; }

        public int Duration()
        {
            return EndTime.HasValue ? (int)(EndTime.Value - StartTime).TotalMinutes: (int)(DateTime.UtcNow - StartTime).TotalSeconds;
        }

    }
}
