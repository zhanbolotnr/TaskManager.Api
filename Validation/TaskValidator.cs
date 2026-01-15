using TaskManager.Api.Contracts;
using TaskManager.Api.Controllers;

namespace TaskManager.Api.Validation;

public static class TaskValidator
{
    public static string? ValidateCreate(CreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return "Title is required.";

        var title = request.Title.Trim();

        if (title.Length < 3) return "Title must be at least 3 characters.";
        if (title.Length > 100) return "Title must be at most 100 characters.";

        if (request.Description is not null && request.Description.Length > 1000)
            return "Description must be at most 1000 characters.";

        return null;
    }

    public static string? ValidateUpdate(UpdateTaskRequest request)
    {
        var title = request.Title.Trim();

        if (title.Length < 3) return "Title must be at least 3 characters.";
        if (title.Length > 100) return "Title must be at most 100 characters.";

        if (request.Description is not null && request.Description.Length > 1000)
            return "Description must be at most 1000 characters.";

        return null;
    }

/*
    internal static object ValidateCreate(TasksController.CreateTaskRequest request)
    {
        throw new NotImplementedException();
    }
*/
}