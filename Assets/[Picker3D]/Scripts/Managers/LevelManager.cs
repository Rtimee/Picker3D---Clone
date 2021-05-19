using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region Variables

    [SerializeField] private LevelData[] _levelDatas;

    private LevelData _currentLevelData;

    #endregion

    #region Other Methods

    public void SpawnLevel(int index)
    {
        if (index > _levelDatas.Length - 1)
            index = GetRandomLevel();

        _currentLevelData = _levelDatas[index];
        Instantiate(_currentLevelData.levelPrefab);
        PlayerPrefs.SetInt("LevelIndex", index);
    }

    int GetRandomLevel()
    {
        int index = Random.Range(0, _levelDatas.Length);

        int lastLevel = PlayerPrefs.GetInt("LevelIndex");
        if (index == lastLevel)
            return GetRandomLevel();
        else
            return index;
    }

    #endregion
}
