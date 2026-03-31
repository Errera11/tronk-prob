using api.Common;
using api.Models;

namespace api.Interfaces;

public interface ITodoTaskService
{
    public Task<TodoTask> GetTaskById(int todoTaskId);
    public Task<PagedResponse<TodoTask>> GetTaskList(TodoTaskQueryFilter filter, CancellationToken cancellationToken);
    public Task<TodoTask> CreateTask(TodoTask todoTask);
    public Task<TodoTask> UpdateTask(TodoTask todoTask);
    public Task<int> DeleteTask(int todoTaskId);
}
