using DevInSales.Context;
using DevInSales.DTOs;
using DevInSales.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SqlContext _context;

        public AuthController(SqlContext context)
        {
            _context = context;
        }
        
        [HttpPost("authenticate")]
        public async Task<ActionResult> AuthenticateAsync([FromBody] UserLoginDto dto)

            // TODO: validar se o dto está preenchido.
        {
            var user = await _context.User.Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null) return BadRequest(new { Message = "Usuário ou senha inválidos." });

            var token = TokenService.GenerateToken(user);


            // TODO: usar o DTO >> UserResponseDTO
            var result = new
            {
                token,
                User = new
                {
                    user.Id,
                    user.Name,
                    user.Email
                }
            };

            return Ok(new { result });
        }
    }
}
