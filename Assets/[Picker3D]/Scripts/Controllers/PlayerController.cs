using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    #region Variables

    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _forceLimit;

    private Rigidbody _rigidBody;
    private Rigidbody _Rigidbody { get { return _rigidBody == null ? _rigidBody = GetComponent<Rigidbody>() : _rigidBody; }}

    #endregion

    #region MonoBehaviour Callbacks

    private void FixedUpdate()
    {
        MoveForward();
    }

    #endregion

    #region Other Methods

    // Public Methods

    public void Slide(float _value)
    {
        _value = Mathf.Clamp(_value, -_forceLimit, _forceLimit);
        _Rigidbody.AddForce(_value * transform.right);
    }

    // Private Methods

    private void MoveForward()
    {
        _Rigidbody.velocity = transform.forward * _playerSpeed * Time.fixedDeltaTime;
    }

    #endregion
}
