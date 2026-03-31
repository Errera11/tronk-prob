using System.Text.Json;
using api.Common;
using api.Dto;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class TodoTaskService: ITodoTaskService
{
    private readonly TodoTaskContext _todoTaskContext;

    public TodoTaskService(TodoTaskContext context)
    {
        _todoTaskContext = context;
    }
    
    public async Task<TodoTask> GetTaskById(int todoTaskId)
    {
        return await _todoTaskContext.TodoTasks
            .Include(t => t.CreatedBy)
            .FirstOrDefaultAsync(t => t.Id == todoTaskId);
    }
    
    public async Task<PagedResponse<TodoTask>> GetTaskList(TodoTaskQueryFilter filter, CancellationToken cancellationToken = default)
    {
        var pageNumber = Math.Max(1, filter.PageNumber);
        var pageSize = Math.Clamp(filter.PageSize, 1, 50);

        var query = _todoTaskContext.TodoTasks.AsNoTracking().AsQueryable();

        query = query.ApplySearch(filter.Search);
        
       query = query.ApplyStatusFilter(filter.Status);

        var totalRecords = await query.CountAsync(cancellationToken);

        var todoTasks = await query
            .ApplyPagination(pageNumber, pageSize)
            .AsQueryable()
            .Select(t => new TodoTask {Id = t.Id,
                Title = t.Title,
                CreatedBy = t.CreatedBy, 
                IsCompleted = t.IsCompleted,
                DueDate = t.DueDate,
                Description = t.Description})
            .ToListAsync(cancellationToken);

        return new PagedResponse<TodoTask>
        {
            Data = todoTasks,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }

    public async Task<TodoTask> CreateTask(TodoTask todoTask)
    {
        var createdTodoTask = await _todoTaskContext.TodoTasks.AddAsync(todoTask);
        
        await _todoTaskContext.SaveChangesAsync();

        return createdTodoTask.Entity;
    }

    public async Task<TodoTask> UpdateTask(TodoTask todoTask)
    {
        var existingTask = await _todoTaskContext.TodoTasks
            .FindAsync(todoTask.Id);
        
        existingTask.Title = todoTask.Title;
        existingTask.Description = todoTask.Description;
        existingTask.DueDate = todoTask.DueDate;
        existingTask.IsCompleted = todoTask.IsCompleted;

        await _todoTaskContext.SaveChangesAsync();

        return existingTask;
    }

    public async Task<int> DeleteTask(int todoTaskId)
    {
        await _todoTaskContext.TodoTasks.Where(u => u.Id == todoTaskId).ExecuteDeleteAsync();
        await _todoTaskContext.SaveChangesAsync();
        
        return todoTaskId;
    }
}