using TaskManager.Api.Domain;
using TaskManager.Api.Contracts;
using TaskManager.Api.Validation;


namespace TaskManager.Api.Contracts;

public record CreateTaskRequest(
    string Title,
    string? Description,
    TaskPriority Priority,
    DateTime? DueDate
);

public record UpdateTaskRequest(
    string Title,
    string? Description,
    TaskPriority Priority,
    DateTime? DueDate,
    TaskState Status
);

public record TaskListResponse(
    int Total,
    int Page,
    int PageSize,
    IReadOnlyList<TaskItem> Items
);
