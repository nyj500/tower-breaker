using UnityEngine;
using UnityEngine.SceneManagement;
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
            Random.InitState(System.Environment.TickCount);
        }

        private void Start()
        {
            // TODO: 초기 상태 설정, 로비 UI 활성화
        }

        public void GoToWorld()
        {
            ChangeState(GameState.Battle);
            SceneManager.LoadScene("Game");
        }

        public void OnPlayerDead()
        {
            ChangeState(GameState.Result);
            StartCoroutine(ReturnToLobbyDelayed(2f));
        }

        private System.Collections.IEnumerator ReturnToLobbyDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnToLobby();
        }

        public void OnStageClear()
        {
            // TODO: 클리어 연출 후 다음 층으로 진행 또는 Result 화면 전환
        }

        public void ReturnToLobby()
        {
            ChangeState(GameState.Lobby);
            SceneManager.LoadScene("Main");
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
