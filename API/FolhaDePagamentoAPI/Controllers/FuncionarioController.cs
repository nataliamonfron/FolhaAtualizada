using FolhaDePagamento.Models;
using FolhaDePagamentoAPI.Data;
using Microsoft.AspNetCore.Mvc;


namespace FolhaDePagamentoAPI.Controllers
{
    [ApiController]
    [Route("api/funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly AppDataContext _ctx;
        public FuncionarioController(AppDataContext ctx){
            _ctx = ctx;
        }
        
        // GET: api/funcionario/listar
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Funcionario> funcionarios = _ctx.Funcionarios.ToList();
                return funcionarios.Count == 0 ? NotFound() : Ok(funcionarios); 
            }
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }

        }

        // GET: api/funcionario/{nome}
        [HttpGet]
        [Route("buscar/{nome}")]
        public IActionResult Buscar([FromRoute] string nome)
        {
            try
            {
                Funcionario? funcionarioCadastrado = _ctx.Funcionarios.FirstOrDefault(x => x.Nome == nome);
                if (funcionarioCadastrado != null)
                {
                    return Ok(funcionarioCadastrado);
                }
                return NotFound();  
            }
            catch (Exception e)
            {
                
                return BadRequest(e.Message);
            }

        }

        // POST: api/funcionario
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Funcionario funcionario)
        {
            try
            {
                _ctx.Funcionarios.Add(funcionario);
                _ctx.SaveChanges();
                return Created("", funcionario);               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // PUT: api/funcionario/{id}
        [HttpPut]
        [Route("alterar/{id}")]
        public IActionResult Alterar([FromRoute] int id, [FromBody] Funcionario funcionario)
        {
            try
            {
                Funcionario? funcionarioCadastrado =
                    _ctx.Funcionarios.FirstOrDefault(x => x.FuncionarioId == id);

                if (funcionarioCadastrado != null)
                {
                    funcionarioCadastrado.Nome = funcionario.Nome;
                    funcionarioCadastrado.Cpf = funcionario.Cpf;
                    _ctx.Funcionarios.Update(funcionarioCadastrado);
                    _ctx.SaveChanges();
                    return Ok();
                }
                return NotFound();                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // DELETE: api/funcionario/{id}
        [HttpDelete]
        [Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            try
            {
                Funcionario? funcionarioCadastrado = _ctx.Funcionarios.Find(id);
                if (funcionarioCadastrado != null)
                {
                    _ctx.Funcionarios.Remove(funcionarioCadastrado);
                    _ctx.SaveChanges();
                    return Ok();
                }
                return NotFound();                 
            }
            catch (Exception e)
            {  
                return BadRequest(e.Message);
            }

        }
    }
}
