using Assets.Game.MiniGames.Scripts;
using Assets.Game.Scripts.UI;

using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.Character.Scripts
{
    public class GameManeger : MonoBehaviour
    {
        [SerializeField] private GlobalCanvas globalCanvas;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private List<MiniGame> miniGames;
        [SerializeField] private Cell cellPrefab;

        private static GameManeger gameManeger;
        private SimplePool<Cell> poolCell;

        public static GameManeger GameManager => gameManeger;
       

        private void Awake()=> gameManeger = this;
       
        private void Start()=> Init();

        private void Init()
        {
            poolCell = new(cellPrefab, true, 4);
            poolCell.Create();

            foreach (var game in miniGames)
            {
                game.Init(globalCanvas, poolCell);
                game.OnOpenedMiniGame += OnOpenMiniGame;
            }
        }
        
        private void OnOpenMiniGame(bool state)
        {
            playerController.CanMove(!state);
        }
    }
}