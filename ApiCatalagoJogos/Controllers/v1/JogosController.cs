using ApiCatalagoJogos.Exception;
using ApiCatalagoJogos.InputModel;
using ApiCatalagoJogos.Services;
using ApiCatalagoJogos.viewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;
        
        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>   
        [HttpGet]
       public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery,Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1,50)] int quantidade=5)
        {
            //throw new Exceptions();
            var jogos = await _jogoService.Obter(pagina, quantidade);

            if(jogos.Count == 0)
            
                return NoContent();
            return Ok(jogos);
        }

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>   

        [HttpGet("(idjogo:guid)")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idjogo)
        {
            var jogo = await _jogoService.Obter(idjogo);

            if (jogo == null)
                return NoContent();
            return Ok(jogo);
        }


        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromRoute] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInputModel);
                return Ok(jogo);
            }

            //catch(jogoJacadastradoException ex)
            catch (JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome e produtora");        
            }
        }

        [HttpPut("(idjogo:guid)")]
        public async Task<ActionResult> Atualizarjogo([FromQuery] Guid idjogo, [FromQuery] JogoInputModel jogoInputModel)
        {

            try
            {
                await _jogoService.Atualizar(idjogo, jogoInputModel);

                return Ok();
            }
            // catch(jogoNaoCadastradoException ex)
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpPatch("(idjogo:guid)/preco/preco:double")]
        public async Task<ActionResult> Atualizarjogo([FromQuery] Guid idjogo,[FromQuery] double preco)
        {
            try
            {
                await _jogoService.Atualizar(idjogo,preco);
                return Ok();
            }
           // catch (JogoNaoCadastradoException ex)
           catch(JogoNaoCadastradoException ex)
            {
                return NotFound("Não exizte este jogo");
            }
        }

        [HttpDelete("(idjogo:guid)")]
        public async Task<ActionResult>ApagarJogo([FromQuery]Guid idjogo)
        {
            try
            {
                await _jogoService.Remover(idjogo);
                return Ok();
            }
            // catch (JogoNaoCadastradoException ex)
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não exizte este jogo");
            }
        }
    }
}
