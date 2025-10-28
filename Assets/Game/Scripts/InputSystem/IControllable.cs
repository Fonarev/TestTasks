using UnityEngine;

namespace Assets.Game.Scripts.InputSystem
{
    public interface IControllable 
    {
        Transform Transform{ get; }
        void Move(Vector3 direction);
        void MoveMouse(float vertical, float horizontal);
        void Jump();
        void Squats();
        void Interact();

    }
}