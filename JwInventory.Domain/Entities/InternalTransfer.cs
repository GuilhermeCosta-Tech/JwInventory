using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class InternalTransfer
    {
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public InternalTransfer()
        {
        }
        public InternalTransfer(string nome, string bio, int quantidade, Guid produtoId, Guid destinoId, Guid origemId)
        {
            Nome = nome;
            Bio = bio;
            Id = Guid.NewGuid();
            ProdutoId = produtoId;
            SetorOrigemId = origemId;
            SetorDestinoId = destinoId;
            Quantidade = quantidade;
            DataTransferencia = DateTime.UtcNow;
            ProdutoId = produtoId;
        }

        public string Nome { get; set; }
        public string Bio { get; set; }
        public Guid Id { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public Guid SetorOrigemId { get; private set; }
        public Guid SetorDestinoId { get; private set; }
        public DateTime DataTransferencia { get; private set; }

        public void AdicionarTransferencia(Product product) => Products.Add(product);

        public void ExibirTransferencia()
        {
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"Bio: {Bio}");
            Console.WriteLine("Produtos transferidos:");
            foreach (var product in Products)
            {
                Console.WriteLine($"- {product.Nome}");
            }
        }

        public override string ToString() =>
            $@"Id: {Id}  
                Nome: {Nome}  
                Bio: {Bio}";
    }
}
