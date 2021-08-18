using Games.InputModel;
using Games.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Services
{
    public interface IGameService
    {
        Task<List<GameViewModel>> Select(int page, int quantity);
        Task<GameViewModel> SelectById(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task UpdatePrice(Guid id, double price);
        Task Remove(Guid id);
        //Task Update(Guid idGame, double price);
    }
}
