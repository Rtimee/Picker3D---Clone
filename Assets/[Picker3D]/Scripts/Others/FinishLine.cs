using System.Collections;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject _finishFx;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ObjectDetector>();

        if (player != null)
            StartCoroutine(LevelPassed());
    }

    #endregion

    #region Other Methods

    private IEnumerator LevelPassed()
    {
        PlayerController.Instance.SetWaitingState(PlayerStates.PlayerState.Waiting);
        _finishFx.SetActive(true);
        yield return new WaitForSeconds(2f);
        EventManager.OnLevelPassed.Invoke();
    }

    #endregion
}
