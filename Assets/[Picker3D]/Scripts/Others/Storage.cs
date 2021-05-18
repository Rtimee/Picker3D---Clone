using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Storage : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform _missingPart;
    [SerializeField] private int _requireObjectCount;

    private List<Transform> _objectsInStorage;
    private List<Transform> _ObjectsInStorage { get { return _objectsInStorage == null ? _objectsInStorage = new List<Transform>() : _objectsInStorage; }}

    private TextMeshPro _requireObjectCountText;
    private TextMeshPro _RequireObjectCountText { get { return _requireObjectCountText == null ? _requireObjectCountText = GetComponentInChildren<TextMeshPro>() : _requireObjectCountText; }}

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        UpdateText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var _obj = collision.transform;

        if (_obj.CompareTag("Object") && !_ObjectsInStorage.Contains(_obj))
            AddToStorage(_obj);
    }

    #endregion

    #region Other Methods

    private void AddToStorage(Transform _target)
    {
        _ObjectsInStorage.Add(_target);
        UpdateText();

        if (_ObjectsInStorage.Count == _requireObjectCount)
            _missingPart.DOLocalMoveY(1.45f, .5f).OnComplete(() =>
            {
                PlayerController.Instance.SetWaitingState(PlayerStates.PlayerState.Moving);
                Destroy(this);
            });
    }

    private void UpdateText()
    {
        _RequireObjectCountText.text = _ObjectsInStorage.Count + "/" + _requireObjectCount;
    }

    #endregion
}
