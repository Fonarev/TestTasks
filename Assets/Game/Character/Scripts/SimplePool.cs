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
        GameObject newContainerObject;
        public SimplePool(T prefab, bool autoSize, int amount = 2, Transform container = null)
        {
            this.prefab = prefab;
            this.autoSize = autoSize;
            this.amount = amount;
            if(container == null)
            {
                newContainerObject = new GameObject("[Container]");
                this.container = newContainerObject.transform;
            }
           else
           {
                this.container = container;
           }
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
        
        public T Get(Transform parent, bool isActivat = true)
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
                list.Add(newObj);
                return newObj;
            }

            return null;
        }

        public void ReturnInPool(GameObject prefab)
        {
            prefab.SetActive(false);
            prefab.transform.SetParent(container,false);
        }

        public void ClierPool()
        {
            foreach (var item in list)
            {
                Object.Destroy(item.gameObject);
            }

            if(newContainerObject!= null)
            {
                Object.Destroy(newContainerObject);
            }
        }

    }
}