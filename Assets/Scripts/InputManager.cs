using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Vector2 _touchPosition;

    [SerializeField] private Vector2 _touchWorldPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetSelectedObject();
    }

    public void GetSelectedObject()
    {
        if (Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                
                _touchPosition = touch.position;

                _touchWorldPosition = Camera.main.ScreenToWorldPoint(_touchPosition);
                
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(_touchWorldPosition, Camera.main.transform.forward);
                if(hitInfo.collider == null)
                    return;
                if (hitInfo.collider.CompareTag("Car"))
                {
                    CarController go = hitInfo.transform.GetComponent<CarController>();
                    go.enabled = true;

                }
            }
        }
    }
}