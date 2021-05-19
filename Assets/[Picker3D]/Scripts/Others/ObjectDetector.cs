using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : Singleton<ObjectDetector>
{
    #region Variables

    [SerializeField] private float _pushForce;

    private List<Rigidbody> _myObjects;
    private List<Rigidbody> _MyObjects { get { return _myObjects == null ? _myObjects = new List<Rigidbody>() : _myObjects; }}

    #endregion

    #region MonoBehaviour Callbacks

    private void OnTriggerEnter(Collider other)
    {
        var _obj = other.GetComponent<Rigidbody>();

        if (other.CompareTag("Object") && !_MyObjects.Contains(_obj))
            AddToList(_obj);
    }

    private void OnTriggerExit(Collider other)
    {
        var _obj = other.GetComponent<Rigidbody>();

        if (other.CompareTag("Object") && _MyObjects.Contains(_obj))
            RemoveFromList(_obj);
    }

    #endregion

    #region Other Methods

    // Public Methods

    public void PushAllObjects()
    {
        for (int i = 0; i < _MyObjects.Count; i++)
        {
            _MyObjects[i].gameObject.layer = 9;
            _MyObjects[i].AddForce(Vector3.forward * _pushForce);
        }
    }

    public void ClearList()
    {
        _MyObjects.Clear();
    }

    public bool CheckHaveEnoughObjects(int _count)
    {
        return _MyObjects.Count >= _count;
    }

    // Private Methods

    private void AddToList(Rigidbody _obj)
    {
        _MyObjects.Add(_obj);
    }

    private void RemoveFromList(Rigidbody _obj)
    {
        _MyObjects.Remove(_obj);
    }

    #endregion
}
