using UnityEngine;

namespace Assets.Game.Scripts.InputSystem
{
    public class KeyboardInputControl : IInputControl
    {
        private bool isEnabled;
        private IControllable controllable;

        public void Init(IControllable controllable)
        {
            this.controllable = controllable;

            if (controllable == null)
                Debug.LogError("[KeyboardInputControl] It is null during initialization" + controllable);
        }

        public void EnableControl()
        {
            isEnabled = true;
        }

        public void DisableControl()
        {
            isEnabled = false;
        }

        public void Update()
        {
            if (!isEnabled && controllable == null) return;

            ReadMovements();
            ReadMoveMouse();
            ReadJump();
            ReadSquats();
            ReadInteract();
           
        }

        private void ReadMovements()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 forward = controllable.Transform.TransformDirection(Vector3.forward);
            Vector3 right = controllable.Transform.TransformDirection(Vector3.right);

            Vector3 direction = (forward * verticalInput) + (right * horizontalInput);

            controllable.Move(direction);

        }

        private void ReadMoveMouse()
        {
            float vertical = -Input.GetAxis("Mouse Y");
            float horizontal = Input.GetAxis("Mouse X");

            controllable.MoveMouse(vertical, horizontal);
        }

        private void ReadJump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                controllable.Jump();
            }
        }

        private void ReadSquats()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                controllable.Jump();
            }
        }

        private void ReadInteract()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                controllable.Interact();
            }
        }

    }
}