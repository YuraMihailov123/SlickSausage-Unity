using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;

    [SerializeField]
    string collidingObjectTag = "";

    private void Update()
    {
        Debug.Log(transform.localPosition.z + " - " + (MapController.Instance.yPos + 30));
        if (transform.localPosition.z > MapController.Instance.yPos + 15)
        {
            SausageSpawner.Instance.ResetSausage();
            GameControllerUI.Instance.restart.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == collidingObjectTag)
        {
            isGrounded = true;
            MapController.Instance.CheckIfNeedToAddNewWall();
        }
        
    }

    private void OnCollisionStay(Collision collision)
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
