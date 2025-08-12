using Microsoft.AspNetCore.Identity;

namespace JwInventory.Domain.Entities
{
    public class PessoaComAcesso : IdentityUser<int>
    {
        // Você pode adicionar propriedades extras aqui se precisar, 
        // por exemplo: public string? FullName { get; set; }
    }
}
