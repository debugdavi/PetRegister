using Microsoft.EntityFrameworkCore;
using PetRegister.Domain.Entities;
using PetRegister.Infrastructure.Context;
using PetRegister.Infrastructure.Repositories.Interfaces;

namespace PetRegister.Infrastructure.Repositories;

public class PetRepository : IPetRepository
{
    protected readonly PetRegisterContext _context;

    public PetRepository(PetRegisterContext context)
    {
        _context = context;
    }
    
    public async Task<Pet> Adicionar(Pet pet)
    {
        _context.Pets.Add(pet);
        
        await _context.SaveChangesAsync();
        
        return pet;
    }

    public async Task<Pet?> ObterPorId(Guid id)
    {
        return await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Pet>> ObterTodos()
    {
        return await _context.Pets.AsNoTracking().ToListAsync();
    }

    public async Task<Pet> Atualizar(Pet pet)
    {
        _context.Pets.Update(pet);
        
        await _context.SaveChangesAsync();
        
        return pet;
    }

    public async Task<Pet> RemoverPet(Pet pet)
    {
        _context.Pets.Remove(pet);
        
        await _context.SaveChangesAsync();
        
        return pet;
    }
}