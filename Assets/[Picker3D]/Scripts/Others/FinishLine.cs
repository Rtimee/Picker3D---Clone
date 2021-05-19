using UnityEngine;

public class FinishLine : MonoBehaviour
{
    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        EventManager.OnLevelPassed.AddListener(LevelPassed);
    }

    private void OnDisable()
    {
        EventManager.OnLevelPassed.RemoveListener(LevelPassed);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ObjectDetector>();

        if (player != null)
            EventManager.OnLevelPassed.Invoke();
    }

    #endregion

    #region Other Methods

    private void LevelPassed()
    {
        Debug.Log("Level Cleared");
    }

    #endregion
}
