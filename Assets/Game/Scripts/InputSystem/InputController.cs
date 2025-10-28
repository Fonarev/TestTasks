using UnityEngine;

namespace Assets.Game.Scripts.InputSystem
{
    public class InputController : MonoBehaviour
    {
        private IControllable controllable;
        private IInputControl inputControl;

        private void Awake()
        {
            if(TryGetComponent(out IControllable controllable))
                this.controllable = controllable;
            else
                Debug.LogError("[InputController] It is null during awaked" + controllable + "need to add component!");

            inputControl = new KeyboardInputControl();
            inputControl.Init(controllable);
        }

        private void OnEnable()
        {
            inputControl?.EnableControl();
        }

        private void OnDisable()
        {
            inputControl?.DisableControl();
        }

        private void Update()
        {
            inputControl?.Update();
        }

        public void Switch(bool isEnable)
        {
            if (isEnable)
                inputControl.EnableControl();
            else
                inputControl?.DisableControl();
        }

    }
}