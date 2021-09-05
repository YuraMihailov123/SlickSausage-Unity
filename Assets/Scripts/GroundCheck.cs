using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    bool isGrounded = false;

    [SerializeField]
    string collidingObjectTag = "";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == collidingObjectTag)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == collidingObjectTag)
        {
            isGrounded = false;
        }
    }
}
