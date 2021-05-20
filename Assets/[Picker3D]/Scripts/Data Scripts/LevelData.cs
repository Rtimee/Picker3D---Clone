using UnityEngine;

[CreateAssetMenu(fileName = "Level",menuName = "Data/Level Data")]
public class LevelData : ScriptableObject
{
    public GameObject levelPrefab;
    public Material groundMaterial;
}
