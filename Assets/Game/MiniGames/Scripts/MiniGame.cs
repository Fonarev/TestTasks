using Assets.Game.Character.Scripts;
using Assets.Game.Character.Scripts.Interacters;
using Assets.Game.Scripts;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.MiniGames.Scripts
{
    public class MiniGame : MonoBehaviour, IInteracter, IMiniGame
    {
        public event Action<bool> OnOpenedMiniGame;

        [SerializeField] private bool randomGetContent;
        [SerializeField, Range(3, 10)] private int levelTask;
        [SerializeField] private MiniGameType gameType;

        [SerializeField] private MiniGameDatabase data;
        [SerializeField] private GameBoard gameBoard;
       
        [SerializeField] private InteractZone interactZone;
        [SerializeField] private Material materialTargetGraphic;

        private bool isOpenGame;
        private int matching;
        private int order;
        private Color colorTarget;
        private IContainer container;
        private GameBoard gameBoardUI;
        private SimplePool<Cell> poolCell;

        private int Order
        {
            get => order;
            set
            {
                order = value;
                if (order < 0) order = 0;

                if (Order < Matching)
                    Matching --;
            }
        }

        private int Matching
        {
            get => matching;
            set
            {
                matching = value;

                if (matching < 0)
                    matching = 0;
            }
        }

        private bool IsOpenGame
        {
            get => isOpenGame;
            set
            {
                isOpenGame = value;
                OnOpenedMiniGame?.Invoke(isOpenGame);
            }
        }

        public void Init(IContainer container, SimplePool<Cell> poolCell)
        {
            this.poolCell = poolCell;
            this.container = container;

            data.Init();

            interactZone.Init(this);

            colorTarget = materialTargetGraphic.color;
        }

        public void InvokeIneract()
        {
            if (!IsOpenGame)
            {
                if (gameBoardUI == null)
                {
                    gameBoardUI = Instantiate(gameBoard, container.Parent, false);
                    gameBoardUI.Init(this, data.GetSettingsData(gameType), poolCell);
                }
                else
                {
                    if (gameBoardUI.gameObject != null && !gameBoardUI.gameObject.activeSelf)
                        gameBoardUI.gameObject.SetActive(true);
                }

                gameBoardUI.OpenBoard(GetNewData());

                IsOpenGame = true;

                materialTargetGraphic.color = colorTarget;
            }
        }

        public void Restart()
        {
            Matching = 0;
            Order = 0;
            gameBoardUI.Restart(GetNewData());
        }

        public void Exit()
        {
            Resume();
        }

        public void ConditionsCheck(int id, bool presset)
        {
            if (presset)
            {
                if (id == Order)
                {
                    Matching++;
                }
                Order++;
            }
            else
            {
                if (Matching == id && id == Order)
                {
                    Matching--;
                }
                Order--;
            }

            if (Order >= levelTask)
            {
                if (Matching >= levelTask)
                {
                    Resume();
                }
                else
                {
                    Restart();
                }
            }
        }

        public void OnZoneEnter(PlayerController player)
        {
            player.CanInteract(this, true);
            materialTargetGraphic.color = Color.green;
        }

        public void OnZoneExit(PlayerController player)
        {
            player.CanInteract(null, false);
            materialTargetGraphic.color = colorTarget;
        }

        private void Resume()
        {
            Matching = 0;
            Order = 0;

            if(gameBoardUI.gameObject != null && gameBoardUI.gameObject.activeSelf)
               gameBoardUI.gameObject.SetActive(false);

            IsOpenGame = false;
        }

        private List<string> GetNewData()
        {
            return data.GetContent(gameType, levelTask, randomGetContent);
        }

        private void OnDisable()
        {
            materialTargetGraphic.color = colorTarget;
        }

    }
}