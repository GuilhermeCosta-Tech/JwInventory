using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Security.Claims;

namespace JwInventory.API.Controllers
{
    /// <summary>
    /// Gerencia os endpoints exclusivos para usuários com o papel de Administrador.
    /// </summary>
    /// <remarks>
    /// O acesso a este controlador é protegido e requer um token JWT válido com a role "Admin".
    /// </remarks>
    [ApiController]
    [Route("api/admin")]
    [Authorize(Policy = "AdminsOnly")]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// Retorna uma mensagem de teste para administradores.
        /// </summary>
        /// <returns>Uma mensagem de sucesso para administradores autenticados.</returns>
        [HttpGet("secret")]
        [SwaggerOperation(
            Summary = "Obter segredo do administrador",
            Description = "Retorna uma mensagem secreta de teste. Apenas administradores podem acessar."
        )]
        [SwaggerResponse(200, "Mensagem secreta retornada com sucesso.")]
        [SwaggerResponse(401, "Não autorizado (token inválido ou ausente).")]
        [SwaggerResponse(403, "Acesso negado (usuário não tem a role 'Admin').")]
        public IActionResult GetAdminSecret()
        {
            return Ok(new { Message = "Você está logado como Administrador!" });
        }

        /// <summary>
        /// Retorna dados simulados de um painel de controle administrativo.
        /// </summary>
        /// <returns>Uma mensagem representando o acesso ao dashboard.</returns>
        [HttpGet("dashboard")]
        [SwaggerOperation(
            Summary = "Obter dashboard do administrador",
            Description = "Retorna informações simuladas do dashboard. Apenas administradores podem acessar."
        )]
        [SwaggerResponse(200, "Dashboard retornado com sucesso.")]
        [SwaggerResponse(401, "Não autorizado.")]
        [SwaggerResponse(403, "Acesso negado.")]
        public IActionResult GetAdminDashboard()
        {
            return Ok(new { Message = "Este é o painel do Adm!" });
        }   

        /// <summary>
        /// Retorna os detalhes do administrador autenticado com base no token.
        /// </summary>
        /// <remarks>
        /// Este endpoint inspeciona o token JWT do usuário logado para extrair suas informações (claims),
        /// como ID, email e papéis (roles). É útil para depuração e verificação de identidade.
        /// </remarks>
        /// <returns>Um objeto com os detalhes do usuário logado.</returns>
        [HttpGet("me")]
        [SwaggerOperation(
            Summary = "Verifica os dados do usuário autenticado",
            Description = "Retorna os detalhes e as permissões (claims) do administrador atualmente logado, com base no token JWT."
        )]
        [SwaggerResponse(200, "Dados do usuário autenticado retornados com sucesso.")]
        [SwaggerResponse(401, "Não autorizado.")]
        public IActionResult GetMyInfo()
        {
            var userInfo = new
            {
                // Coleta as informações do usuário autenticado a partir do token JWT
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),  
                Email = User.FindFirstValue(ClaimTypes.Email),  
                Roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList(),

                // Coleta todas as claims do usuário autenticado
                AllClaims = User.Claims.Select(c => new { 
                    Type = c.Type, 
                    Value = c.Value  
                }).ToList()
            };

            return Ok(userInfo);
        }
    }
}
