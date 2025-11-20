using HybridWork.Dtos;
using HybridWork.Models;
using HybridWork.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace HybridWork.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;
        private readonly JwtService _jwtService;

        public AuthController(IMongoClient client, JwtService jwtService, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            var dbName = config.GetSection("MongoDBSettings:DatabaseName").Value ?? "HybridWorkDB";
            var database = client.GetDatabase(dbName);
            _users = database.GetCollection<User>("Users");
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDto dto)
        {
            var user = await _users.Find(u => u.Username == dto.Username).FirstOrDefaultAsync();
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Usuário ou senha inválidos.");

            var token = _jwtService.GenerateToken(user.Id!, user.Username, user.Role);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> Register([FromBody] UserLoginDto dto)
        {
            var exists = await _users.Find(u => u.Username == dto.Username).AnyAsync();
            if (exists) return BadRequest("Usuário já existe.");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Employee"
            };

            await _users.InsertOneAsync(user);

            return Ok(new UserReadDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            });
        }
    }
}
