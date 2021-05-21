﻿using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public class Storage : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform _playformPart;
    [SerializeField] private Transform[] _barrierParts;
    [SerializeField] private GameObject _barrierFx;
    [SerializeField] private FXData _objectsParticleData;
    [SerializeField] private int _requireObjectCount;

    private List<Transform> _objectsInStorage;
    private List<Transform> _ObjectsInStorage { get { return _objectsInStorage == null ? _objectsInStorage = new List<Transform>() : _objectsInStorage; }}

    private TextMeshPro _requireObjectCountText;
    private TextMeshPro _RequireObjectCountText { get { return _requireObjectCountText == null ? _requireObjectCountText = GetComponentInChildren<TextMeshPro>() : _requireObjectCountText; }}

    private Material _platformPartMaterial;
    private Material _PlatformPartMaterial { get { return _platformPartMaterial == null ? _platformPartMaterial = _playformPart.GetComponent<MeshRenderer>().material : _platformPartMaterial; }}

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        _playformPart.DOLocalMoveY(-1.5f, 0);
    }

    private void Start()
    {
        UpdateText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var _obj = collision.transform;

        if (_obj.CompareTag("Object") && !_ObjectsInStorage.Contains(_obj) && GameManager.Instance.isGameStarted)
            AddToStorage(_obj);
    }

    #endregion

    #region Other Methods

    // Public Methods

    public int GetRequireObjectCount()
    {
        return _requireObjectCount;
    }

    // Private Methods

    private void AddToStorage(Transform _target)
    {
        _ObjectsInStorage.Add(_target);
        UpdateText();
        StartCoroutine(ExplodeObjectsWithDelay());
    }

    private void UpdateText()
    {
        _RequireObjectCountText.text = _ObjectsInStorage.Count + "/" + _requireObjectCount;
    }

    private IEnumerator ExplodeObjectsWithDelay()
    {
        if (_ObjectsInStorage.Count == _requireObjectCount)
        {
            PlayerController.Instance.canPass = true;
            for (int i = 0; i < _ObjectsInStorage.Count; i++)
            {
                yield return new WaitForSeconds(.05f);
                GameObject currentObject = _ObjectsInStorage[i].gameObject;
                _objectsParticleData.myFxPool.GetObjFromPool(currentObject.transform.position);
                Destroy(currentObject);
            }
            _playformPart.DOLocalMoveY(1.45f, 1.5f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                OpenBarrier();
                _PlatformPartMaterial.DOColor(LevelManager.Instance.GetLevelMaterial().color, 1f);
            });
        }
    }

    private void OpenBarrier()
    {
        for (int i = 0; i < _barrierParts.Length; i++)
            _barrierParts[i].DOLocalRotate(_barrierParts[i].transform.forward * (-60 * (-i == 0 ? 1 : - 1)), 1f).OnComplete(() => {
                _barrierFx.SetActive(true);
                PlayerController.Instance.SetWaitingState(PlayerStates.PlayerState.Moving);
                _playformPart.SetParent(null);
                Destroy(this);
            });
    }

    #endregion
}
