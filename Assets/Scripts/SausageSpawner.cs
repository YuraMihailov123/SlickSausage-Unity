using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageSpawner : MonoBehaviour
{
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
    bool reset, spawn, snapFirst, snapLast;

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

    public void Spawn()
    {
        int count = (int)(length / partDistance);

        for(int i = 0; i < count; i++)
        {
            GameObject gameObject;
            gameObject = parentTransform.AddChild(partPrefab);
            //gameObject = Instantiate(partPrefab, new Vector3(transform.localPosition.x , transform.localPosition.y + partDistance * (i + 1), transform.localPosition.z ), partPrefab.transform.rotation, parentTransform.transform);
            gameObject.transform.eulerAngles = new Vector3(180, 0, 0);
            gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + partDistance * (i + 1), transform.localPosition.z);
            gameObject.transform.localScale = new Vector3(scale,scale,scale);
            Debug.Log(partDistance * (i + 1));
            gameObject.name = parentTransform.transform.childCount.ToString();
            
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
    }
}
