using LojaEsportiva_Prototipo.Domain;
using LojaEsportiva_Prototipo.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace LojaEsportiva_Prototipo.Data
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly IConfiguration _configuration;

        private string connectionString;

        public MarcaRepository(IConfiguration configuration)
        {
             _configuration = configuration;
             connectionString = _configuration.GetSection("DefaultConnection").Value;
        }

        public async Task<bool> DeleteAsync(int id) 
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("DELETE FROM TB_MARCA WHERE ID_MARCA = @ID_MARCA", new { ID_MARCA = id }).ConfigureAwait(false);
                return retorno > 0;
            }
        }

        public async Task<IList<Marca>> GetAllAsync()
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                return (await connection.QueryAsync<Marca>("SELECT ID_MARCA as Id, NOME as NomeMarca, PAIS_ORIGEM as PaisOrigem FROM TB_MARCA").ConfigureAwait(false)).AsList();
            }
        }

        public async Task<bool> CreateAsync(Marca marca)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("INSERT INTO TB_MARCA (NOME, PAIS_ORIGEM) VALUES (@NOME, @PAIS_ORIGEM) ", new { NOME = marca.NomeMarca, PAIS_ORIGEM = marca.PaisOrigem }).ConfigureAwait(false);
                return retorno > 0;
            }
        }

        public async Task<Marca> GetAsync(int id)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Marca>("SELECT ID_MARCA as Id, NOME as NomeMarca, PAIS_ORIGEM as PaisOrigem FROM TB_MARCA WHERE ID_MARCA = @ID_MARCA", new { ID_MARCA = id }).ConfigureAwait(false);
            }
        }

        public async Task<bool> UpdateAsync(Marca marca)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("UPDATE TB_MARCA SET NOME = @NOME, PAIS_ORIGEM = @PAIS_ORIGEM WHERE ID_MARCA = @ID_MARCA", new { NOME = marca.NomeMarca, PAIS_ORIGEM = marca.PaisOrigem, ID_MARCA = marca.Id }).ConfigureAwait(false);
                return retorno > 0;
            }
        }
    }
}
