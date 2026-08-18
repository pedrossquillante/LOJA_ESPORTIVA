using LojaEsportiva_Prototipo.Domain;
using LojaEsportiva_Prototipo.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace LojaEsportiva_Prototipo.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _configuration;

        private string connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetSection("DefaultConnection").Value;
        }

        public async Task<bool> CreateAsync(Cliente cliente)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("INSERT INTO " +
                    "TB_CLIENTE (NOME_COMPLETO, CPF , ENDERECO_COMPLETO, DATA_NASCIMENTO, TELEFONE) " +
                    "VALUES (@NOME_COMPLETO, @CPF, @ENDERECO_COMPLETO, @DATA_NASCIMENTO, @TELEFONE)", 
                    new { NOME_COMPLETO = cliente.NomeCompleto, CPF = cliente.Cpf, ENDERECO_COMPLETO = cliente.EnderecoCompleto, DATA_NASCIMENTO = cliente.DataNascimento, TELEFONE = cliente.Telefone}).ConfigureAwait(false);
                return retorno > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("DELETE FROM TB_CLIENTE WHERE ID_CLIENTE = @ID_CLIENTE", new { ID_CLIENTE = id }).ConfigureAwait(false);
                return retorno > 0;
            }
        }

        public async Task<IList<Cliente>> GetAllAsync()
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var clientes = await connection.QueryAsync<Cliente>("SELECT ID_CLIENTE as Id, NOME_COMPLETO as NomeCompleto, CPF, ENDERECO_COMPLETO as EnderecoCompleto, DATA_NASCIMENTO as DataNascimento, TELEFONE as Telefone FROM TB_CLIENTE").ConfigureAwait(false);
                return clientes.ToList();
            }
        }

        public async Task<Cliente> GetAsync(int id)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var cliente = await connection.QueryFirstOrDefaultAsync<Cliente>("SELECT ID_CLIENTE as Id, NOME_COMPLETO as NomeCompleto, CPF, ENDERECO_COMPLETO as EnderecoCompleto, DATA_NASCIMENTO as DataNascimento, TELEFONE as Telefone " +
                    "FROM TB_CLIENTE WHERE ID_CLIENTE = @ID_CLIENTE", new { ID_CLIENTE = id }).ConfigureAwait(false);
                return cliente;
            }
        }

        public async Task<bool> UpdateAsync(Cliente cliente)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("UPDATE TB_PRODUTO " +
                    "SET NOME_COMPLETO = @NOME_COMPLETO, CPF = @CPF, ENDERECO_COMPLETO = @ENDERECO_COMPLETO, DATA_NASCIMENTO = @DATA_NASCIMENTO, TELEFONE = @TELEFONE " +
                    "WHERE ID_CLIENTE = @ID_CLIENTE", new { NOME_COMPLETO = cliente.NomeCompleto, CPF = cliente.Cpf, ENDERECO_COMPLETO = cliente.EnderecoCompleto, DATA_NASCIMENTO = cliente.DataNascimento, TELEFONE = cliente.Telefone }).ConfigureAwait(false);
                return retorno > 0;
            }
        }
    }
}
