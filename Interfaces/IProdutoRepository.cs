using LojaEsportiva_Prototipo.Domain;

namespace LojaEsportiva_Prototipo.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IList<Produto>> GetAllAsync();
        Task<Produto> GetAsync(int id);
        Task<bool> CreateAsync(Produto produto);
        Task<bool> UpdateAsync(Produto produto);
        Task<bool> DeleteAsync(int id);
    }
}
