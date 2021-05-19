using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    #region Variables

    [HideInInspector] public bool canPass;

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
            SetWaitingState(PlayerStates.PlayerState.Waiting);
            var storage = other.GetComponentInParent<Storage>();
            canPass = ObjectDetector.Instance.CheckHaveEnoughObjects(storage.GetRequireObjectCount());
            StartCoroutine(CheckPlayerState());
            Destroy(other.gameObject);
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

    public void StopPlayer()
    {
        _Rigidbody.velocity = Vector3.zero;
    }

    // Private Methods

    private IEnumerator CheckPlayerState()
    {
        yield return new WaitForSeconds(2f);
        if (!canPass)
            EventManager.OnGameOver.Invoke();
    }

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
