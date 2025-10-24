using System;

using UnityEngine;

namespace Assets.Game.MiniGames.Scripts
{
    [Serializable]
    public class SettingsGameData
    {
        [SerializeField] private MiniGameType gameType;
        [SerializeField, Range(3, 9)] private int levelTask;
       
        [SerializeField] private MiniGameData data;
        public MiniGameType GameType => gameType;
        public int LevelTask => levelTask;
    }
}