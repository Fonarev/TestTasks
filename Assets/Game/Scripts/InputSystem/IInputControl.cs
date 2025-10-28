namespace Assets.Game.Scripts.InputSystem
{
    public interface IInputControl
    {
        void Init(IControllable controllable);
        void EnableControl();
        void DisableControl();
        void Update();
    }
}