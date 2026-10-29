using TodoApp.Enums;
using TodoApp.Models;

namespace TodoApp.Services;

public class TaskManager
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public TaskItem Add(string title, string? description, Priority priority, DateTime? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề không được để trống.");

        var task = new TaskItem
        {
            Id = _nextId++,
            Title = title.Trim(),
            Description = description,
            Priority = priority,
            DueDate = dueDate
        };
        _tasks.Add(task);
        return task;
    }

    public bool Remove(int id)
    {
        var task = GetById(id);
        return task != null && _tasks.Remove(task);
    }

    public bool Update(int id, string title, string? description, Priority priority, DateTime? dueDate)
    {
        var task = GetById(id);
        if (task == null) return false;
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề không được để trống.");

        task.Title = title.Trim();
        task.Description = description;
        task.Priority = priority;
        task.DueDate = dueDate;
        return true;
    }

    public bool MarkCompleted(int id)
    {
        var task = GetById(id);
        if (task == null) return false;
        task.IsCompleted = true;
        return true;
    }

    public TaskItem? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public IReadOnlyList<TaskItem> GetAll() => _tasks.AsReadOnly();

    public IEnumerable<TaskItem> Filter(bool? isCompleted = null, Priority? priority = null)
    {
        var query = _tasks.AsEnumerable();
        if (isCompleted.HasValue) query = query.Where(t => t.IsCompleted == isCompleted.Value);
        if (priority.HasValue) query = query.Where(t => t.Priority == priority.Value);
        return query.OrderByDescending(t => t.Priority);
    }
}