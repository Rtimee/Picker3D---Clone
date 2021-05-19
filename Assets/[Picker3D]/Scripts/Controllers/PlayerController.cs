using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    #region Variables

    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _forceLimit;

    private Rigidbody _rigidBody;
    private Rigidbody _Rigidbody { get { return _rigidBody == null ? _rigidBody = GetComponent<Rigidbody>() : _rigidBody; }}

    private PlayerStates.PlayerState _myState;
    private float _initialSpeed;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        _initialSpeed = _playerSpeed;
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.isGameStarted)
            MoveForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Storage_Trigger"))
        {
            Destroy(other.gameObject);
            SetWaitingState(PlayerStates.PlayerState.Waiting);
        }
    }

    #endregion

    #region Other Methods

    // Public Methods

    public void Slide(float _value)
    {
        _value = Mathf.Clamp(_value, -_forceLimit, _forceLimit);
        _Rigidbody.AddForce(_value * _Rigidbody.mass / 2 * transform.right);
    }

    public void SetWaitingState(PlayerStates.PlayerState _state)
    {
        _myState = _state;
        UpdatePlayerState();
    }

    // Private Methods

    private void MoveForward()
    {
        _Rigidbody.velocity = transform.forward * _playerSpeed * Time.fixedDeltaTime;
    }

    private void UpdatePlayerState()
    {
        switch (_myState)
        {
            case PlayerStates.PlayerState.Moving:
                _playerSpeed = _initialSpeed;
                ObjectDetector.Instance.ClearList();
                break;
            case PlayerStates.PlayerState.Waiting:
                _playerSpeed = 0;
                ObjectDetector.Instance.PushAllObjects();
                break;
        }
    }

    #endregion
}
