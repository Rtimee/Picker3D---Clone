using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region Variables

    [HideInInspector] public Transform finishLine;

    [SerializeField] private LevelData[] _levelDatas;

    private LevelData _currentLevelData;
    private LevelData _nextLevelData;

    private PlatformGenerator _platformGenerator;
    private PlatformGenerator _PlatformGenerator { get { return _platformGenerator == null ? _platformGenerator = FindObjectOfType<PlatformGenerator>() : _platformGenerator; }}
    private int _IsLevelPassed { get { return PlayerPrefs.HasKey("LevelPassed") ? PlayerPrefs.GetInt("LevelPassed") : 0; }}

    #endregion

    #region MonoBehaviour Callback

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("LevelPassed", 0);
    }

    #endregion

    #region Other Methods

    // Public Methods

    public void SpawnLevel(int _index, int _nextLevelIndex)
    {
        if (_index >= _levelDatas.Length - 1)
        {
            if (_IsLevelPassed == 1)
            {
                _index = _nextLevelIndex;
                _nextLevelIndex = GetRandomLevel(_index);
            }
            else
                _index = PlayerPrefs.GetInt("LevelIndex");
        }
        else
            _nextLevelIndex = _index + 1;

        _currentLevelData = _levelDatas[_index];
        _nextLevelData = _levelDatas[_nextLevelIndex];

        Instantiate(_currentLevelData.levelPrefab);
        finishLine = FindObjectOfType<FinishLine>().transform;
        Instantiate(_nextLevelData.levelPrefab, _PlatformGenerator.transform);

        PlayerPrefs.SetInt("LevelIndex", _index);
        PlayerPrefs.SetInt("NextLevelIndex", _nextLevelIndex);
    }

    public Material GetLevelMaterial()
    {
        return _currentLevelData.groundMaterial;
    }

    // Private Methods

    private int GetRandomLevel(int _indexValue)
    {
        int _index = Random.Range(0, _levelDatas.Length);

        if (_index == _indexValue )
            return GetRandomLevel(_indexValue);
        else
            return _index;
    }

    #endregion
}
