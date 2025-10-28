using Assets.Game.Scripts;

using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.MiniGames.Scripts
{
    [CreateAssetMenu(fileName = "Database", menuName = "Test/Database")]
    public class MiniGameDatabase : ScriptableObject
    {
        [SerializeField] private SettingsMiniGameData[] settingsData;

        Dictionary<MiniGameType, SettingsMiniGameData> settigsLinks = new();

        public void Init()
        {
            foreach (var data in settingsData)
            {
                settigsLinks[data.GameType] = data;
            }
        }

        public SettingsMiniGameData GetSettingsData(MiniGameType type)
        {
            if (settigsLinks.TryGetValue(type, out SettingsMiniGameData settingsData))
            {
                return settingsData;
            }

            Debug.LogError("[MiniGameData] No settings data!" + type + "  is not found");

            return null;
        }

        public List<string> GetContent(MiniGameType type, int amount, bool isRandom = false)
        {
            if (settigsLinks.TryGetValue(type, out SettingsMiniGameData settingsData))
            {
                if (isRandom)
                {
                    return Randomer.GetNonrepeatingItems(settingsData.Content, amount);
                }
                else
                {
                    return settingsData.Content.GetRange(0, amount);
                }

            }
            else
            {
                Debug.LogError("[MiniGameData] No settings data!" + type + "  is not found");
            }

            return null;
        }
      
    }
}