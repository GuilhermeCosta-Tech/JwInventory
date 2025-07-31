using JwInventory.Application.DTOs.Auth;
using JwInventory.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JwInventory.API.Controllers
{
    /// <summary>
    /// Controlador para autenticação de usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Construtor do controlador de autenticação.
        /// </summary>
        /// <param name="authService">Serviço de autenticação.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="dto">Dados do usuário para registro.</param>
        /// <returns>Token JWT do usuário registrado.</returns>
        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Registrar novo usuário",
            Description = "Cria um novo usuário e retorna um token JWT."
        )]
        [SwaggerResponse(200, "Usuário registrado com sucesso", typeof(string))]
        [SwaggerResponse(400, "Dados inválidos ou usuário já existe")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Realiza login de usuário.
        /// </summary>
        /// <param name="dto">Credenciais do usuário.</param>
        /// <returns>Token JWT se o login for bem-sucedido.</returns>
        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Login de usuário",
            Description = "Realiza o login e retorna um token JWT."
        )]
        [SwaggerResponse(200, "Login realizado com sucesso", typeof(string))]
        [SwaggerResponse(401, "Credenciais inválidas")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
    }
}
