using Assets.Game.Character.Scripts.Interacters;
using Assets.Game.Scripts.InputSystem;

using UnityEngine;

namespace Assets.Game.Character.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IControllable
    {
        [SerializeField] private Settings settings;
        [SerializeField] private InputController inputController;
        [SerializeField] private GameObject hint;
       
        private bool Moving;
        public float speed;

        private float rotationX = 0;
        private float rotationY = 0;

        private float InstallFOV;
        private Vector3 moveDirection = Vector3.zero;
        private float movementDirectionY;
        private CharacterController characterController;
        private Camera cam;
        private IInteracter interacter;
        private bool canInteract;

        public float acceleration = 6;
        public float maxSpeed = 12;

        public Transform Transform => transform;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            cam = GetComponentInChildren<Camera>();

            InstallFOV = cam.fieldOfView;
        }

        public void Move(Vector3 direction)
        {
            moveDirection = direction;
            characterController.Move(settings.Speed * Time.deltaTime * moveDirection);
        }

        public void MoveMouse(float vertical, float horizontal)
        {
            rotationX += vertical * settings.LookVelosity;
            rotationX = Mathf.Clamp(rotationX, -settings.LookLimit_X, settings.LookLimit_X);
            cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, horizontal * settings.LookVelosity, 0);

            //rotationY += vertical * settings.LookVelosity;
            //rotationY = Mathf.Clamp(rotationY, -settings.LookLimit_Y, settings.LookLimit_Y);
            //cam.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

            if (Moving)
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, settings.ReturnSpeedCamera * Time.deltaTime);
        }

        public void Jump()
        {
            if (!characterController.isGrounded)
                moveDirection.y -= settings.Gravity * Time.deltaTime;

            var movementDirectionY = moveDirection.y;

            if (characterController.isGrounded)
                moveDirection.y = settings.JumpHeight;
            else
                moveDirection.y = movementDirectionY;
        }

        public void Squats()
        {

        }

        public void Interact()
        {
            if (canInteract && interacter != null)
            {
                interacter.InvokeIneract();
                hint.SetActive(false);
            }
        }

        public void CanInteract(IInteracter interacter, bool canInteract)
        {
            hint.SetActive(canInteract);
            this.interacter = interacter;
            this.canInteract = canInteract;
        }

        public void CanMove(bool isEnable)
        {
            inputController.Switch(isEnable);
        }
       
    }
}
