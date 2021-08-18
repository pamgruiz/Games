using Games.Entities;
using Games.Exceptions;
using Games.InputModel;
using Games.Repositories;
using Games.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }



        public async Task<List<GameViewModel>> Select(int page, int quantity)
        {
            var games = await _gameRepository.Select(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producter = game.Producter,
                Price = game.Price
            }).ToList();

        }


        public async Task<GameViewModel> SelectById(Guid id)
        {
            var game = await _gameRepository.SelectById(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producter = game.Producter,
                Price = game.Price
            };
        }



        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var entityGame = await _gameRepository.Select(game.Name, game.Producter);

            if (entityGame.Count > 0)
                throw new GameAlreadyExistsException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producter = game.Producter,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Producter = game.Producter,
                Price = game.Price
            };
        }



        public async Task Update(Guid id, GameInputModel game)
        {
            var entityGame = await _gameRepository.SelectById(id);

            if(entityGame == null)
                throw new GameNotFoundException();

            entityGame.Name = game.Name;
            entityGame.Producter = game.Producter;
            entityGame.Price = game.Price;

            await _gameRepository.Update(id, entityGame);
        }




        public async Task UpdatePrice(Guid id, double price)
        {
            var entityGame = await _gameRepository.SelectById(id);

            if (entityGame == null)
                throw new GameNotFoundException();

            entityGame.Price = price;

            await _gameRepository.Update(id, entityGame);
        }



        public async Task Remove(Guid id)
        {
            var entityGame = await _gameRepository.SelectById(id);

            if (entityGame == null)
                throw new GameNotFoundException();

            await _gameRepository.Remove(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();   //Fecha conexões, ajuda no controle de requisicoes
        }
    }
}
