using API.BackEnd.DTOS.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.BackEnd.Interfaces;
using API.BackEnd.Models;
using Microsoft.IdentityModel.Tokens;
using MODELOS.Shared;
using Microsoft.Extensions.Options;

namespace API.BackEnd.Services
{
    public class AuthService : IAuthService
    {
        private readonly ReseniasLibrosContext _context;
        private readonly JwtSettings _jwt;

        public AuthService(ReseniasLibrosContext context, IOptions<JwtSettings> options)
        {
            _context = context;
            _jwt = options.Value;
        }

        public async Task<AuthResultDto> RegisterAsync(RegistroDto dto)
        {
            // Verificar usuario único
            if (_context.Usuarios.Any(u => u.Username == dto.Username))
                throw new ArgumentException("El username ya está en uso.");
            if (_context.Usuarios.Any(u => u.Email == dto.Email))
                throw new ArgumentException("El email ya está registrado.");

            // Crear usuario
            var user = new Usuario
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            // Generar token
            return GenerateToken(user);
        }

        public async Task<AuthResultDto> LoginAsync(LoginDto dto)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new ArgumentException("Credenciales inválidas.");

            return GenerateToken(user);
        }

        private AuthResultDto GenerateToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UsuarioId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwt.ExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new AuthResultDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires
            };
        }
    }
}
