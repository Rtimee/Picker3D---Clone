using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    #region Variables

    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] [Range(0, 1f)] private float _smoothness;

    #endregion

    #region MonoBehaviour Callbacks

    private void FixedUpdate()
    {
        // Lerp with no X axis
        Vector3 targetPosition = new Vector3(transform.position.x, _target.position.y + _offset.y, _target.position.z + _offset.z);
        transform.position = Vector3.Slerp(transform.position, targetPosition, _smoothness);
    }

    // For set offset
    public void ChangeOffset(Vector3 _newOffset)
    {
        _offset.x = _newOffset.x;
        _offset.y = _newOffset.y;
        _offset.z = _newOffset.z;
    }

    #endregion
}
