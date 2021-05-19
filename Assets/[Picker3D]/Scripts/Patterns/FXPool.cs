using System.Collections.Generic;
using UnityEngine;

public class FXPool : MonoBehaviour
{
    #region Veriables

    [SerializeField] private ParticleSystem _prefab;
    [SerializeField] private int _poolSize;

    private List<ParticleSystem> _objectPool;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        InitPool();
    }

    #endregion

    #region Private Methods

    public void InitPool()
    {
        _objectPool = new List<ParticleSystem>();
        for (int i = 0; i < _poolSize; i++)
        {
            ParticleSystem particle = Instantiate(_prefab, transform);
            particle.GetComponent<Fx>().myFxData.myFxPool = this;
            _objectPool.Add(particle);
            _objectPool[i].gameObject.SetActive(false);
        }
    }

    public ParticleSystem GetObjFromPool(Vector3 pos)
    {
        ParticleSystem newObject = _objectPool[_objectPool.Count - 1];
        newObject.gameObject.SetActive(true);
        newObject.transform.position = pos;
        newObject.transform.rotation = _prefab.transform.rotation;
        _objectPool.RemoveAt(_objectPool.Count - 1);
        return newObject;
    }

    public void ReturnObjToPool(ParticleSystem go)
    {
        go.gameObject.SetActive(false);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        _objectPool.Add(go);
    }

    #endregion
}
