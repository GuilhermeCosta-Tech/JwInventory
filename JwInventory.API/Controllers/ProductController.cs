using JwInventory.Application.DTOs.Product;
using JwInventory.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JwInventory.API.Controllers
{
    /// <summary>
    /// Gerencia as operações de CRUD para os produtos somente com autenticação do usuário.
    /// </summary>
    [ApiController]
    [Route("api/product")]
    [Authorize] // Solicita autenticação para todos os endpoints, exceto endpoints marcados com '[AllowAnonymous]'.
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retorna uma lista de todos os produtos cadastrados.
        /// </summary>
        /// <remarks>
        /// Este endpoint é público e não requer autenticação!
        /// </remarks>
        /// <returns>Uma lista de produtos.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obter todos os produtos")]
        [SwaggerResponse(200, "A lista de produtos foi retornada com sucesso.", typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.ListByFilterAsync();
            return Ok(products);
        }

        /// <summary>
        /// Busca um produto específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do produto a ser buscado.</param>
        /// <remarks>
        /// Este endpoint é público e não requer autenticação.
        /// </remarks>
        /// <returns>O produto correspondente ao ID fornecido.</returns>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obter produto por ID")]
        [SwaggerResponse(200, "O produto foi encontrado e retornado com sucesso.", typeof(ProductDto))]
        [SwaggerResponse(404, "Nenhum produto com o ID especificado foi encontrado.")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Cria um novo produto no sistema.
        /// </summary>
        /// <param name="createProductDto">Os dados do produto a ser criado.</param>
        /// <remarks>
        /// Requer permissão de **Administrador** ou **Gerente**. O ID do produto é gerado automaticamente.
        /// </remarks>
        /// <returns>O produto recém-criado, incluindo seu novo ID.</returns>
        [HttpPost]
        [Authorize(Policy = "AdminsAndManagersOnly")]
        [SwaggerOperation(Summary = "Criar um novo produto")]
        [SwaggerResponse(201, "Produto criado com sucesso.", typeof(ProductDto))]
        [SwaggerResponse(400, "Os dados fornecidos para o produto são inválidos.")]
        [SwaggerResponse(401, "Não autorizado (token inválido ou ausente).")]
        [SwaggerResponse(403, "Acesso negado (usuário não tem a permissão necessária).")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            // Este método ainda retorna somente void, portanto não há retorno de dados do produto criado ainda!
            // Retorna 201 Created sem corpo, pois não há retorno do produto.
            return Created(string.Empty, null);
        }
            
       /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">O ID do produto a ser atualizado.</param>
        /// <param name="updateProductDto">Os novos dados para o produto.</param>
        /// <remarks>
        /// Requer permissão de **Administrador** ou **Gerente**.
        /// </remarks>
        /// <returns>O produto com os dados atualizados.</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminsAndManagersOnly")]
        [SwaggerOperation(Summary = "Atualizar um produto")]
        [SwaggerResponse(200, "Produto atualizado com sucesso.", typeof(ProductDto))]
        [SwaggerResponse(400, "Os dados fornecidos são inválidos.")]
        [SwaggerResponse(404, "Nenhum produto com o ID especificado foi encontrado.")]
        [SwaggerResponse(401, "Não autorizado.")]
        [SwaggerResponse(403, "Acesso negado.")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto updateProductDto)
        {
            var product = await _productService.UpdateAsync(id, updateProductDto);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Exclui um produto do sistema.
        /// </summary>
        /// <param name="id">O ID do produto a ser excluído.</param>
        /// <remarks>
        /// Requer permissão de **Administrador**. Esta é uma operação destrutiva.
        /// </remarks>
        /// <returns>Nenhum conteúdo se a exclusão for bem-sucedida.</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminsOnly")]
        [SwaggerOperation(Summary = "Deletar um produto")]
        [SwaggerResponse(204, "Produto deletado com sucesso.")]
        [SwaggerResponse(404, "Nenhum produto com o ID especificado foi encontrado.")]
        [SwaggerResponse(401, "Não autorizado.")]
        [SwaggerResponse(403, "Acesso negado.")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _productService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
