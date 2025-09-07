using DocesLu.Model.Doces;
using DocesLu.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocesLu.Controllers
{
    [Route("doces")]
    [ApiController]
    public class DocesController : ControllerBase
    {
        private readonly IDocesRepository _docesRepository;

        public DocesController(IDocesRepository docesRepository)
        {
            _docesRepository = docesRepository ?? throw new ArgumentNullException();
        }


        [HttpPost("save")]
        public IActionResult Save([FromForm] DocesViewModel docesView)
        {
            if (docesView.ImagemUrl == null || docesView.ImagemUrl.Length == 0)
                return BadRequest(new { message = "Imagem não enviada" });

            // Salva a imagem na pasta Storage
            var fileName = Path.GetFileName(docesView.ImagemUrl.FileName);
            var filePath = Path.Combine("Storage", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                docesView.ImagemUrl.CopyTo(fileStream);
            }

            var imageUrl = $"/Storage/{fileName}";

            var doce = new Doces(
                docesView.Titulo,
                docesView.Descricao,
                docesView.Preco,
                imageUrl,
                docesView.Mensagem
            );

            _docesRepository.Add(doce);

            var r = new RespostaPadrao
            {
                Sucesso = true,
                Dados = doce
            };

            return Ok(r);
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            RespostaPadrao r = new RespostaPadrao();
            var doces = _docesRepository.GetAll();
            if (doces == null || !doces.Any())
            {
                r.Sucesso = false;
                r.Erro = "Nenhum doce encontrado.";
                return NotFound(r);
            }
            return Ok(doces);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var doces = _docesRepository.GetAll().FirstOrDefault(d => d.Id == id);
            if (doces == null)
            {
                return NotFound($"Produto {id} não encontrado");
            }
            return Ok(doces);
        }


        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProd(int id)
        {
            RespostaPadrao r= new RespostaPadrao();
            var doces = _docesRepository.GetAll().FirstOrDefault(d => d.Id == id);
            if (doces == null)
            {
                r.Sucesso = false;
                r.Erro= $"Produto {id} não encontrado.";
                return NotFound(r);
            }

            _docesRepository.Delete(doces);
            r.Sucesso = true;
            r.Dados = $"Produto {id} deletado com sucesso.";
            return Ok(r);
        }
    }
}
