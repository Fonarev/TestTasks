
using System.Collections.Generic;

namespace Assets.Game.MiniGames.Scripts
{
    public interface IGameBoardUI 
    {
        void Init();
        void OpenBoard(List<string> list);
        void Restart(List<string> list);
    }
}