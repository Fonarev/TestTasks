using System;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Game.MiniGames.Scripts
{
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        public event Action<bool, string> OnPresseted;
        public Image tile;
        public TextMeshProUGUI text;
        private string id;
        private bool presset;

        public void Set(string id)
        {
            this.id = id;
            text.text = id;
            presset = false;
            tile.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            presset = presset == true ? false : true;
            tile.transform.localScale = presset == true ? new Vector3(1.3f, 1.3f, 1.3f) : new Vector3(1f, 1f, 1f);
            
            OnPresseted?.Invoke(presset, id);
        }
    }
}