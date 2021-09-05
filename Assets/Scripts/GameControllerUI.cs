using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerUI : MonoBehaviour
{
    #region Singleton
    private static GameControllerUI _instance;
    public static GameControllerUI Instance { get { return _instance; } }

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

    public GameObject start;
    public GameObject restart;

    private void Start()
    {
        start = transform.Find("Start").gameObject;
        restart = transform.Find("Fail").gameObject;
    }

    public void StartGame(GameObject obj)
    {
        SausageSpawner.Instance.spawn = true;
        obj.SetActive(false);
    }

    public void Restart(GameObject obj)
    {
        SausageSpawner.Instance.reset = false;
        SausageSpawner.Instance.spawn = true;
        obj.SetActive(false);
    }
}
