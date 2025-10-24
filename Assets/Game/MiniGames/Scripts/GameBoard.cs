using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.MiniGames.Scripts
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private Cell cellPrefab;
        [SerializeField] private GridLayoutGroup group;
        [SerializeField] private Button buttonRestart;

        private List<Cell> cells;
        private List<string> contentTasks = new();
        private MiniGame miniGame;

        public void Init(MiniGame miniGame)
        {
            this.miniGame = miniGame;

            if(miniGame.GameType == MiniGameType.WordsOrder)
            {
                group.cellSize = new Vector2(150, 50);
                group.startAxis = GridLayoutGroup.Axis.Vertical;
            }

            buttonRestart.onClick.AddListener(() => miniGame.Restart());
        }

        public void OpenBoard(List<string> list)
        {
            cells = new();
            contentTasks.Clear();

            foreach (var item in list) contentTasks.Add(item);

            for (int i = 0; i < miniGame.LevelTask; i++)
            {
                Cell cell = Instantiate(cellPrefab, group.gameObject.transform);

                var random = Random.Range(0, contentTasks.Count);
             
                cell.Set(contentTasks[random]);
                cell.OnPresseted += (presset,id) => miniGame.ConditionsCheck(presset,id);
                cells.Add(cell);
                contentTasks.RemoveAt(random);
            }
           
        }

        public void Restart(List<string> list)
        {
            ResetList(list);

            for (int i = 0; i < cells.Count; i++)
            {
                Cell cell = cells[i];
               
                var random = Random.Range(0, contentTasks.Count);
                cell.Set(contentTasks[random]);
                contentTasks.RemoveAt(random);
            }
        }

        private void ResetList(List<string> list)
        {
            contentTasks.Clear();
            foreach (var item in list) contentTasks.Add(item);
        }

        private void OnDisable()
        {
            if (cells.Count > 0)
            {
                foreach (var cell in cells)
                {
                    cell.OnPresseted -= (presset, id) => miniGame.ConditionsCheck(presset, id);
                    Destroy(cell.gameObject);
                }
                cells = null;
            }

        }
    }
}