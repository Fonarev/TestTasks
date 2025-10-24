using Assets.Game.MiniGames.Scripts;

using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.Character.Scripts
{
    public class GameManeger : MonoBehaviour
    {
        public List<MiniGame> miniGames;
        public PlayerController playerController;

        void Start()
        {
            foreach (var game in miniGames)
            {
                game.Init();
                game.OnOpenedMiniGame += OnOpenMiniGame;
            }
        }

        private void OnOpenMiniGame(bool obj)
        {
            playerController.CanMove(!obj);
        }
    }
}