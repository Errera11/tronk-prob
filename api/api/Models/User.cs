using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class UserContext: DbContext
{
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "todo.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

[Table("user")]
public class User
{
    public int Id { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}