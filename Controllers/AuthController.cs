using DocesLu.DTOs.Auth;
using DocesLu.Model.User;
using DocesLu.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using LoginRequest = DocesLu.DTOs.Auth.LoginRequest;
using RegisterRequest = DocesLu.DTOs.Auth.RegisterRequest;

namespace DocesLu.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
        }

        [HttpPost("login")]
        public IActionResult Auth([FromBody] LoginRequest request)
        {
            var user = _authRepository.GetByUsername(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return BadRequest("Username ou password inválido");

            var token = TokenService.GenerateToken(user);
            return Ok(token);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (_authRepository.Exists(request.Username))
                return BadRequest("Username ja existente");

            var hashedPassword= BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new Auth(request.Username, hashedPassword, request.IsAdmin);
            _authRepository.Add(user);

            return Ok("Registrado com sucesso!");
        }

    }
}
