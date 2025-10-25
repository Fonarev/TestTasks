using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.MiniGames.Scripts
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Test/SettingsData", order =1)]
    public class SettingsMiniGameData : ScriptableObject
    {
        [SerializeField] private MiniGameType gameType;
        [SerializeField] private List<string> content;
        [SerializeField] private Vector2 cellSize = new(150, 50);
        [SerializeField] private GridLayoutGroup.Axis startAxis = GridLayoutGroup.Axis.Horizontal;

        public MiniGameType GameType => gameType;
        public List<string> Content => content;
        public Vector2 CellSize => cellSize;
        public GridLayoutGroup.Axis StartAxis => startAxis;
    }
}