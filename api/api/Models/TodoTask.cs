using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class TodoTaskContext: DbContext
{
    public DbSet<TodoTask> TodoTasks { get; set; }

    public string DbPath { get; }

    public TodoTaskContext(DbContextOptions<TodoTaskContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        DbPath = System.IO.Path.Join(AppContext.BaseDirectory, "todo.db");
        Console.WriteLine($"Using {DbPath}");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

[Table("todo_task")]
public class TodoTask
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime DueDate { get; set; }

    public bool? IsCompleted { get; set; } = false;
    
    public int UserId { get; set; }
    
    public User CreatedBy { get; set; } = null!;
}