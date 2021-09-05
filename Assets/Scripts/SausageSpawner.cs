using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageSpawner : MonoBehaviour
{
    #region Singleton
    private static SausageSpawner _instance;
    public static SausageSpawner Instance { get { return _instance; } }

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


    [SerializeField]
    GameObject partPrefab;
    [SerializeField]
    GameObject parentTransform;

    [SerializeField]
    [Range(1, 1000)]
    int length = 1;

    [SerializeField]
    float partDistance = 7.12f;

    [SerializeField]
    float scale = 1f;

    [SerializeField]
    bool reset, spawn;


    public List<Rigidbody> sausageBodies;
    public List<GroundCheck> sausageGroundChecks;

    private void Update()
    {
        if (reset)
        {
            foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(gameObject);
            }
        }

        if (spawn)
        {
            Spawn();

            spawn = false;
        }
    }

    public bool IsSausageOnGround()
    {
        foreach(GroundCheck groundCheck in sausageGroundChecks)
        {
            if (groundCheck.isGrounded)
                return true;
        }
        return false;
    }

    public void Spawn()
    {
        int count = (int)(length / partDistance);

        for(int i = 0; i < count; i++)
        {
            GameObject gameObject;
            gameObject = parentTransform.AddChild(partPrefab);
            gameObject.transform.eulerAngles = new Vector3(180, 0, 0);
            gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + partDistance * (i + 1), transform.localPosition.z);
            gameObject.transform.localScale = new Vector3(scale,scale,scale);
            gameObject.name = parentTransform.transform.childCount.ToString();
            sausageBodies.Add(gameObject.GetComponent<Rigidbody>());
            sausageGroundChecks.Add(gameObject.GetComponent<GroundCheck>());
            if (i == 0)
            {
                Destroy(gameObject.GetComponent<HingeJoint>());
            }
            else
            {
                gameObject.GetComponent<HingeJoint>().connectedBody = parentTransform.transform.Find((parentTransform.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
        parentTransform.transform.eulerAngles = new Vector3(90, 0, 0);
        parentTransform.transform.localPosition = new Vector3(-2.4f, 1.4f, -1.0f);
        CameraController.Instance.player = sausageBodies[0].gameObject;
    }
}
