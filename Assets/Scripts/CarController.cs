using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float _carSpeed = 2;

    [SerializeField] private bool _retreating;
    
    [SerializeField] private WayPointSO _turnDetails;

    [SerializeField] private Vector2 _currentMovingDirection;
    
    
    private void Start()
    {
        _turnDetails.Initialize();
        _retreating = false;
        _currentMovingDirection = _turnDetails.movementDir;
    }

    private void Update()
    {
        this.transform.position += (Vector3) _currentMovingDirection * _carSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Car"))
            Debug.Log("Collided with car");
        
        if (other.gameObject.CompareTag("ChangeDirectionTrigger"))
        {
            Debug.Log("Rotate Now");
            //ChangeMovementDirection(_turnDetails.turnType[_turnDetails.index], _turnDetails);
            if(_turnDetails.turnType.Count == 0)
                return;
            if (_turnDetails.index < _turnDetails.turnType.Count)
            {
                ChangeDirection(_turnDetails.turnType[_turnDetails.index]);
                Rotate(_turnDetails.turnType[_turnDetails.index]);
                _turnDetails.index++;
            }
        }
    }

    void Rotate(string nextMoveDirection)
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
    }

    void ChangeMovementDirection(string nextDirection, WayPointSO wayPointSo)
    {
        switch (nextDirection)
        {
            case "TurnLeft":
                wayPointSo.currentMovingDirection = MovingDirection.Left;
                wayPointSo.SetMovementDirection(wayPointSo.currentMovingDirection);
                break;
            case "TurnRight":
                wayPointSo.currentMovingDirection = MovingDirection.Right;
                break;
            case "TurnUp":
                wayPointSo.currentMovingDirection = MovingDirection.Up;
                break;
            case "TurnDown":
                wayPointSo.currentMovingDirection = MovingDirection.Down;
                break;
        }

        
        wayPointSo.SetMovementDirection(wayPointSo.currentMovingDirection);
        _currentMovingDirection = _turnDetails.movementDir;
        Debug.Log(wayPointSo.currentMovingDirection);
    }

    void ChangeDirection(string nextDirection)
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
        _turnDetails.SetMovementDirection(_turnDetails.currentMovingDirection);
        _currentMovingDirection = _turnDetails.movementDir;
    }
}
