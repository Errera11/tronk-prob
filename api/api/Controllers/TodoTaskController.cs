using System.Security.Claims;
using System.Text.Json;
using api.Dto;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("api/todotasks")]
public class TodoTaskController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ITodoTaskService _todoTaskService;

    public TodoTaskController(ITodoTaskService todoTaskService, IMapper todoTaskMapper)
    {
        _mapper = todoTaskMapper; 
        _todoTaskService = todoTaskService; 
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoTaskDto>> CreateTask([FromBody] CreateTodoTaskDto createTaskDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine("UserId: " + userId);
        
        TodoTask newTodoTask = _mapper.Map<TodoTask>(createTaskDto);
        User userIssuer = await _userService.GetUserById(int.Parse(userId));
        newTodoTask.CreatedBy = userIssuer;
        
        TodoTask createdTodoTask = await _todoTaskService.CreateTask(newTodoTask);
        Console.WriteLine(JsonSerializer.Serialize(createdTodoTask));
        return CreatedAtAction(nameof(GetTaskById), new { id = createdTodoTask.Id }, createdTodoTask);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<TodoTask>>> GetTasks()
    {
        List<TodoTask> taskList = await _todoTaskService.GetTaskList();

        var taskDtoList = _mapper.Map<List<TodoTaskDto>>(taskList);

        return Ok(taskDtoList);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoTask>> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        TodoTask newTodoTask = new TodoTask
        {
            Id = id,
            Description = updateTaskDto.description,
            Title = updateTaskDto.title,
            DueDate = updateTaskDto.dueDate,
            IsCompleted = updateTaskDto.is_completed 
        };
        
        TodoTask updatedTask = await _todoTaskService.UpdateTask(newTodoTask);
        
        var taskDto = _mapper.Map<List<TodoTaskDto>>(updatedTask);
        
        return Ok(_mapper.Map<TodoTaskDto>(taskDto));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> DeleteTask(int id)
    {
        var deletedTaskId = await _todoTaskService.DeleteTask(id);

        return deletedTaskId;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<TodoTask> GetTaskById()
    {
        throw new NotImplementedException();
    }

}