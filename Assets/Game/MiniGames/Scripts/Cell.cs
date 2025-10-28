using System;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Game.MiniGames.Scripts
{
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        public Action<int, bool> OnClick;
        public Image tile;
        public TextMeshProUGUI text;
        private int id;
        private bool presset;
        public string Con { get; private set; }

        public void Init(string content,Action<int, bool> onClick)
        {
            Con = content;
            text.text = content;
            presset = false;
            tile.transform.localScale = new Vector3(1f, 1f, 1f);
            OnClick = onClick;
        }

        public void Res()
        {
            presset = false;
            tile.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void SetID(int id) => this.id = id;
      
        public void OnPointerClick(PointerEventData eventData)
        {
            presset = presset != true;
            tile.transform.localScale = presset == true ? new Vector3(1.3f, 1.3f, 1.3f) : new Vector3(1f, 1f, 1f);

            OnClick?.Invoke(id, presset);
        }
    }
}