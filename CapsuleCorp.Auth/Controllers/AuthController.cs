using CapsuleCorp.Auth.DTOs;
using CapsuleCorp.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize] // Garante que apenas usuários com Token JWT válido acessem
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto dto)
        {
            try
            {
                // Camada de Segurança: Extraímos o Guid do Claim 'NameIdentifier' injetado pelo Middleware de Auth
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new { message = "Identificador do usuário não encontrado no token." });

                // Conversão do ID do token para o tipo Guid (Type Safety)
                var userId = Guid.Parse(userIdClaim);

                // Chamada do serviço com a lógica de negócio
                var updatedUser = await _authService.UpdateUserAsync(userId, dto);

                var fusoBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

                var dataLocal = TimeZoneInfo.ConvertTimeFromUtc(updatedUser.LastUpdateDate ?? DateTime.UtcNow, fusoBrasilia);

                return Ok(new
                {
                    message = "Perfil atualizado com sucesso!",
                    user = new { updatedUser.Name, updatedUser.Email },
                    updatedAtLocal = dataLocal.ToString("yyyy-MM-dd HH:mm:ss.fffffff")
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize] // <--- Cadeado
        public IActionResult GetDadosProtegidos()
        {
            return Ok("Você só vê isso porque o Program.cs validou seu Token!");
        }
    }
}