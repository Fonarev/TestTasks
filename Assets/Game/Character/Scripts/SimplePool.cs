using System.Collections.Generic;

using UnityEngine;

namespace Assets.Game.Character.Scripts
{
    public class SimplePool<T>where T : MonoBehaviour
    {
        int amount;
        T prefab;
        Transform container;
        List<T> list = new();
        bool autoSize;

        public SimplePool(T prefab, bool autoSize, int amount = 2, Transform container = null)
        {
            this.prefab = prefab;
            this.autoSize = autoSize;
            this.amount = amount;
            this.container = container == null?  new GameObject("[Container]").transform : container;
        }

        public void Create()
        {
            for (int i = 0; i < amount; i++)
            {
                var prefabCreated = Object.Instantiate(prefab, container, false);
                prefabCreated.gameObject.SetActive(false);
                list.Add(prefabCreated);
            }
        }
        
        public T Get(T prefab, Transform parent, bool isActivat = true)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var obj = list[i];

                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.transform.SetParent(parent, false);
                    obj.gameObject.SetActive(isActivat);
                    return obj;
                }
            }

            if (autoSize)
            {
                var newObj = Object.Instantiate(prefab, parent, false);
                newObj.gameObject.SetActive(isActivat);

                return newObj;
            }

            return null;
        }

        public void ReturnInPool(GameObject prefab)
        {
            prefab.SetActive(false);
            prefab.transform.SetParent(container,false);
        }

    }
}