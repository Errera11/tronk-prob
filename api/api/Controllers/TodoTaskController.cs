using System.Security.Claims;
using System.Text.Json;
using api.Common;
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

    public TodoTaskController(ITodoTaskService todoTaskService, IMapper todoTaskMapper, IUserService userService)
    {
        _mapper = todoTaskMapper;
        _userService = userService;
        _todoTaskService = todoTaskService; 
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoTaskDto>> CreateTask([FromBody] CreateTodoTaskDto createTaskDto)
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userEmail))
        {
            return BadRequest(new { message = "User not authenticated" });
        }

        TodoTask newTodoTask = _mapper.Map<TodoTask>(createTaskDto);
        User userIssuer = await _userService.GetUserByEmail(userEmail);
        newTodoTask.UserId = userIssuer.Id;
        
        TodoTask createdTodoTask = await _todoTaskService.CreateTask(newTodoTask);
        Console.WriteLine(JsonSerializer.Serialize(createdTodoTask));
        return CreatedAtAction(nameof(GetTaskById), new { id = createdTodoTask.Id }, createdTodoTask);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResponse<TodoTaskDto>>> GetTasks([FromQuery] TodoTaskQueryFilter filter, CancellationToken cancellationToken)
    {
        PagedResponse<TodoTask> paginatedData = await _todoTaskService.GetTaskList(filter, cancellationToken);

        var response = new PagedResponse<TodoTaskDto>
        {
            Data = _mapper.Map<List<TodoTaskDto>>(paginatedData.Data),
            PageNumber = paginatedData.PageNumber,
            PageSize = paginatedData.PageSize,
            TotalRecords = paginatedData.TotalRecords,
            TotalPages = paginatedData.TotalPages
        };
        
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoTaskDto>> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        TodoTask newTodoTask = new TodoTask
        {
            Id = id,
            Description = updateTaskDto.description,
            Title = updateTaskDto.title,
            DueDate = updateTaskDto.dueDate,
            IsCompleted = updateTaskDto.isCompleted 
        };
        
        TodoTask updatedTask = await _todoTaskService.UpdateTask(newTodoTask);
        
        var taskDto = _mapper.Map<TodoTaskDto>(updatedTask);
        
        return Ok(_mapper.Map<TodoTaskDto>(taskDto));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> DeleteTask(int id)
    {
        var deletedTaskId = await _todoTaskService.DeleteTask(id);

        return Ok(deletedTaskId);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoTaskDto>> GetTaskById(int id)
    {
        var task = await _todoTaskService.GetTaskById(id);

        if (task == null)
        {
            return NotFound(new
            {
                message = "Task not found"
            });
        }

        var taskDto = _mapper.Map<TodoTaskDto>(task);
        
        return Ok(taskDto);
    }

}