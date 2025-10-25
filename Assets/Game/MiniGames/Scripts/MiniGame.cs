using Assets.Game.Character.Scripts;
using Assets.Game.Character.Scripts.Interacters;
using Assets.Game.Scripts;

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
        
        [SerializeField] private MiniGameDatabase data;
        [SerializeField] private GameBoard gameBoard;
        [SerializeField] private InteractZone interactZone;
        [SerializeField] private Material materialTargetGraphic;

        private Color colorTarget;
        private int conditionsOrder;
        private int order;
        private List<string> contentTasks = new();
        private Dictionary<string, int> map = new();
        private IContainer container;
        private GameBoard gameBoardUI;

        public MiniGameType GameType => gameType;
        public int LevelTask => levelTask;

        public void Init(IContainer container)
        {
            this.container = container;
            data.Init();
            interactZone.Init(this);
            SetNewData();
           
            colorTarget = materialTargetGraphic.color;
        }

        public void InvokeIneract()
        {
            SetNewData();

            if(gameBoardUI == null)
            {
                gameBoardUI = Instantiate(gameBoard, container.Parent, false);
                gameBoardUI.Init(data.GetSettingsData(gameType));
                gameBoardUI.OnPressedCell += ConditionsCheck;
            }
           
            gameBoardUI.gameObject.SetActive(true);
            gameBoardUI.OpenBoard(contentTasks, Exit);

            OnOpenedMiniGame?.Invoke(true);

            materialTargetGraphic.color = colorTarget;
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

        private void Restart()
        {
            conditionsOrder = 0;
            order = 0;
            SetNewData();
            gameBoardUI.Restart(contentTasks);
        }

        private void Exit()
        {
            conditionsOrder = 0;
            order = 0;
            gameBoardUI.gameObject.SetActive(false);
            OnOpenedMiniGame?.Invoke(false);
        }

        private void ConditionsCheck(bool presset, string id)
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
                    gameBoardUI.gameObject.SetActive(false);
                    OnOpenedMiniGame?.Invoke(false);
                }
                else
                {
                    Restart();
                }

            }
        }

        private void SetNewData()
        {
            contentTasks.Clear();
            contentTasks = data.GetContent(gameType, levelTask, randomGetContent);

            map.Clear();
            for (int i = 0; i < contentTasks.Count; i++) map[contentTasks[i]] = i;
        }

        private void OnDisable()
        {
            materialTargetGraphic.color = colorTarget;
            if(gameBoardUI!= null) gameBoardUI.OnPressedCell -= ConditionsCheck;
        }

    }
}