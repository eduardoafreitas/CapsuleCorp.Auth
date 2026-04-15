using CapsuleCorp.Auth.DTOs;
using CapsuleCorp.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapsuleCorp.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Agora injetamos apenas o serviço, não mais o banco diretamente
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            try
            {
                // Delegamos toda a lógica (validação, hash, salvar) para o serviço
                var user = await _authService.RegisterAsync(registerDto);

                return CreatedAtAction(nameof(Register), new { message = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                // O serviço lançará exceções se algo der errado (ex: e-mail já existe)
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Chamamos o serviço que acabamos de criar
            var token = await _authService.LoginAsync(loginDto);

            // Se o serviço retornar null, é porque a senha ou e-mail estão errados
            if (token == null)
            {
                return Unauthorized(new { message = "E-mail ou senha inválidos." });
            }

            // Se deu certo, retornamos o Token JWT para o cliente
            return Ok(new { token = token });
        }

        [HttpGet]
        [Authorize] // <--- Cadeado
        public IActionResult GetDadosProtegidos()
        {
            return Ok("Você só vê isso porque o Program.cs validou seu Token!");
        }
    }
}