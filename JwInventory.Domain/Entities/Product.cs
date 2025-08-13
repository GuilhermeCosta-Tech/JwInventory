using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class Product
    {
        public Product(string nome, Guid id)
        {
            Nome = nome;
            Id = id;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public string Description { get; set; }
        public decimal Preco { get; set; }
        public int StockQuantity { get; set; }

        public void ExibirFichaTecnica()
        {
            Console.WriteLine($"Nome: {Nome}");
        }

        public override string ToString()
        {
            return @$"Id: {Id}
        Nome: {Nome}";
        }
    }
}
