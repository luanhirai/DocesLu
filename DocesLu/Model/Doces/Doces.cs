using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocesLu.Model.Doces
{
    [Table("Doces")]
    public class Doces
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public string Mensagem { get; set; }

        public Doces(string titulo, string descricao, string preco, string imagemUrl, string mensagem)
        {
            Titulo = titulo;
            Descricao = descricao;
            Preco = preco;
            ImagemUrl = imagemUrl;
            Mensagem = mensagem;
        }
    }
}