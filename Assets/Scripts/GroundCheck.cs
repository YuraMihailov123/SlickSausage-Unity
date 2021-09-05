using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    float distToGround = 0;

    private void Start()
    {
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    private void Update()
    {
        Debug.Log(isGrounded());
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
