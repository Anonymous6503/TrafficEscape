using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float _carSpeed = 2;

    [SerializeField] private bool _canChangeDirection;

    public Vector2 _currentVector;
    // Start is called before the first frame update
    void Start()
    {
        _currentVector = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (Vector3) _currentVector *_carSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ChangeDirectionTrigger") && _canChangeDirection)
        {
            Debug.Log("Triggered Colliders");
            this.gameObject.transform.rotation =
                Quaternion.RotateTowards(transform.rotation, new Quaternion(0, 0, -90, 0), 1);
            _canChangeDirection = false;

            _currentVector = Vector2.right;
        }
    }
}
