namespace TaskManager.Api.Domain;

public enum TaskState
{
    New = 0,
    InProgress = 1,
    Done = 2
}

public enum TaskPriority
{
    Low = 0,
    Medium = 1,
    High = 2
}

public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime? DueDate { get; set; }

    public TaskState Status { get; set; } = TaskState.New;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


