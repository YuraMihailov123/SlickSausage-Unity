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
    float partDistance = 6.12f;

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;

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

            gameObject = Instantiate(partPrefab, new Vector3(transform.position.x , transform.position.y + partDistance * (i + 1), transform.position.z ), partPrefab.transform.rotation, parentTransform.transform);
            gameObject.transform.eulerAngles = new Vector3(180, 0, 0);

            gameObject.name = parentTransform.transform.childCount.ToString();
            
            if (i == 0)
            {
                Destroy(gameObject.GetComponent<CharacterJoint>());
            }
            else
            {
                gameObject.GetComponent<CharacterJoint>().connectedBody = parentTransform.transform.Find((parentTransform.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
    }
}
