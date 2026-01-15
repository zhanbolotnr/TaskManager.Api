using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Contracts;
using TaskManager.Api.Domain;
using TaskManager.Api.Persistence;
using TaskManager.Api.Validation;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db) => _db = db;

    // READ: список
    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] TaskState? status,
    [FromQuery] TaskPriority? priority,
    [FromQuery] string? q,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _db.Tasks.AsQueryable();

        if (status is not null)
            query = query.Where(t => t.Status == status);

        if (priority is not null)
            query = query.Where(t => t.Priority == priority);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(t =>
                t.Title.Contains(term) ||
                (t.Description != null && t.Description.Contains(term)));
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new { total, page, pageSize, items });
    }


    // READ: по id
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskItem>> GetById(Guid id)
    {
        var task = await _db.Tasks.FindAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var error = TaskValidator.ValidateCreate(request);
        if (error != null)
            return BadRequest(error);

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            Description = request.Description,
            Priority = request.Priority,
            DueDate = request.DueDate,
            Status = TaskState.New,
            CreatedAt = DateTime.UtcNow
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    // UPDATE (полное обновление)
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
    Guid id,
    [FromBody] UpdateTaskRequest request)
    {
        var error = TaskValidator.ValidateUpdate(request);
        if (error != null)
            return BadRequest(error);

        var task = await _db.Tasks.FindAsync(id);
        if (task is null)
            return NotFound();

        task.Title = request.Title.Trim();
        task.Description = request.Description;
        task.Priority = request.Priority;
        task.DueDate = request.DueDate;
        task.Status = request.Status;

        await _db.SaveChangesAsync();
        return NoContent();
    }


    // DELETE
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return NoContent();
    }

}



