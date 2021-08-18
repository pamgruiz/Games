using Games.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Repositories
{
    public interface IGameRepository : IDisposable //IDisposable permite que eu consiga controlar a destruicao do objeto
    {
        Task<List<Game>> Select(int page, int quantity);
        Task<Game> SelectById(Guid id);
        Task<List<Game>> Select(string name, string producter);
        Task Insert(Game game);
        Task Update(Guid id, Game game);
        Task UpdatePrice(Guid id, double price);
        Task Remove(Guid id);
    }
}
