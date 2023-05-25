namespace TaskTracker.Tasks
{
    public record class Task
    {
        public string Title { get; init; }
        public string Summary { get; init; }
        public DateTime? Deadline { get; }
        public TaskType Type { get; init; }
        public TaskState Status { get; set; } = TaskState.New;
        public TaskPriority? Priority { get; init; } = TaskPriority.Normal;

        public Task(string title, string description, DateTime? deadline, TaskType type, TaskPriority? priority)
        {
            Title = title;
            Summary = description;
            Deadline = deadline;
            this.Type = type;
            if (priority != null)
                this.Priority = priority;
        }
        public override string ToString()
        {
            DateTime dtForCheck = new DateTime();
            if (Deadline is not null && Deadline != dtForCheck)
            {
                string format = "\n{0}\n[{1}] [{2}]\nPriority: {3}, Due till {4}\n {5}\n";
                return String.Format(format, Title, Type, Status, Priority, Deadline, Summary);
            }
            else
            {
                string format = "{0}\n[{1}] [{2}]\nPriority: {3}\n {4}";
                return String.Format(format, Title, Type, Status, Priority, Summary);
            }
        }
    }
}
