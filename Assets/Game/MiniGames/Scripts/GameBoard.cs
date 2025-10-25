using Assets.Game.Character.Scripts;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

namespace Assets.Game.MiniGames.Scripts
{
    public class GameBoard : MonoBehaviour
    {
        public event Action OnClickRestart;
        public event Action<bool,string> OnPressedCell;

        [SerializeField] private Cell cellPrefab;
        [SerializeField] private GridLayoutGroup group;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonExit;

        private List<Cell> cells;
        private List<string> contentTasks = new();
        private SimplePool<Cell> poolCell;
        private Action OnExit;

        public void Init(SettingsMiniGameData data)
        {
            group.cellSize = data.CellSize;
            group.startAxis = data.StartAxis;
            poolCell = new(cellPrefab, true, 5);
            poolCell.Create();

            buttonRestart.onClick?.AddListener(() => OnClickRestart?.Invoke());
            buttonExit.onClick?.AddListener(() => OnExit?.Invoke());
        }

        public void OpenBoard(List<string> list, Action onExit)
        {
            cells = new();
            contentTasks.Clear();

            foreach (var item in list) contentTasks.Add(item);

            for (int i = 0; i < list.Count; i++)
            {
                Cell cell = poolCell.Get(cellPrefab, group.transform,false);

                var random = Random.Range(0, contentTasks.Count);
             
                cell.Set(contentTasks[random]);
                cell.OnPresseted += (presset,id) => OnPressedCell?.Invoke(presset,id);
                cell.gameObject.SetActive(true);
                cells.Add(cell);
                contentTasks.RemoveAt(random);
            }

            OnExit = onExit;
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
                    cell.OnPresseted -= (presset, id) => OnPressedCell?.Invoke(presset, id);
                    poolCell.ReturnInPool(cell.gameObject);
                }
                cells = null;
            }

        }
    }
}