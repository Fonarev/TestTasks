using Assets.Game.Character.Scripts;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.MiniGames.Scripts
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup group;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonExit;

        private IMiniGame miniGame;
        private List<Cell> cells = new();
        private SimplePool<Cell> poolCell;
      
        public void Init(IMiniGame miniGame, SettingsMiniGameData data, SimplePool<Cell> poolCell)
        {
            this.miniGame = miniGame;
            this.poolCell = poolCell;

            group.cellSize = data.CellSize;
            group.startAxis = data.StartAxis;
        }

        public void OpenBoard(List<string> content)
        {
            for (int i = 0; i < content.Count; i++)
            {
                Cell cell = poolCell.Get( group.transform);
                cell.Init(content[i], miniGame.ConditionsCheck);
                cells.Add(cell);
            }

            content.Sort();

            var cellIndices = new HashSet<int>();

            for (int i = 0; i < content.Count; i++)
            {
                var item = content[i];
                for (int j = 0; j < cells.Count; j++)
                {
                    if (!cellIndices.Contains(j) && item == cells[j].Con)
                    {
                        cells[j].SetID(i);
                        cellIndices.Add(j);
                        break;
                    }
                }
            }
        }

        public void Restart(List<string> content)
        {
            Resume();
            OpenBoard(content);
        }

        private void OnClickRestart()
        {
            miniGame.Restart();
        }

        private void OnClickExit()
        {
            miniGame.Exit();
        }

        private void Resume()
        {
            if (cells.Count > 0)
            {
                foreach (var cell in cells)
                {
                    poolCell.ReturnInPool(cell.gameObject);
                    cells.Remove(cell);
                }
            }
        }

        private void OnEnable()
        {
            buttonRestart.onClick?.AddListener(OnClickRestart);
            buttonExit.onClick?.AddListener(OnClickExit);
        }

        private void OnDisable()
        {
            Resume();

            buttonRestart.onClick?.RemoveAllListeners();
            buttonExit.onClick?.RemoveAllListeners();
        }

    }
}