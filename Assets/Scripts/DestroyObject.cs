using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    void Update()
    {
        if (CameraController.Instance.player != null)
        {
            if (CameraController.Instance.player.transform.localPosition.x - transform.localPosition.x > 20)
            {
                Destroy(gameObject);
            }
        }
    }
}
