using UnityEngine;

namespace Assets.Game.Character.Scripts.Interacters
{
    public class InteractZone : MonoBehaviour, IInteracterZone
    {
        private IInteracter interacter;

        public void Init(IInteracter interacter)
        {
            this.interacter = interacter;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
               interacter.OnZoneEnter(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                interacter.OnZoneExit(player);
            }
        }
      
    }
}