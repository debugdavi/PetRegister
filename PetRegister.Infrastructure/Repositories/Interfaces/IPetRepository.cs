using PetRegister.Domain.Entities;

namespace PetRegister.Infrastructure.Repositories.Interfaces
{
    public interface IPetRepository
    {
        Task<Pet> Adicionar(Pet pet);
        Task<Pet?> ObterPorId(Guid id);
        Task<IEnumerable<Pet>> ObterTodos();
        Task<Pet> Atualizar(Pet pet);
        Task<Pet> RemoverPet(Pet pet);
    }
}
