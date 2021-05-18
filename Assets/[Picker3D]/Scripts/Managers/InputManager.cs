using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothness;

    private float _firstValue;
    private float _lastValue;
    private float _distance;
    private float _calculatedValue;

    #endregion

    #region MonoBehaviour Callbacks

    private void Update()
    {
        ControllInput();
    }

    #endregion

    #region Other Methods

    private void ControllInput()
    {
        if (Input.GetMouseButtonDown(0))
            _firstValue = Input.mousePosition.x;
        else if (Input.GetMouseButton(0))
        {
            _lastValue = Input.mousePosition.x;
            _distance = _lastValue - _firstValue;
            _calculatedValue = (_distance / Screen.width) * _smoothness;

            // Movement
            PlayerController.Instance.Slide(_calculatedValue);
            
            _firstValue = _lastValue;
        }
    }

    #endregion
}
