using UnityEngine;

namespace Assets.Game.Character.Scripts
{
    [CreateAssetMenu(fileName ="character",menuName ="test")]
    public class CharacterSettingData : ScriptableObject
    {
        [SerializeField, Range(0.5f, 10)] private float speed;
        [SerializeField, Range(0.5f, 10)] private float jumpHeight;
        [SerializeField, Range(0.5f, 10)] private float jumpVilocity;

        [SerializeField, Range(0.5f, 10)] private float lookSpeed;
        [SerializeField, Range(10, 120)] private float lookLimit;
        [SerializeField, Range(1, 10)] private float returnSpeedCamera;
        [SerializeField, Range(8, 22)] private float gravity;
        public float Speed => speed;
        public float JumpHeight => jumpHeight;
        public float JumpVelocity => jumpVilocity;
        public float LookSpeed => lookSpeed;
        public float LookLimit => lookLimit;
        public float ReturnSpeedCamera => returnSpeedCamera;
        public float Gravity => gravity;
    }
}