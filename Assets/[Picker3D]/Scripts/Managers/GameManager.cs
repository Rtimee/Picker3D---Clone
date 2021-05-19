using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Variables

    public bool isGameStarted;

    private int _currentLevelIndex;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        base.Awake();
        GetData();
        LevelManager.Instance.SpawnLevel(_currentLevelIndex);
        UIManager.Instance.LoadGameUI(_currentLevelIndex + 1);
    }

    private void OnEnable()
    {
        EventManager.OnGameOver.AddListener(GameOver);
        EventManager.OnLevelPassed.AddListener(LevelClear);
    }

    private void OnDisable()
    {
        EventManager.OnGameOver.RemoveListener(GameOver);
        EventManager.OnLevelPassed.RemoveListener(LevelClear);
    }

    #endregion

    #region Other Methods

    // Public Methods

    public void StartGameButton()
    {
        isGameStarted = true;
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    // Private Methods

    private void LevelClear()
    {
        PlayerController.Instance.StopPlayer();
        isGameStarted = false;
        LevelUp();
    }

    private void GameOver()
    {
        PlayerController.Instance.StopPlayer();
        isGameStarted = false;
    }

    private void GetData()
    {
        _currentLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
    }

    private void LevelUp()
    {
        _currentLevelIndex++;
        PlayerPrefs.SetInt("LevelIndex", _currentLevelIndex);
    }

    #endregion
}
