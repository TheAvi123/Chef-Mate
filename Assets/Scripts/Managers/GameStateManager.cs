using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager sharedInstance;

    private enum GameState {Null, Menu, Play, Over, Instructions, CutScene};

    [Header("Scene Names")]
    [SerializeField] string menuSceneName        = null;
    [SerializeField] string playSceneName        = null;
    [SerializeField] string gameOverSceneName    = null;
    [SerializeField] string instructionSceneName = null;
    [SerializeField] string cutSceneName         = null;

    //Configuration Parameters
    [Header("Configuration")]
    [SerializeField] bool startFromInitialState = true;
    [SerializeField] GameState initialState = GameState.Menu;

    //State Variables
    private GameState currentState;
    private Coroutine waitLoader = null;

    //Internal Methods
    private void Awake() {
        SetSharedInstance();
        SetCurrentState();
        LoadInitialState();
    }

    private void OnSceneChange() {
        SetCurrentState();
    }   //Called from Singleton

    private void SetSharedInstance() {
        sharedInstance = this;
    }

    private void SetCurrentState() {
        currentState = SceneNameToState(SceneManager.GetActiveScene());
    }

    public void LoadInitialState() {
        if (startFromInitialState && currentState != initialState) {
            currentState = initialState;
            SceneManager.LoadScene(StateToName(initialState));
        }
    }

    //Helper Methods
    private string StateToName(GameState state) {
        switch (state) {
            case GameState.Menu:
                return menuSceneName;
            case GameState.Play:
                return playSceneName;
            case GameState.Over:
                return gameOverSceneName;
            case GameState.Instructions:
                return instructionSceneName;
            case GameState.CutScene:
                return cutSceneName;
            case GameState.Null:
            default:
                Debug.LogError("Game State " + state.ToString() + " Does Not Exist");
                return null;
        }
    }

    private GameState SceneNameToState(Scene scene) {
        string sceneName = scene.name;
        if (sceneName == menuSceneName) {
            return GameState.Menu;
        } else if (sceneName == playSceneName) {
            return GameState.Play;
        } else if (sceneName == gameOverSceneName) {
            return GameState.Over;
        } else if (sceneName == instructionSceneName) {
            return GameState.Instructions;
        } else if (sceneName == cutSceneName) {
            return GameState.CutScene;
        } else {
            Debug.LogError("Scene " + scene.name + " Does Not Exist in GameState Enumeration");
            return GameState.Null;
        }
    }

    private void LoadState(GameState state) {
        SceneManager.LoadScene(StateToName(state));
    }

    private IEnumerator WaitAndLoad(GameState state, float delayInSeconds) {
        yield return new WaitForSecondsRealtime(delayInSeconds);
        LoadState(state);
        waitLoader = null;
    }

    //Load Methods
    public void LoadStartMenu() {
        LoadState(GameState.Menu);
    }

    public void PlayGame() {
        LoadState(GameState.Play);
    }

    public void QuitGame() {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    public void GameOver(float delayInSeconds) {
        if (waitLoader != null) {
            Debug.LogError("WaitAndLoad Already In Progress!");
            return;
        }
        waitLoader = StartCoroutine(WaitAndLoad(GameState.Over, delayInSeconds));
    }

    public void LoadInstructions() {
        LoadState(GameState.Instructions);
    }

    public void LoadCutScene() {
        LoadState(GameState.CutScene);
    }

    //Public Methods
    public string GetCurrentScene() {
        return SceneManager.GetActiveScene().name;
    }
}