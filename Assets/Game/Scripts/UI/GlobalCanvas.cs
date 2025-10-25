using Assets.Game.MiniGames.Scripts;

using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class GlobalCanvas : MonoBehaviour, IContainer
    {
        [SerializeField] private Transform miniGameContainer;
        [SerializeField] private Transform popupContainer;
        [SerializeField] private IGameBoardUI gameBoard;

       
        private Dictionary<string, IGameBoardUI> screens = new();

        public Transform Parent => miniGameContainer;
    }
}