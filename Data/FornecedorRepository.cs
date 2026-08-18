using LojaEsportiva_Prototipo.Domain;
using LojaEsportiva_Prototipo.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace LojaEsportiva_Prototipo.Data
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly IConfiguration _configuration;
        private string connectionString;
        public FornecedorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetSection("DefaultConnection").Value;
        }

        public async Task<IList<Fornecedor>> GetAllAsync()
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var fornecedores = await connection.QueryAsync<Fornecedor>("SELECT ID_FORNECEDOR as Id, RAZAO_SOCIAL as RazaoSocial, CNPJ, ENDERECO_COMPLETO as EnderecoCompleto, EMAIL as Email, TELEFONE as Telefone FROM TB_FORNECEDOR").ConfigureAwait(false);
                return fornecedores.ToList();
            }
        }

        public async Task<Fornecedor> GetAsync(int id)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var fornecedor = await connection.QueryFirstOrDefaultAsync<Fornecedor>("SELECT ID_FORNECEDOR as Id, RAZAO_SOCIAL as RazaoSocial, CNPJ, ENDERECO_COMPLETO as EnderecoCompleto, EMAIL as Email, TELEFONE as Telefone " +
                    "FROM TB_FORNECEDOR WHERE ID_FORNECEDOR = @ID_FORNECEDOR", new { ID_FORNECEDOR = id }).ConfigureAwait(false);
                return fornecedor;
            }
        }

        public async Task<bool> CreateAsync(Fornecedor fornecedor)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("INSERT INTO " +
                    "TB_FORNECEDOR (RAZAO_SOCIAL, CNPJ , ENDERECO_COMPLETO, EMAIL, TELEFONE) " +
                    "VALUES (@RAZAO_SOCIAL, @CNPJ, @ENDERECO_COMPLETO, @EMAIL, @TELEFONE)",
                    new { RAZAO_SOCIAL = fornecedor.RazaoSocial, CNPJ = fornecedor.Cnpj, ENDERECO_COMPLETO = fornecedor.EnderecoCompleto, EMAIL = fornecedor.Email, TELEFONE = fornecedor.Telefone }).ConfigureAwait(false);
                return retorno > 0;
            }
        }

        public async Task<bool> UpdateAsync(Fornecedor fornecedor)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("UPDATE TB_FORNECEDOR " +
                    "SET RAZAO_SOCIAL = @RAZAO_SOCIAL, CNPJ = @CNPJ, ENDERECO_COMPLETO = @ENDERECO_COMPLETO, EMAIL = @EMAIL, TELEFONE = @TELEFONE " +
                    "WHERE ID_FORNECEDOR = @ID_FORNECEDOR", new { RAZAO_SOCIAL = fornecedor.RazaoSocial, CNPJ = fornecedor.Cnpj, ENDERECO_COMPLETO = fornecedor.EnderecoCompleto, EMAIL = fornecedor.Email, TELEFONE = fornecedor.Telefone }).ConfigureAwait(false);
                return retorno > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("DELETE FROM TB_FORNECEDOR WHERE ID_FORNECEDOR = @ID_FORNECEDOR", new { ID_FORNECEDOR = id }).ConfigureAwait(false);
                return retorno > 0;
            }
        }
    }
}
