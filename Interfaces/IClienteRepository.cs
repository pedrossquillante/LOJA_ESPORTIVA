using LojaEsportiva_Prototipo.Domain;
namespace LojaEsportiva_Prototipo.Interfaces;

    public interface IClienteRepository
    {
        Task<IList<Cliente>> GetAllAsync();
        Task<Cliente> GetAsync(int id);
        Task<bool> CreateAsync(Cliente cliente);
        Task<bool> UpdateAsync(Cliente cliente);
        Task<bool> DeleteAsync(int id);
    }

