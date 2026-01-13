using PetRegister.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetRegister.Domain.Repositories
{
    public interface IPetRepository
    {
        Task Adicionar(Pet pet);
        Task<Pet?> ObterPorId(Guid id);
        Task<IEnumerable<Pet>> ObterTodos();
        Task Atualizar(Pet pet);
        Task RemoverPet(Guid id);
    }
}
