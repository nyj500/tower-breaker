using UnityEngine;
using TowerBreaker.UI;

namespace TowerBreaker.Core
{
    public enum GameState { Lobby, Battle, Result }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameState currentState = GameState.Lobby;
        public GameState CurrentState => currentState;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // TODO: 초기 상태 설정, 로비 UI 활성화
        }

        public void GoToWorld()
        {
            // TODO: 로비에서 월드맵으로 이동, 월드맵 UI 활성화
            // TODO: 상태를 Battle로 전환, StageManager.StartStage() 호출
            ChangeState(GameState.Battle);
        }

        public void OnPlayerDead()
        {
            // TODO: 상태를 Result로 전환, 게임오버 UI 표시
            ChangeState(GameState.Result);
        }

        public void OnStageClear()
        {
            // TODO: 클리어 연출 후 다음 층으로 진행 또는 Result 화면 전환
        }

        public void ReturnToLobby()
        {
            // TODO: 씬 재로드 또는 상태 초기화 후 Lobby로 전환
            ChangeState(GameState.Lobby);
        }

        private void ChangeState(GameState newState)
        {
            currentState = newState;
            // TODO: EventBus를 통해 상태 변경 이벤트 발행
            EventBus.Publish(new GameStateChangedEvent { NewState = newState });
        }
    }

    public struct GameStateChangedEvent
    {
        public GameState NewState;
    }
}
