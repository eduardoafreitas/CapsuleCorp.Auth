using CapsuleCorp.Auth.API.Data;
using CapsuleCorp.Auth.DTOs;
using CapsuleCorp.Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapsuleCorp.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerDto) // 1. Recebe o DTO
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return BadRequest("Este e-mail já está cadastrado.");
            }

            // 2. Mapeamento Manual (Transformando DTO em Model)
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                CreateDate = DateTime.UtcNow
            };

            // 3. Persistência
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // 4. Resposta Segura (Não devolvemos o objeto 'newUser' inteiro para não vazar o hash)
            return Ok(new { message = "Usuário cadastrado com sucesso!" });
        }
    }
}
