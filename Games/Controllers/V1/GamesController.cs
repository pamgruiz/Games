using Games.Exceptions;
using Games.InputModel;
using Games.Services;
using Games.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <param name="page">Indica qual pagina esta sendo consultada. Minimo 1 </param> 
        /// <param name="quantity">Indica a quantidade de registros por pagina. Min1 e Max 50 </param>
        /// <responde>code="200" OK</responde>
        /// <responde>code="400" Not Found</responde>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> Select([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)    
        //Usando task porque garante uma melhor perfomance falando de requisicao web, async espera um actionresult list
        {
            var games = await _gameService.Select(page, quantity);   //usando await porque estou falando de uma task

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }


        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> SelectById([FromRoute] Guid idGame)    //Guid      -    Obs: object eu substituo pelo GameViewModel
        {
            var games = await _gameService.SelectById(idGame);
            // var result = await _gameService.SelectById
            if (games == null)
                return NoContent();

            return Ok(games);
        }


        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertNewGame([FromBody]GameInputModel gameInputModel)    //object eu substituo pela GameInputModel
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);
                return Ok(game);
            }
            catch (GameAlreadyExistsException ex)
            {
                return UnprocessableEntity("This game with this producter alreay exists in catalog");
            }
        }


        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> Update([FromRoute]Guid idGame, [FromBody] GameInputModel gameInputModel)    
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);
                return Ok();
            }
            catch (GameNotFoundException ex)
            {
                return NotFound("This game don't exist");
            }
        }


        [HttpPatch("{idGame:guid}/price/{price:double}")]   //Existe um pacote da Microsoft chamado Json Patch - estudar (Mas o patch atualiza uma parte do recurso apenas, e o Put o recurso todo)
        public async Task<ActionResult> UpdatePrice([FromRoute]Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.UpdatePrice(idGame, price);
                return Ok();
            }
            catch (GameNotFoundException ex)
            {
                return NotFound("This game don't exist");
            }
        }


        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Remove(idGame);
                return Ok();
            }
            catch (GameNotFoundException ex)
            {
                return NotFound("This game don't exist");
            }
        }
    }
}
