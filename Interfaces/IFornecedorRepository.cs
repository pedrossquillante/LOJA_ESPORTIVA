using LojaEsportiva_Prototipo.Domain;

namespace LojaEsportiva_Prototipo.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<IList<Fornecedor>> GetAllAsync();
        Task<Fornecedor> GetAsync(int id);
        Task<bool> CreateAsync(Fornecedor fornecedor);
        Task<bool> UpdateAsync(Fornecedor fornecedor);
        Task<bool> DeleteAsync(int id);
    }
}
