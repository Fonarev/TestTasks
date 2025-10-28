namespace Assets.Game.MiniGames.Scripts
{
    public interface IMiniGame
    {
        void ConditionsCheck(int id, bool presset);
        void Exit();
        void Restart();
    }
}