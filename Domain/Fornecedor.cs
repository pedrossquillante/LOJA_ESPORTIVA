namespace LojaEsportiva_Prototipo.Domain
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public int Cnpj { get; set; }
        public string Email { get; set; }
        public int Telefone { get; set; }
        public string EnderecoCompleto { get; set; }
    }
}
