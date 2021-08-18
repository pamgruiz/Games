using Games.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("d077a0e7-1b0c-4611-996f-5bf9a656fc0b"), new Game{Id = Guid.Parse("d077a0e7-1b0c-4611-996f-5bf9a656fc0b"), Name = "PowerPuff Girls", Producter = "CN", Price = 100} },
            {Guid.Parse("41e67129-b137-47c3-9950-661d3821b55c"), new Game{Id = Guid.Parse("41e67129-b137-47c3-9950-661d3821b55c"), Name = "Fruit Ninja", Producter = "Apple Store", Price = 30} },
            {Guid.Parse("2e3fd0bb-5542-4331-9906-6ac9a340de3f"), new Game{Id = Guid.Parse("2e3fd0bb-5542-4331-9906-6ac9a340de3f"), Name = "Duolingo", Producter = "Duolingo Cia.", Price = 189} },
            {Guid.Parse("2cfca12d-8907-4018-981d-726d113a628d"), new Game{Id = Guid.Parse("2cfca12d-8907-4018-981d-726d113a628d"), Name = "Wizard World Harry Potter", Producter = "Universal", Price = 1000} }
        };

        public Task<List<Game>> Select(int page, int quantity)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }


        public Task<Game> SelectById(Guid id)
        {
        if (!games.ContainsKey(id))
            return null;

        return Task.FromResult(games[id]);

        }

        public Task<List<Game>> Select(string name, string producter)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Producter.Equals(producter)).ToList());
        }

        public Task<List<Game>> SelectNoLambda(string name, string producter)
        {
            var back = new List<Game>();

            foreach (var game in games.Values)
            {
                if (game.Name.Equals(name) && game.Producter.Equals(producter))
                    back.Add(game);
            }
            return Task.FromResult(back);
        }



        public Task Insert(Game game)
        {
            games.Add(game.Id, game);
            return (Task<Game>)Task.CompletedTask;
        }


        public Task Update(Guid id, Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task UpdatePrice(Guid id, double price)
        {
            //games[game.Id] = game;
            //games[price] = price;
            //return Task.CompletedTask;
            return Task.CompletedTask;
        }

        public Task Remove(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fecha conexao
        }
    }
}
