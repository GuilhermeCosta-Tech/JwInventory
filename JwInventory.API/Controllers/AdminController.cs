using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JwInventory.API.Controllers
{
    /// <summary>
    /// Controlador para endpoints exclusivos de administradores.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
