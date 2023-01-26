using Microsoft.AspNetCore.Mvc;
using WebAPI.Context;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost("CriarContato")]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();

            //existem duas opções de retorno:
            //retorno padrão:
            return Ok(contato);

            //retorno que após inserir, informa a rota na qual o usuário/aplicação poderá utilizar para obter o objeto inserido
            //return CreatedAtAction(nameof(ProcurePorId), new { id = contato.Id }, contato);
        }

        [HttpGet("ProcurePorId/{id}")]
        public IActionResult ProcurePorId(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
            {
                return NotFound();
            }

            return Ok(contato);
        }

        [HttpGet("ProcurePorNome/{id}")]
        public IActionResult ProcurePorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));

            return Ok(contatos);
        }

        [HttpPut("Atualizar/{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contato == null)
            {
                return NotFound();
            }

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contato);
        }


        [HttpDelete("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contatoBanco == null)
            {
                return NotFound();
            }

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
