using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.MiniGames.Scripts
{
    [CreateAssetMenu(fileName ="MiniGameData",menuName ="test")]
    public class MiniGameData : ScriptableObject
    {
        [SerializeField] private List<string> numbers;
        [SerializeField] private List<string> Words;
        Dictionary<MiniGameType, List<string>> contentMap;

        public void Init()
        {
            contentMap = new();
            contentMap[MiniGameType.NumbersOrder] = numbers;
            contentMap[MiniGameType.WordsOrder] = Words;
        }

        public List<string> GetContent(MiniGameType type, int levelTask)
        {
            List<string> contentList = new();
            if (contentMap.TryGetValue(type, out List<string> content))
            {
                for (int i = 0; i < levelTask; i++)
                {
                    contentList.Add(content[i]);
                }
            }

            return contentList;
        }

        public List<string> GetRandomContent(MiniGameType type, int levelTask)
        {
            List<string> newContent = new();

            if (contentMap.TryGetValue(type, out List<string> content))
            {

                List<string> contentList = new();
               
                foreach (var con in content) contentList.Add(con);
                
                for (int i = 0; i < levelTask; i++)
                {
                    var randomIndex = Random.Range(0, contentList.Count);
                    newContent.Add(contentList[randomIndex]);
                    contentList.RemoveAt(randomIndex);
                }

                newContent.Sort();
            }

            return newContent;
        }
    }
}