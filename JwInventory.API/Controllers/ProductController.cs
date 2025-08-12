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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obter todos os produtos", Description = "Retorna uma lista de todos os produtos cadastrados.")]
        [SwaggerResponse(200, "Sucesso", typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.ListByFilterAsync();
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obter produto por ID", Description = "Retorna um produto específico pelo seu ID.")]
        [SwaggerResponse(200, "Sucesso", typeof(ProductDto))]
        [SwaggerResponse(404, "Produto não encontrado")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Gerente")]
        [SwaggerOperation(Summary = "Criar um novo produto", Description = "Cria um novo produto no sistema. Requer permissão de Administrador ou Gerente.")]
        [SwaggerResponse(201, "Produto criado com sucesso", typeof(ProductDto))]
        [SwaggerResponse(400, "Dados inválidos")]
        public async Task<IActionResult> Create([FromBody] ProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            // Supondo que o ProductDto recebido já contém o Id do produto criado
            return CreatedAtAction(nameof(GetById), new { id = createProductDto.Id }, createProductDto);
        }
            
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Gerente")]
        [SwaggerOperation(Summary = "Atualizar um produto", Description = "Atualiza um produto existente. Requer permissão de Administrador ou Gerente.")]
        [SwaggerResponse(200, "Produto atualizado com sucesso", typeof(ProductDto))]
        [SwaggerResponse(400, "Dados inválidos")]
        [SwaggerResponse(404, "Produto não encontrado")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto updateProductDto)
        {
            var product = await _productService.UpdateAsync(id, updateProductDto);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Deletar um produto", Description = "Deleta um produto pelo seu ID. Requer permissão de Administrador.")]
        [SwaggerResponse(204, "Produto deletado com sucesso")]
        [SwaggerResponse(404, "Produto não encontrado")]
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
