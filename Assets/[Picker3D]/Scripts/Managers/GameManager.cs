using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Variables

    [HideInInspector] public bool isGameStarted = true;

    #endregion

    #region MonoBehaviour Callbacks

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

    private void LevelClear()
    {
        isGameStarted = false;
        Debug.Log("Level Clear");
    }

    private void GameOver()
    {
        isGameStarted = false;
        Debug.Log("Game Over");
    }

    #endregion
}
