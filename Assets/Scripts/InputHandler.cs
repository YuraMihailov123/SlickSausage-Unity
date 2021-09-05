using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    float forcePower = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && SausageSpawner.Instance.IsSausageOnGround())
        {
            Jump();
            Debug.Log("Touched");
        }

        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Finger moved");
            }
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Finger began");
            }
            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Finger ended");
            }

        }*/
    }

    void Jump()
    {
        foreach(Rigidbody rgb in SausageSpawner.Instance.sausageBodies)
        {
            rgb.AddForce(new Vector3(1.0f, 1.25f, 0) * forcePower, ForceMode.Impulse);
        }
    }
}
