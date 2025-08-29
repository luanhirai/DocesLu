using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocesLu.Model.Doces
{
    [Table("Doces")]
    public class Doces
    {
        [Key]
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Preco { get; private set; }
        public string? ImagemUrl { get; private set; }
        public string Mensagem { get; private set; }

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