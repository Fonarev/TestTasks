using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.MiniGames.Scripts
{
    [CreateAssetMenu(fileName = "Database", menuName = "Test/Database")]
    public class MiniGameDatabase : ScriptableObject
    {
        [SerializeField] private List<SettingsMiniGameData> settingsData;

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
            List<string> newContent = new();

            if (settigsLinks.TryGetValue(type, out SettingsMiniGameData settingsData))
            {
                List<string> content = settingsData.Content;

                if (isRandom)
                {
                    List<string> dynamicList = new();

                    foreach (var con in content) dynamicList.Add(con);

                    for (int i = 0; i < amount; i++)
                    {
                        var randomIndex = Random.Range(0, dynamicList.Count);
                        newContent.Add(dynamicList[randomIndex]);
                        dynamicList.RemoveAt(randomIndex);
                    }

                    newContent.Sort();
                }
                else
                {
                    for (int i = 0; i < amount; i++)
                    {
                        newContent.Add(content[i]);
                    }
                }

               
            }
            else
            {
                Debug.LogError("[MiniGameData] No settings data!" + type + "  is not found");
            }

            return newContent;
        }
      
    }
}