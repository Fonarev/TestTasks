using Assets.Game.MiniGames.Scripts;
using Assets.Game.Scripts.UI;

using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.Character.Scripts
{
    public class GameManeger : MonoBehaviour
    {
        [SerializeField] private IInputPlayer inputPlayer;
        [SerializeField] private GlobalCanvas globalCanvas;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private List<MiniGame> miniGames;

        private static GameManeger gameManeger;
        
        public static GameManeger GameManager => gameManeger;

        private void Awake()=> gameManeger = this;
       
        private void Start()=> Init();

        private void Update()
        {
            if(inputPlayer!= null) inputPlayer.UpData();

        }

        private void Init()
        {
            playerController.Init(inputPlayer);

            foreach (var game in miniGames)
            {
                game.Init(globalCanvas);
                game.OnOpenedMiniGame += OnOpenMiniGame;
            }
        }
        
        private void OnOpenMiniGame(bool state)
        {
            playerController.CanMove(!state);
        }
    }
}