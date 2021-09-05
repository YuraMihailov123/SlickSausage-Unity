using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    #endregion

    public GameObject player; 

    private Vector3 offset;    

    void Start()
    {
        if (player != null)
            offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (player != null)
            transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y,transform.position.z);
    }
}
