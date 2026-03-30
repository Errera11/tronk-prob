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
    
    public async Task<List<TodoTask>> GetTaskList()
    {
        return await _todoTaskContext.TodoTasks.ToListAsync();
    }

    public async Task<TodoTask> CreateTask(TodoTask todoTask)
    {
        var createdTodoTask = await _todoTaskContext.TodoTasks.AddAsync(todoTask);
        
        await _todoTaskContext.SaveChangesAsync();

        return createdTodoTask.Entity;
    }

    public async Task<TodoTask> UpdateTask(TodoTask todoTask)
    {
        var existingTask = await _todoTaskContext.TodoTasks.FindAsync(todoTask.Id);

        _todoTaskContext.Entry(existingTask).CurrentValues.SetValues(todoTask);

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