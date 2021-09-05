using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    float forcePower = 0.1f;

    [SerializeField]
    Vector3 touchedPostion;
    [SerializeField]
    Vector3 endTouchPosition;

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && SausageSpawner.Instance.IsSausageOnGround())
        {
            Jump();
            Debug.Log("Touched");
        }*/

        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Finger moved");
            }
            if (touch.phase == TouchPhase.Began)
            {
                touchedPostion = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Jump((endTouchPosition - touchedPostion).normalized);
            }

        }
    }

    void Jump(Vector3 dir)
    {
        foreach(Rigidbody rgb in SausageSpawner.Instance.sausageBodies)
        {

            rgb.AddForce(dir * forcePower, ForceMode.Impulse);
        }
    }
}
