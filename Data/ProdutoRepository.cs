using LojaEsportiva_Prototipo.Domain;
using LojaEsportiva_Prototipo.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace LojaEsportiva_Prototipo.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IConfiguration _configuration;

        private string connectionString;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetSection("DefaultConnection").Value;
        }

        public async Task<bool> CreateAsync(Produto produto)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("INSERT INTO TB_PRODUTO (NOME, PRECO, ID_MARCA , PESO, COR, DESCRICAO, TAMANHO, QUANTIDADE_ESTOQUE, CODIGO_BARRAS) VALUES (@NOME, @PRECO, @ID_MARCA, @PESO, @COR, @DESCRICAO, @TAMANHO, @QUANTIDADE_ESTOQUE, @CODIGO_BARRAS)", new { NOME = produto.Nome, PRECO = produto.Preco, ID_MARCA = produto.IdMarca, PESO = produto.Peso, COR = produto.Cor, DESCRICAO = produto.Descricao, TAMANHO = produto.Tamanho, QUANTIDADE_ESTOQUE = produto.QuantidadeEstoque, CODIGO_BARRAS = produto.CodigoBarras }).ConfigureAwait(false);
                return retorno > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("DELETE FROM TB_PRODUTO WHERE ID_PRODUTO = @ID_PRODUTO", new { ID_PRODUTO = id }).ConfigureAwait(false);
                return retorno > 0;
            }
        }

        public async Task<IList<Produto>> GetAllAsync()
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var produtos = await connection.QueryAsync<Produto>("SELECT ID_PRODUTO as Id, NOME as Nome, PRECO, ID_MARCA as IdMarca, PESO, COR, DESCRICAO, TAMANHO, QUANTIDADE_ESTOQUE as QuantidadeEstoque, CODIGO_BARRAS as CodigoBarras  FROM TB_PRODUTO").ConfigureAwait(false);
                return produtos.ToList();
            }
        }

        public async Task<Produto> GetAsync(int id)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var produto = await connection.QueryFirstOrDefaultAsync<Produto>("SELECT ID_PRODUTO as Id, NOME as Nome, PRECO, ID_MARCA as IdMarca, PESO, COR, DESCRICAO, TAMANHO, QUANTIDADE_ESTOQUE as QuantidadeEstoque, CODIGO_BARRAS as CodigoBarras FROM TB_PRODUTO WHERE ID_PRODUTO = @ID_PRODUTO", new { ID_PRODUTO = id }).ConfigureAwait(false);
                return produto;
            }
        }

        public async Task<bool> UpdateAsync(Produto produto)
        {
            await using (var connection = new MySqlConnection(connectionString))
            {
                var retorno = await connection.ExecuteAsync("UPDATE TB_PRODUTO SET NOME = @NOME, PRECO = @PRECO, ID_MARCA = @ID_MARCA, PESO = @PESO, COR = @COR, DESCRICAO = @DESCRICAO, TAMANHO = @TAMANHO, QUANTIDADE_ESTOQUE = @QUANTIDADE_ESTOQUE, CODIGO_BARRAS = @CODIGO_BARRAS WHERE ID_PRODUTO = @ID_PRODUTO", new { NOME = produto.Nome, PRECO = produto.Preco, ID_MARCA = produto.IdMarca, PESO = produto.Peso, COR = produto.Cor, DESCRICAO = produto.Descricao, TAMANHO = produto.Tamanho, QUANTIDADE_ESTOQUE = produto.QuantidadeEstoque, CODIGO_BARRAS = produto.CodigoBarras, ID_PRODUTO = produto.Id }).ConfigureAwait(false);
                return retorno > 0;
            }
        }
    }
}
