namespace Assets.Game.Character.Scripts.Interacters
{
    public interface IInteracter 
    {
        public void OnZoneEnter(PlayerController player);
        public void OnZoneExit(PlayerController player);
        public void InvokeIneract();

    }
}