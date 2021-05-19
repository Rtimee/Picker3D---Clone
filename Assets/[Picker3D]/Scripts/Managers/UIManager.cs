using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    #region Variables

    [SerializeField] private GameObject _finishLevelScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _gameplayScreen;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Text _currentLevelText;
    [SerializeField] private Text _nextLevelText;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        EventManager.OnGameOver.AddListener(ShowGameOverScreen);
        EventManager.OnLevelPassed.AddListener(ShowFinishScreen);
    }


    private void OnDisable()
    {
        EventManager.OnGameOver.RemoveListener(ShowGameOverScreen);
        EventManager.OnLevelPassed.RemoveListener(ShowFinishScreen);
    }

    #endregion

    #region Other Methods

    // Public Methods

    public void FillProgressBar(float _value)
    {
        _progressBar.fillAmount = _value;
    }

    public void LoadGameUI(int _currentLevel)
    {
        _currentLevelText.text = _currentLevel.ToString();
        _nextLevelText.text = (_currentLevel + 1).ToString();
    }

    // Private Methods

    private void ShowFinishScreen()
    {
        _gameplayScreen.SetActive(false);
        _finishLevelScreen.SetActive(true);
    }

    private void ShowGameOverScreen()
    {
        _gameplayScreen.SetActive(false);
        _gameOverScreen.SetActive(true);
    }

    #endregion
}
