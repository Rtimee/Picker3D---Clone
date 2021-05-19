using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Variables

    [HideInInspector] public bool isGameStarted = true;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        EventManager.OnGameOver.AddListener(EndOfLevel);
        EventManager.OnLevelPassed.AddListener(EndOfLevel);
    }

    private void OnDisable()
    {
        EventManager.OnGameOver.RemoveListener(EndOfLevel);
        EventManager.OnLevelPassed.RemoveListener(EndOfLevel);
    }

    #endregion

    #region Other Methods

    private void EndOfLevel()
    {
        isGameStarted = false;
    }

    #endregion
}
