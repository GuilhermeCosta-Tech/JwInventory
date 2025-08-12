using JwInventory.Application.DTOs.Auth;
using JwInventory.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

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
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "Registrar novo usuário",
            Description = "Cria um novo usuário com base nos dados fornecidos. A 'Role' pode ser 'Admin', 'Gerente' ou 'Colaborador'."
        )]
        [SwaggerResponse(200, "Usuário registrado com sucesso", typeof(string))]
        [SwaggerResponse(400, "Dados inválidos ou usuário já existe")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                var response = await _authService.RegisterAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Realiza login de usuário.
        /// </summary>
        /// <param name="dto">Credenciais do usuário.</param>
        /// <returns>Token JWT se o login for bem-sucedido.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "Login de usuário",
            Description = "Autentica um usuário e retorna um token JWT se as credenciais estiverem corretas."
        )]
        [SwaggerResponse(200, "Login realizado com sucesso", typeof(string))]
        [SwaggerResponse(401, "Credenciais inválidas")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            try
            {
                var response = await _authService.LoginAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        

        /// <summary>
        /// Endpoint acessível por Gerentes e Admins.
        /// </summary>
        /// <returns>Mensagem de acesso para Gerente ou Admin.</returns>
        [HttpGet("manager-and-admin")]
        [Authorize(Roles = "Gerente,Admin")]
        [SwaggerOperation(
            Summary = "Endpoint para Gerentes e Admins",
            Description = "Acessível por usuários com as roles 'Gerente' ou 'Admin'."
        )]
        public IActionResult GetManagerAndAdminData()
        {
            return Ok(new { message = "Você tem acesso como Gerente ou Admin." });
        }

        /// <summary>
        /// Endpoint para todos os usuários autenticados.
        /// </summary>
        /// <returns>Mensagem de dados autenticados.</returns>
        [HttpGet("all-users")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Endpoint para todos os usuários autenticados",
            Description = "Qualquer usuário logado (Admin, Gerente, Colaborador) pode acessar."
        )]
        public IActionResult GetAllUsersData()
        {
            return Ok(new { message = "Você está autenticado e pode ver estes dados." });
        }
    }
}
