
using FolhaDePagamento.Models;
using FolhaDePagamentoAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FolhaDePagamentoAPI.Controllers;

[ApiController]
[Route("api/folha")]
public class FolhaController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public FolhaController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    //GET: api/folha/listar
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Folha> folhas =
                _ctx.Folhas
                .Include(x => x.Funcionario)
                .ToList();
            return folhas.Count == 0 ? NotFound() : Ok(folhas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Folha folha)
    {
        try
        {
            Funcionario? funcionario = _ctx.Funcionarios.Find(folha.FuncionarioId);
            if (funcionario == null)
            {
                return NotFound("Cliente não encontrado.");
            }

                folha.Funcionario = funcionario;
                _ctx.Folhas.Add(folha);
                _ctx.SaveChanges();
                return Created("", folha);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("buscar/{cpf}/{mes}/{ano}")]
    public IActionResult Buscar([FromRoute] string cpf, int mes, int ano)
    {
        try
        {
            Folha? folhaCadastrada =
                _ctx.Folhas
                .Include(x => x.Funcionario)
                .FirstOrDefault(x => x.Funcionario!.Cpf == cpf && x.Mes == mes && x.Ano == ano);
            if (folhaCadastrada != null)
            {
                return Ok(folhaCadastrada);
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



}