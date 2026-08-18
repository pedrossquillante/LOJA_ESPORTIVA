namespace LojaEsportiva_Prototipo.Interfaces;
using LojaEsportiva_Prototipo.Domain;

  public interface IMarcaRepository
  {
     Task<IList<Marca>> GetAllAsync();
     Task<Marca> GetAsync(int id);
     Task<bool> CreateAsync(Marca marca);
     Task<bool> UpdateAsync(Marca marca);
     Task<bool> DeleteAsync(int id);
  }

