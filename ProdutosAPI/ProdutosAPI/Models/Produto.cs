namespace ProdutosAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preço { get; set; }
        public bool Situacao { get; set; }
        public int CategoriaId { get; set; }
        private Categoria Categoria { get; set; }
    }
}
