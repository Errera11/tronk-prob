using System.Text.Json.Serialization;

namespace api.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TodoStatusFilter
{
    allTasks,
    completedTasks,
    activeTasks
}

public class TodoTaskQueryFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? Search { get; set; }

    public TodoStatusFilter? Status { get; set; } = TodoStatusFilter.allTasks;
}