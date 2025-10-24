using Assets.Game.Character.Scripts;
using Assets.Game.Character.Scripts.Interacters;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.MiniGames.Scripts
{
    public class MiniGame : MonoBehaviour, IInteracter
    {
        public event Action<bool> OnOpenedMiniGame;

        [SerializeField] private bool randomGetContent;
        [SerializeField, Range(3, 10)] private int levelTask;
        [SerializeField] private MiniGameType gameType;
        
        [SerializeField] private MiniGameData data;
        [SerializeField] private GameBoard gameBoard;
        [SerializeField] private InteractZone interactZone;
        [SerializeField] private Material materialTargetGraphic;

        private Color colorTarget;
        private int conditionsOrder;
        private int order;
        private List<string> contentTasks = new();
        private Dictionary<string, int> map = new();

        public MiniGameType GameType => gameType;
        public int LevelTask => levelTask;

        public void Init()
        {
            data.Init();
            interactZone.Init(this);
            SetNewData();
            gameBoard.Init(this);
            gameBoard.gameObject.SetActive(false);
            colorTarget = materialTargetGraphic.color;
        }

        public void ConditionsCheck(bool presset, string id)
        {
            if (presset)
            {
                if (map[id] == order)
                {
                    conditionsOrder++;
                }
                order++;
            }
            else
            {
                order--;
            }

            if (order >= levelTask)
            {
                if (conditionsOrder >= levelTask)
                {
                    conditionsOrder = 0;
                    order = 0;
                    gameBoard.gameObject.SetActive(false);
                    OnOpenedMiniGame ?.Invoke(false);
                   
                }
                else
                {
                    Restart();
                }
               
            }
        }

        public void Restart()
        {
            conditionsOrder = 0;
            order = 0;
            SetNewData();
            gameBoard.Restart(contentTasks);
        }

        public void InvokeIneract()
        {
            SetNewData();
            gameBoard.gameObject.SetActive(true);
            gameBoard.OpenBoard(contentTasks);
            materialTargetGraphic.color = colorTarget;
            OnOpenedMiniGame?.Invoke(true);
        }

        public void OnZoneEnter(PlayerController player)
        {
            player.Interacted(this, true);
            materialTargetGraphic.color = Color.green;
        }

        public void OnZoneExit(PlayerController player)
        {
            player.Interacted(null, false);
            materialTargetGraphic.color = colorTarget;
        }

        private void SetNewData()
        {
            contentTasks.Clear();
            contentTasks = randomGetContent? data.GetRandomContent(gameType, levelTask):data.GetContent(gameType, levelTask);

            map.Clear();
            for (int i = 0; i < contentTasks.Count; i++) map[contentTasks[i]] = i;
        }

        private void OnDisable()
        {
            materialTargetGraphic.color = colorTarget;
        }

    }
}