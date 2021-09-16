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

    public bool reset, spawn;


    public List<Rigidbody> sausageBodies;

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

    public void ResetSausage()
    {
        if (!reset)
        {
            reset = true;
            sausageBodies.Clear();
            parentTransform.transform.rotation = Quaternion.identity;
        }
    }

    public void Spawn()
    {
        int count = (int)(length / partDistance);

        for(int i = 0; i < count; i++)
        {
            GameObject gameObject;
            gameObject = parentTransform.AddChild(partPrefab);
            gameObject.transform.localEulerAngles = new Vector3(180, 0, 0);
            gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + partDistance * (i + 1), transform.localPosition.z);
            gameObject.transform.localScale = new Vector3(scale,scale,scale);
            gameObject.name = parentTransform.transform.childCount.ToString();
            sausageBodies.Add(gameObject.GetComponent<Rigidbody>());
            if (i == 0)
            {
                Destroy(gameObject.GetComponent<HingeJoint>());
            }
            else
            {
                gameObject.GetComponent<HingeJoint>().connectedBody = parentTransform.transform.Find((parentTransform.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
        sausageBodies[0].gameObject.AddComponent<SnakeMovement>();
        sausageBodies[0].constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX; 
        //sausageBodies[0].isKinematic = true;
        //sausageBodies[0].useGravity = false;

        CameraController.Instance.player = sausageBodies[0].gameObject;
        CameraController.Instance.CalculateOffset();

        parentTransform.transform.localEulerAngles = new Vector3(90, 180, 0);
        parentTransform.transform.localPosition = new Vector3(-1.6f, 3.4f, -25.0f);
    }
}
