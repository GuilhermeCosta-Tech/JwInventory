using FluentValidation;
using JwInventory.Application.DTOs.Product;
using JwInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Validations
{
    public class ProductValidation : AbstractValidator<ProductDto>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .Length(2, 50).WithMessage("O nome do produto deve ter entre 2 e 100 caracteres.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("A descrição do produto é obrigatória.")
                .Length(10, 300).WithMessage("A descrição do produto deve ter entre 10 e 500 caracteres.");
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");
            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade do produto não pode ser negativa.");
        }
    }
}
