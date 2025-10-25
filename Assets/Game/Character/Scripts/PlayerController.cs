using Assets.Game.Character.Scripts.Interacters;

using UnityEngine;

namespace Assets.Game.Character.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterSettingData data;
        [SerializeField] private GameObject hint;

        private IInputPlayer inputPlayer;
        private bool canMove;
        private bool Moving;
        private float vertical;
        private float horizontal;
        private float Lookvertical;
        private float Lookhorizontal;
        private float rotationX = 0;

        private float InstallFOV;
        private Vector3 moveDirection = Vector3.zero;
        private CharacterController characterController;
        private Camera cam;
        private IInteracter interacter;
        private bool canInteract;

        public void Init(IInputPlayer inputPlayer)
        {
            this.inputPlayer = inputPlayer;
            canMove = true;
        }

        public void Interacted(IInteracter interacter, bool canInteract)
        {
            hint.SetActive(canInteract);
            this.interacter = interacter;
            this.canInteract = canInteract;
        }

        public void CanMove(bool v)
        {
            canMove = v;
        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            cam = GetComponentInChildren<Camera>();
            InstallFOV = cam.fieldOfView;
        }

        private void Update()
        {
            if (!characterController.isGrounded)
                moveDirection.y -= data.Gravity * Time.deltaTime;

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            vertical = canMove ? data.Speed * Input.GetAxis("Vertical") : 0;
            horizontal = canMove ? data.Speed * Input.GetAxis("Horizontal") : 0;

            float movementDirectionY = moveDirection.y;

            moveDirection = (forward * vertical) + (right * horizontal);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
                moveDirection.y = data.JumpHeight;
            else
                moveDirection.y = movementDirectionY;

            characterController.Move(moveDirection * Time.deltaTime);
            Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

            if (canMove)
            {
                Lookvertical = -Input.GetAxis("Mouse Y");
                Lookhorizontal = Input.GetAxis("Mouse X");

                rotationX += Lookvertical * data.LookSpeed;
                rotationX = Mathf.Clamp(rotationX, -data.LookLimit, data.LookLimit);
                cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Lookhorizontal * data.LookSpeed, 0);

                if (Moving)
                    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, data.ReturnSpeedCamera * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.E) && canInteract && interacter!= null)
            {
                interacter.InvokeIneract();
                hint.SetActive(false);
            }
        }

    }
}
