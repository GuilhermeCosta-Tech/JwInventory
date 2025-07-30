using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class Product
    {
        public Product()
        {

        }

        public Product(string nome, int id)
        {
            Nome = nome;
            Id = id;
        }

        public string Nome { get; set; }
        public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
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
