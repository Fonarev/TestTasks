using System;

using UnityEngine;

namespace Assets.Game.Character.Scripts
{
    [Serializable]
    public class Settings
    {
        [SerializeField, Range(0.5f, 10)] private float speed = 5.5f;
        [SerializeField, Range(0.5f, 10)] private float jumpHeight = 5.5f;
        [SerializeField, Range(0.5f, 10)] private float jumpVilocity = 5.5f;
        [SerializeField, Range(8, 22)] private float gravity = 11;

        [SerializeField, Range(0.5f, 10)] private float lookVelocity = 5;
        [SerializeField, Range(0, 120)] private float lookLimit_X = 60;
        [SerializeField, Range(0, 120)] private float lookLimit_Y = 60;
        [SerializeField, Range(1, 10)] private float returnSpeedCamera = 5;
        
        public float Speed => speed;
        public float JumpHeight => jumpHeight;
        public float JumpVelocity => jumpVilocity;
        public float Gravity => gravity;

        public float LookVelosity => lookVelocity;
        public float LookLimit_X => lookLimit_X;
        public float LookLimit_Y => lookLimit_Y;
        public float ReturnSpeedCamera => returnSpeedCamera;
       
    }
}