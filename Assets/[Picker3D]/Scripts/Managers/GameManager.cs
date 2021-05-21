using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Variables

    [HideInInspector] public bool isGameStarted;

    [SerializeField] private GameObject _startTutorialObject;

    private int _currentLevelIndex;
    private int _nextLevelIndex;
    private int _totalLevelIndex;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        base.Awake();
        GetData();
        LoadLevel();
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
        if (_totalLevelIndex == 0)
            _startTutorialObject.SetActive(true);
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    // Private Methods

    private void LevelClear()
    {
        isGameStarted = false;
        LevelUp();
    }

    private void GameOver()
    {
        isGameStarted = false;
        PlayerPrefs.SetInt("LevelPassed", 0);
    }

    private void GetData()
    {
        _currentLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
        _totalLevelIndex = PlayerPrefs.GetInt("TotalLevelIndex",0);
        _nextLevelIndex = PlayerPrefs.GetInt("NextLevelIndex", 0);
    }

    private void LevelUp()
    {
        _currentLevelIndex++;
        _totalLevelIndex++;
        PlayerPrefs.SetInt("LevelIndex", _currentLevelIndex);
        PlayerPrefs.SetInt("TotalLevelIndex", _totalLevelIndex);
        PlayerPrefs.SetInt("LevelPassed", 1);
    }

    private void LoadLevel()
    {
        LevelManager.Instance.SpawnLevel(_totalLevelIndex, _nextLevelIndex);
        UIManager.Instance.LoadGameUI(_totalLevelIndex + 1);
    }

    #endregion
}
