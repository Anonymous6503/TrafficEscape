using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const float _carSpeed = 5;
    public float _speed;

    [SerializeField] private bool _retreating;
    [SerializeField] private bool _canRotate;
    [SerializeField] private bool _isMoving;
    
    [SerializeField] private WayPointSO MainTurnDetails;
    [SerializeField] private WayPointSO _turnDetails;

    [SerializeField] private Vector2 _currentMovingDirection;
    [SerializeField] private Vector3 _initialPosition;

    [SerializeField] private GameObject _ray;
    [SerializeField] private GameObject _rotationDirection;

    [SerializeField] private InitialDirection _initialDirection;

    public event Action OnStopping;

    private RaycastHit2D _hitInfo;

    private void Awake()
    {
        _turnDetails = Instantiate(MainTurnDetails);
        _initialPosition = this.transform.position;
        _turnDetails.Initialize(_initialDirection);
        Rotate();
    }

    private void Start()
    {
        
        /*_currentMovingDirection = _turnDetails.movementDir;*/
    }

    private void OnEnable()
    {
        if (GameManager.Instance._moveCount <= 0)
        {
            GameManager.Instance.GameOverEvent();
            return;
        }
        GameManager.Instance._moveCount--;
        GameManager.Instance._LevelManager._moveCountText.text = GameManager.Instance._moveCount.ToString("00");
        OnStopping += OnCarStopped;
        _turnDetails.index = 0;
        _speed = 02f;
        _retreating = false;
        _isMoving = true;
        _canRotate = true;
        _ray.transform.localPosition = new Vector3(_ray.transform.localPosition.x, -_ray.transform.localPosition.y,
            _ray.transform.localPosition.z);
        _currentMovingDirection = _turnDetails.movementDir;
    }

    private void OnDisable()
    {
        OnStopping -= OnCarStopped;
    }

    private void Update()
    {
        this.transform.position += (Vector3) _currentMovingDirection * _speed * Time.deltaTime;
        
        CheckForTrigger();
        
        if (_retreating)
        {
            if ((_initialPosition - transform.position).magnitude <= 0.1f)
            {
                _speed = 0;
                _isMoving = false;
                OnStopping.Invoke();
            }
        }
    }

    void CheckForTrigger()
    {
        if (_turnDetails.turnType.Count == 0)
        {
            Debug.Log("Test");
            return;
        }

        _hitInfo = Physics2D.Raycast(_ray.transform.position, Vector3.forward);
        
        
        if (_hitInfo.collider != null && _hitInfo.collider.CompareTag("ChangeDirectionTrigger") && _canRotate)
        {
            Debug.Log("Change Trigger Triggered");
            if (_turnDetails.index < _turnDetails.turnType.Count)
            {
                ChangeDirection(_turnDetails.turnType[_turnDetails.index]);
                Rotate();
                _turnDetails.index++;
                _canRotate = false;
            }
        }

        if (!_hitInfo.collider.CompareTag("ChangeDirectionTrigger"))
        {
            _canRotate = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            Debug.Log("Collided with Car");
            _ray.transform.localPosition = new Vector3(_ray.transform.localPosition.x, -_ray.transform.localPosition.y,
                _ray.transform.localPosition.z);
            _retreating = true;
            _speed *= -1;
            _turnDetails.index = 0;
            _canRotate = true;
        }
        
        
        /*if (other.gameObject.CompareTag("ChangeDirectionTrigger"))
        {
            Debug.Log("Rotate Now");
            //ChangeMovementDirection(_turnDetails.turnType[_turnDetails.index], _turnDetails);
            if(_turnDetails.turnType.Count == 0)
                return;
            if (_turnDetails.index < _turnDetails.turnType.Count)
            {
                ChangeDirection(_turnDetails.turnType[_turnDetails.index]);
                Rotate();
                _turnDetails.index++;
            }
        }*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OuterBound"))
        {
            Destroy(this.gameObject);
            GameManager.Instance._totalCarsCount--;

            if (GameManager.Instance._totalCarsCount <= 0)
            {
                GameManager.Instance.LevelCleardEvent();
            }
            Debug.Log("Destroyed");
        }
    }

    void Rotate()
    {
        switch (_turnDetails.currentMovingDirection)
        {
            case MovingDirection.Up:
                transform.eulerAngles = new Vector3(0, 0, 0);
                
                break;
            case MovingDirection.Down:
                transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case MovingDirection.Left:
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case MovingDirection.Right:
                transform.eulerAngles = new Vector3(0, 0, -90);
                break;
        }
        Quaternion rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x,this.transform.rotation.y,this.transform.rotation.z));
        _rotationDirection.transform.rotation = Quaternion.Inverse(rotation);
    }

    void ChangeDirection(string nextDirection)
    {
        if (!_retreating)   
        {
            switch (_turnDetails.currentMovingDirection)
            {
                case MovingDirection.Up:
                    if (nextDirection.Equals("TurnLeft"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Left;
                    }
                    else if(nextDirection.Equals("TurnRight"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Right;
                    }

                    break;
                case MovingDirection.Down:
                    if (nextDirection.Equals("TurnLeft"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Right;
                    }
                    else if(nextDirection.Equals("TurnRight"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Left;
                    }
                    break;
                case MovingDirection.Left:
                    if (nextDirection.Equals("TurnLeft"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Down;
                    }
                    else if(nextDirection.Equals("TurnRight"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Up;
                    }
                    break;
                case MovingDirection.Right:
                    if (nextDirection.Equals("TurnLeft"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Up;
                    }
                    else if(nextDirection.Equals("TurnRight"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Down;
                    }
                    break;
            }
        }
        else
        {
            switch (_turnDetails.currentMovingDirection)
            {
                case MovingDirection.Up:
                /*if (nextDirection.Equals("TurnLeft"))
                {
                    _turnDetails.currentMovingDirection = MovingDirection.Left;
                }
                else if(nextDirection.Equals("TurnRight"))
                {
                    _turnDetails.currentMovingDirection = MovingDirection.Right;
                }*/
                case MovingDirection.Down:
                    if (nextDirection.Equals("TurnLeft"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Left;
                    }
                    else if(nextDirection.Equals("TurnRight"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Right;
                    }
                    break;
                case MovingDirection.Left:
                    if (nextDirection.Equals("TurnLeft"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Up;
                    }
                    else if(nextDirection.Equals("TurnRight"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Down;
                    }
                    break;
                case MovingDirection.Right:
                    if (nextDirection.Equals("TurnLeft"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Down;
                    }
                    else if(nextDirection.Equals("TurnRight"))
                    {
                        _turnDetails.currentMovingDirection = MovingDirection.Up;
                    }
                    break;
            }
        }
        _turnDetails.SetMovementDirection(_turnDetails.currentMovingDirection);
        _currentMovingDirection = _turnDetails.movementDir;
    }

    void OnCarStopped()
    {
        _speed = 0;
        _retreating = false;
        _canRotate = true;
        this.GetComponent<CarController>().enabled = false;
    }
}
