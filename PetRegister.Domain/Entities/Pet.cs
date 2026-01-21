using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetRegister.Domain.Enums;

namespace PetRegister.Domain.Entities
{
    // Pet herda de Entity para ter um Id (Guid) automaticamente
    public class Pet : Entity
    {
        // Construtor vazio necessário para o Entity Framework
        public Pet() { }

        public Pet(string nome, DateTime dataNascimento, Sexo sexo, decimal peso)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Peso = peso;
        }

        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        
        [EnumDataType(typeof(Sexo), ErrorMessage = "Selecione um sexo válido.")]
        public Sexo Sexo { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal Peso { get; set; }

        public void AtualizarPeso(decimal novoPeso)
        {
            if (Peso <= 0)
                throw new ArgumentException("O peso deve ser maior que zero.");
            Peso = novoPeso;
        }
    }
}
