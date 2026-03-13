namespace TowerBreaker.Player
{
    public interface IPlayerState
    {
        void Enter();
        void Exit();
        void Update();
        void FixedUpdate();
    }
}
