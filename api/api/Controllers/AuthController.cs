using api.Dto;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    
    public AuthController(IMapper mapper, IUserService userService, IAuthService authService)
    {
        _mapper = mapper;
        _userService = userService;
        _authService = authService;
    }
    
    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Signup([FromBody] CreateUserDto createUserDto)
    {
        User newUser = _mapper.Map<User>(createUserDto);

        User? existingUser = await _userService.GetUserByEmail(newUser.Email);
        
        if (existingUser != null)
        {
            return BadRequest(new
            {
                message = "Email already exists"
            });
        }
        
        var hashedPassword = _authService.HashPassword(newUser.Password);
        User createdUser = await _userService.CreateUser(new User
        {
            Email = newUser.Email,
            Password = hashedPassword,
        });
        
        UserDto userResponseDto = _mapper.Map<UserDto>(createdUser);
        var tokenString = _authService.GenerateJSONWebToken(userResponseDto);
        
        return Ok(new { AccessToken = tokenString , user = userResponseDto });
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Login([FromBody] CreateUserDto userDto)
    {
        User newUser = _mapper.Map<User>(userDto);

        User? existingUser = await _userService.GetUserByEmail(newUser.Email);
        if (existingUser == null)
        {
            return BadRequest(new
            {
                message = "Invalid credentials"
            });
        }

        var isValidPassword = _authService.VerifyPassword(existingUser.Password, userDto.password);
        if (!isValidPassword)
        {
            return BadRequest(new
            {
                message = "Invalid credentials"
            });
        }
        
        UserDto userResponseDto = _mapper.Map<UserDto>(existingUser);
        var tokenString = _authService.GenerateJSONWebToken(userResponseDto);
        
        return Ok(new { AccessToken = tokenString , user = userResponseDto });
    }
}