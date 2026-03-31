using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Common;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
    
   public static IQueryable<TodoTask> ApplySearch(this IQueryable<TodoTask> query, string? search)
    { 
        if (string.IsNullOrWhiteSpace(search))
            return query;

        return query.Where(t => t.Title.Contains(search));
    }
   
    public static IQueryable<TodoTask> ApplyStatusFilter(this IQueryable<TodoTask> query, TodoStatusFilter? status)
    {
        if (status == TodoStatusFilter.allTasks)
        {
            return query;
        }

        if (status == TodoStatusFilter.completedTasks)
        {
            return query.Where(t => t.IsCompleted == true);
        }

        if (status == TodoStatusFilter.activeTasks)
        {
            return query.Where(t => t.IsCompleted == false);
        }

        return query;
    }
}