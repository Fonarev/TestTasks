using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.Scripts
{
    public static class Randomer 
    {
        public static List<string> GetNonrepeatingItems(List<string> content, int amount)
        {
            List<string> newList = new();

            for (int i = 0; i < amount; i++)
            {
                int indexRandom = Random.Range(0, content.Count);
                Check(indexRandom, content, newList);
            }
          
            return newList;
        }

        private static void Check(int random, List<string> content, List<string> newList)
        {
            var item = content[random];

            if (newList.Contains(item))
            {
                random++;

                if (random >= 0 && random < content.Count)
                {
                    Check(random, content, newList);
                }
                else
                {
                    random = 0;
                    Check(random, content, newList);
                }

            }
            else
            {
                newList.Add(item);
            }
        }

    }
}