using LojaEsportiva_Prototipo.Domain;

namespace LojaEsportiva_Prototipo.Dto
{
    public class ProdutoRequest
    {
        public string Nome { get; set; }
        public int IdMarca { get; set; }
        public decimal Peso { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string Tamanho { get; set; }
        public string CodigoBarras { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
