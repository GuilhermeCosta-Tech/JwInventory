using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Security.Claims;

namespace JwInventory.API.Controllers
{
    /// <summary>
    /// Controlador para endpoints exclusivos de administradores.
    /// </summary>
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// Retorna uma mensagem secreta para administradores.
        /// </summary>
        /// <returns>Mensagem para administradores autenticados.</returns>
        [HttpGet("secret")]
        [SwaggerOperation(
            Summary = "Obter segredo do administrador",
            Description = "Retorna uma mensagem secreta. Apenas administradores podem acessar."
        )]
        [SwaggerResponse(200, "Mensagem secreta retornada.")]
        [SwaggerResponse(401, "Não autorizado.")]
        [SwaggerResponse(403, "Acesso negado.")]
        public IActionResult GetAdminSecret()
        {
            return Ok(new { Message = "Você está logado como Administrador!" });
        }

        /// <summary>
        /// Retorna o dashboard do administrador.
        /// </summary>
        /// <returns>Dashboard para administradores.</returns>
        [HttpGet("dashboard")]
        [SwaggerOperation(
            Summary = "Obter dashboard do administrador",
            Description = "Retorna informações do dashboard. Apenas administradores podem acessar."
        )]
        [SwaggerResponse(200, "Dashboard retornado.")]
        [SwaggerResponse(401, "Não autorizado.")]
        [SwaggerResponse(403, "Acesso negado.")]
        public IActionResult GetAdminDashboard()
        {
            return Ok(new { Message = "Este é o painel do Adm!" });
        }

        /// <summary>
        /// Endpoint protegido para Admins.
        /// </summary>
        /// <returns>Mensagem de boas-vindas para Admin.</returns>
        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Endpoint protegido para Admins",
            Description = "Este endpoint só pode ser acessado por usuários com a role 'Admin'."
        )]
        public IActionResult GetAdminData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(new { message = $"Olá, Admin! Seu ID de usuário é {userId}. Você tem acesso a esta área." });
        }

        /// <summary>
        /// Retorna os detalhes do administrador autenticado.
        /// </summary>
        /// <returns>Um objeto com os detalhes do usuário logado.</returns>
        [HttpGet("me")]
        [SwaggerOperation(
            Summary = "Verifica os dados do usuário autenticado",
            Description = "Retorna os detalhes e as permissões (claims) do administrador atualmente logado, com base no token JWT."
        )]
        [SwaggerResponse(200, "Dados do usuário autenticado.")]
        [SwaggerResponse(401, "Não autorizado.")]
        public IActionResult GetMyInfo()
        {
            // O objeto 'User' está disponível em qualquer controller e representa o usuário autenticado.
            // Podemos extrair as informações (claims) que foram colocadas no token durante o login.
            var userInfo = new
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier), // Pega o ID do usuário
                Email = User.FindFirstValue(ClaimTypes.Email),       // Pega o Email
                Roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList(), // Pega todas as "Roles"
                
                // Para uma visão completa, aqui estão todas as claims contidas no token:
                AllClaims = User.Claims.Select(c => new { 
                    Type = c.Type, // O tipo da claim (ex: "role", "email")
                    Value = c.Value  // O valor da claim
                }).ToList()
            };

            return Ok(userInfo);
        }
    }
}
