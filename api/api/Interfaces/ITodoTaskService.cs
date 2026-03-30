using api.Models;

namespace api.Interfaces;

public interface ITodoTaskService
{
    public Task<List<TodoTask>> GetTaskList();
    public Task<TodoTask> CreateTask(TodoTask todoTask);
    public Task<TodoTask> UpdateTask(TodoTask todoTask);
    public Task<int> DeleteTask(int todoTaskId);
}
