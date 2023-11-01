using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCarController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _currentMovingDirection;

    [SerializeField] private bool _canRotate;

    private RaycastHit2D _hitInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        _canRotate = true;
        _currentMovingDirection = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)_currentMovingDirection * _speed * Time.deltaTime;
        GetTrigger();
    }

    void GetTrigger()
    {
        _hitInfo = Physics2D.Raycast(transform.position, Vector3.forward);
        
        if (_hitInfo.collider != null && _hitInfo.collider.CompareTag("ChangeDirectionTrigger") && _canRotate)
        {
            Debug.Log("Collided");
            Debug.Log(_hitInfo.collider.gameObject.name);
            _currentMovingDirection = Vector2.left;
            transform.Rotate(0,0,-90);
            _canRotate = false;
        }

        if (!_hitInfo.collider.CompareTag("ChangeDirectionTrigger") && !_canRotate)
        {
            _canRotate = true;
        }
        
        /*if(_hitInfo.collider != null)
            Debug.Log(_hitInfo.collider.name);
        if (_hitInfo.collider.CompareTag("ChangeDirectionTrigger"))
        {
            Debug.Log("Change Direction");
        }*/
    }
}
