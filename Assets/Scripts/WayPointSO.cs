using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class initialDirection
{
    public MovingDirection _initialDirection;
}
[CreateAssetMenu(fileName = "Waypoint", menuName = "Waypoint_So")]
public class WayPointSO : ScriptableObject
{
    
    public initialDirection initialMovingDirection; 
    [Space]
    public int index = 0;
    
    public Vector2 movementDir;
    public MovingDirection currentMovingDirection; 
    public List<string> turnType = new List<string>();

    public void Initialize()
    {
        index = 0;
        currentMovingDirection = initialMovingDirection._initialDirection;
        SetMovementDirection(currentMovingDirection);
        Debug.Log("Initialized Success");
    }

    public void SetMovementDirection(MovingDirection dir)
    {
        switch (dir)
        {
            case MovingDirection.Up:
                movementDir = Vector2.up;
                break;
            case MovingDirection.Down:
                movementDir = Vector2.down;
                break;
            case MovingDirection.Left:
                movementDir = Vector2.left;
                break;
            case MovingDirection.Right:
                movementDir = Vector2.right;
                break;
        }
    }
    
    
}
